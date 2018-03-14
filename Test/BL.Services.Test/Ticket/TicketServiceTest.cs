using System.Data;
using FirstUnitTest.BL.BE.Ticket;
using FirstUnitTest.BL.Services.Ticket;
using FirstUnitTest.DA.Whatever.Ticket;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace FirstUnitTest.BL.Services.Test.Ticket
{
    /// <summary>
    /// 測試TicketServiceTest
    /// </summary>
    public class TicketServiceTest
    {
        /// <summary>
        /// StubSCV檔案內容
        /// </summary>
        private DataTable _stubScvContentDataTable;

        /// <summary>
        /// 票券主檔資料
        /// </summary>
        private TicketEntity _stubTicketEntity;

        /// <summary>
        /// ITicketRepository
        /// </summary>
        private ITicketRepository _stubTicketRepository;

        /// <summary>
        /// ITicketValidator
        /// </summary>
        private ITicketValidator _stubTicketValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicketServiceTest"/> class
        /// </summary>
        public TicketServiceTest()
        {
            this._stubTicketRepository = Substitute.For<ITicketRepository>();
            this._stubTicketValidator = Substitute.For<ITicketValidator>();
        }

        /// <summary>
        /// 驗證新增票券子檔的工作流程
        /// </summary>
        [Fact]
        public void Test_InsertSlave()
        {

        }

        /// <summary>
        /// 建立測試目標物件
        /// </summary>
        /// <returns>StubTicketService</returns>
        private TicketService CreateTargetInstance()
        {
            var target = new TicketService(
                this._stubTicketRepository,
                this._stubTicketValidator);

            return target;
        }
    }
}
