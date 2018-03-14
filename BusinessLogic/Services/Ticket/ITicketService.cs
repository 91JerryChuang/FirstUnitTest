namespace FirstUnitTest.BL.Services.Ticket
{
    /// <summary>
    /// ITicketService
    /// </summary>
    public interface ITicketService
    {
        /// <summary>
        /// 新增票券子檔
        /// </summary>
        /// <param name="id">票券主檔序號</param>
        void InsertSlave(long id);
    }
}
