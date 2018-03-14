using System.Data;
using FirstUnitTest.BL.BE.Ticket;
using FirstUnitTest.BL.Services.Ticket;
using FirstUnitTest.DA.Whatever.Ticket;

namespace FirstUnitTest.BL.Services.Test.Ticket
{
    /// <summary>
    /// StubTicketService
    /// </summary>
    /// <remarks>
    /// 為隔離TicketService類別直接相依其他物件
    /// </remarks>
    public class StubTicketService : TicketService
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
        /// Initializes a new instance of the <see cref="StubTicketService"/> class
        /// </summary>
        /// <param name="ticketRepository">ITicketRepository</param>
        /// <param name="ticketValidator">ITicketValidator</param>
        public StubTicketService(ITicketRepository ticketRepository, ITicketValidator ticketValidator)
            : base(ticketRepository, ticketValidator)
        {
        }

        /// <summary>
        /// LoadFile 方法接收到的票券主檔資料
        /// </summary>
        public TicketEntity ActualReceivedTicketEntity { get; set; }

        /// <summary>
        /// Stub SCV檔案內容
        /// </summary>
        public DataTable StubScvContentDataTable { get; set; }

        /// <summary>
        /// 取得檔案內容
        /// </summary>
        /// <param name="ticketEntity">票券主檔資料</param>
        /// <returns>csv檔案的內容</returns>
        /// <remarks>
        /// 為隔離直接相依 System.IO
        /// </remarks>
        protected override DataTable LoadFile(TicketEntity ticketEntity)
        {
            this.ActualReceivedTicketEntity = ticketEntity;
            return this.StubScvContentDataTable;
        }
    }
}
