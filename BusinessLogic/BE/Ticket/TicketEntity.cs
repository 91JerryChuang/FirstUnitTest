namespace FirstUnitTest.BL.BE.Ticket
{
    /// <summary>
    /// TicketEntity
    /// </summary>
    public class TicketEntity
    {
        /// <summary>
        /// 序號
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// 條碼類型
        /// </summary>
        public string BarCodeTypeDef { get; set; }

        /// <summary>
        /// 條碼組數
        /// </summary>
        public byte CodeNumber { get; set; }

        /// <summary>
        /// 存放檔案資料夾的Guid
        /// </summary>
        public string FolderGuid { get; set; }

        /// <summary>
        /// 檔案名稱
        /// </summary>
        public string FileName { get; set; }
    }
}
