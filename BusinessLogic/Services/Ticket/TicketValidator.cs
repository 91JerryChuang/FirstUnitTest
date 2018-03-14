using System;
using System.Collections.Generic;
using System.Data;
using FirstUnitTest.BL.BE.Ticket;
using ZXing;

namespace FirstUnitTest.BL.Services.Ticket
{
    /// <summary>
    /// TicketValidator
    /// </summary>
    public class TicketValidator : ITicketValidator
    {
        /// <summary>
        /// BarcodeWriter
        /// </summary>
        private BarcodeWriter _barcodeWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicketValidator"/> class
        /// </summary>
        public TicketValidator()
        {
            this._barcodeWriter = new BarcodeWriter();
        }

        /// <summary>
        /// 檢查票券主檔資料條碼檔案資料
        /// </summary>
        /// <param name="ticketEntity">票券主檔資料</param>
        /// <param name="ticketSlaveCodeData">票券主檔資料暫存資料清單</param>
        public void CheckFileData(TicketEntity ticketEntity, DataTable ticketSlaveCodeData)
        {
            var barCodeNumber = ticketEntity.CodeNumber;

            BarcodeFormat barCodeType = this.GetBarcodeFormatEnum(ticketEntity);

            var verificationCodeHashSet = new HashSet<string>();

            foreach (DataRow row in ticketSlaveCodeData.Rows)
            {
                string[] codeList = new string[]
                {
                    (string) row["TicketSlave_Code1"],
                    (string) row["TicketSlave_Code2"],
                    (string) row["TicketSlave_Code3"]
                };

                var rowStatus = (string)row["TicketSlave_StatusTypeDef"];
                TicketSlaveStatusEnum status;
                if (Enum.TryParse(rowStatus, out status) == false)
                {
                    var errorMessage = $"條碼來源子檔狀態類型不正確，Ticket_Id: {ticketEntity.Id}、TicketSlave_StatusTypeDef: {rowStatus}";
                    throw new ArgumentException(errorMessage);
                }

                //// 依據條碼組數設定，決定要檢查到第幾組條碼
                for (int index = 0; index < barCodeNumber; index++)
                {
                    if (status == TicketSlaveStatusEnum.Normal)
                    {
                        status = this.CheckTicketCode(codeList[index], barCodeType);
                        if (status != TicketSlaveStatusEnum.Normal)
                        {
                            row["TicketSlave_StatusTypeDef"] = status.ToString();
                        }
                    }
                }

                if (status != TicketSlaveStatusEnum.Normal)
                {
                    var invalidationData = string.Join(",", codeList).TrimEnd(',');

                    row["TicketSlave_InvalidationData"] = invalidationData;
                }
            }
        }

        /// <summary>
        /// 檢查條碼
        /// </summary>
        /// <param name="code">條碼</param>
        /// <param name="barCodeType">條碼類型</param>
        /// <returns>TicketSlaveStatusEnum</returns>
        private TicketSlaveStatusEnum CheckTicketCode(string code, BarcodeFormat barCodeType)
        {
            //// 條碼長度上限
            var codeLengthLimit = 20;
            if (code.Length > codeLengthLimit)
            {
                return TicketSlaveStatusEnum.CodeLengthOverLimit;
            }

            //// 使用ZXing.BarcodeWriter的Encode方法來檢查條碼，是否符合CODE_39或CODE_128的格式
            try
            {
                this._barcodeWriter.Format = barCodeType;
                this._barcodeWriter.Encode(code);

                return TicketSlaveStatusEnum.Normal;
            }
            catch (Exception)
            {
                return TicketSlaveStatusEnum.InvalidationCodeFormat;
            }
        }

        /// <summary>
        /// 取得BarcodeFormat
        /// </summary>
        /// <param name="ticketEntity">票券主檔資料</param>
        /// <returns>BarcodeFormat</returns>
        private BarcodeFormat GetBarcodeFormatEnum(TicketEntity ticketEntity)
        {
            TicketBarCodeTypeEnum eCouponBarCodeType;

            if (Enum.TryParse(ticketEntity.BarCodeTypeDef, out eCouponBarCodeType) == false)
            {
                var errorMessage = $"條碼類型不正確，ECoupon_Id: {ticketEntity.Id}、BarCodeTypeDef: {ticketEntity.BarCodeTypeDef}";
                throw new ArgumentException(errorMessage);
            }

            if (eCouponBarCodeType == TicketBarCodeTypeEnum.CODE_128)
            {
                return BarcodeFormat.CODE_128;
            }

            return BarcodeFormat.CODE_39;
        }
    }
}
