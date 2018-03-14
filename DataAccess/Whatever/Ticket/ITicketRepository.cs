using System.Data;
using FirstUnitTest.BL.BE.Ticket;

namespace FirstUnitTest.DA.Whatever.Ticket
{
    /// <summary>
    /// ITicketRepository
    /// </summary>
    public interface ITicketRepository
    {
        /// <summary>
        /// 取得票券主檔資料
        /// </summary>
        /// <param name="id">序號</param>
        /// <returns>票券主檔資料</returns>
        TicketEntity Get(long id);

        /// <summary>
        /// 新增票券子檔資料
        /// </summary>
        /// <param name="dataTable">票券子檔資料</param>
        void InsertSlave(DataTable dataTable);
    }
}
