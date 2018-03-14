using System;
using System.Configuration;
using System.Data;
using System.IO;
using FirstUnitTest.BL.BE.Ticket;
using FirstUnitTest.DA.Whatever.Ticket;

namespace FirstUnitTest.BL.Services.Ticket
{
    /// <summary>
    /// TicketService
    /// </summary>
    public class TicketService : ITicketService
    {
        /// <summary>
        /// ITicketRepository
        /// </summary>
        private ITicketRepository _ticketRepository;

        /// <summary>
        /// ITicketValidator
        /// </summary>
        private ITicketValidator _ticketValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicketService"/> class
        /// </summary>
        /// <param name="ticketRepository">ITicketRepository</param>
        /// <param name="ticketValidator">ITicketValidator</param>
        public TicketService(ITicketRepository ticketRepository, ITicketValidator ticketValidator)
        {
            this._ticketRepository = ticketRepository;
            this._ticketValidator = ticketValidator;
        }

        /// <summary>
        /// 新增票券子檔
        /// </summary>
        /// <param name="id">票券主檔序號</param>
        public void InsertSlave(long id)
        {
        }
        
        /// <summary>
        /// 取得檔案內容
        /// </summary>
        /// <param name="ticketEntity">票券主檔資料</param>
        /// <returns>csv檔案的內容</returns>
        protected virtual DataTable LoadFile(TicketEntity ticketEntity)
        {
            //// 檔案儲存路徑
            var ticketFilePath = ConfigurationManager.AppSettings["Ticket.File.Path"];

            var fullFilePath = Path.Combine(ticketFilePath, ticketEntity.FolderGuid, ticketEntity.FileName);

            var fileExtension = Path.GetExtension(fullFilePath).ToLower();

            if (fileExtension != ".csv")
            {
                throw new ApplicationException("csv檔不存在");
            }

            var barCodeNumber = ticketEntity.CodeNumber;

            var dataTable = new DataTable("ECouponCustomVerificationCodeSourceSlaveTemp");
            dataTable.Columns.Add("TicketSlave_Id", typeof(long));
            dataTable.Columns.Add("TicketSlave_Code1", typeof(string));
            dataTable.Columns.Add("TicketSlave_Code2", typeof(string));
            dataTable.Columns.Add("TicketSlave_Code3", typeof(string));
            dataTable.Columns.Add("TicketSlave_StatusTypeDef", typeof(string));
            dataTable.Columns.Add("TicketSlave_InvalidationData", typeof(string));

            //// 讀取csv檔
            using (var reader = new StreamReader(File.OpenRead(fullFilePath)))
            {
                while (!reader.EndOfStream)
                {
                    var fileRowData = reader.ReadLine();

                    if (string.IsNullOrWhiteSpace(fileRowData))
                    {
                        continue;
                    }

                    var row = dataTable.NewRow();
                    row[0] = ticketEntity.Id;
                    row[1] = string.Empty;
                    row[2] = string.Empty;
                    row[3] = string.Empty;
                    row[4] = TicketSlaveStatusEnum.Normal.ToString();
                    row[5] = string.Empty;

                    //// 條碼組數設定，判斷資料是否有效
                    var codeArray = fileRowData.Split(',');
                    if (codeArray.Length != barCodeNumber)
                    {
                        row[4] = TicketSlaveStatusEnum.InvalidationData.ToString();
                        row[5] = fileRowData;
                        dataTable.Rows.Add(row);
                        continue;
                    }

                    //// 條碼組數設定，判斷讀取codeArray的位置與寫入row的位置
                    //// row[1] 等於 ECouponCustomVerificationCodeSourceSlaveTemp_Code1
                    //// row[2] 等於 ECouponCustomVerificationCodeSourceSlaveTemp_Code2
                    //// row[3] 等於 ECouponCustomVerificationCodeSourceSlaveTemp_Code3
                    for (int index = 0; index < barCodeNumber; index++)
                    {
                        row[1 + index] = codeArray[index];
                    }

                    dataTable.Rows.Add(row);
                }
            }

            //// 移除表頭
            dataTable.Rows.RemoveAt(0);

            return dataTable;
        }
    }
}
