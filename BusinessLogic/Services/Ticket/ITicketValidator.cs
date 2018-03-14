using System.Data;
using FirstUnitTest.BL.BE.Ticket;

namespace FirstUnitTest.BL.Services.Ticket
{
    /// <summary>
    /// ITicketValidator
    /// </summary>
    public interface ITicketValidator
    {
        /// <summary>
        /// 檢查票券主檔資料條碼檔案資料
        /// </summary>
        /// <param name="ticketEntity">票券主檔資料</param>
        /// <param name="ticketSlaveCodeData">票券主檔資料暫存資料清單</param>
        void CheckFileData(TicketEntity ticketEntity, DataTable ticketSlaveCodeData);
    }
}
