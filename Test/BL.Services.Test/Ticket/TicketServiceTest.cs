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

            this.ArrangeStubEnvironment();
        }

        /// <summary>
        /// 驗證新增票券子檔的工作流程
        /// </summary>
        [Fact]
        public void Test_InsertSlave()
        {
            //// Arrange
            var ticketId = 1L;
            this._stubTicketRepository.Get(ticketId).Returns(this._stubTicketEntity);

            var target = this.CreateTargetInstance();
            target.StubScvContentDataTable = this._stubScvContentDataTable;

            //// Act
            target.InsertSlave(ticketId);

            //// Assert
            target.ActualReceivedTicketEntity.Should().Be(this._stubTicketEntity);

            this._stubTicketRepository.Received(1).Get(Arg.Is(ticketId));

            this._stubTicketValidator.Received(1).CheckFileData(Arg.Is(this._stubTicketEntity), Arg.Is(this._stubScvContentDataTable));

            this._stubTicketRepository.Received(1).InsertSlave(Arg.Is(this._stubScvContentDataTable));
        }

        /// <summary>
        /// 準備Stub物件行為
        /// </summary>
        private void ArrangeStubEnvironment()
        {
            this._stubScvContentDataTable = new DataTable();
            this._stubScvContentDataTable.Columns.Add("TicketSlave_Id", typeof(long));
            this._stubScvContentDataTable.Columns.Add("TicketSlave_Code1", typeof(string));
            this._stubScvContentDataTable.Columns.Add("TicketSlave_Code2", typeof(string));
            this._stubScvContentDataTable.Columns.Add("TicketSlave_Code3", typeof(string));
            this._stubScvContentDataTable.Columns.Add("TicketSlave_StatusTypeDef", typeof(string));
            this._stubScvContentDataTable.Columns.Add("TicketSlave_InvalidationData", typeof(string));
            var row = this._stubScvContentDataTable.NewRow();
            row[0] = 1;
            row[1] = "AB2018";
            row[2] = "0102";
            row[3] = "/+00001";
            row[4] = TicketSlaveStatusEnum.Normal.ToString();
            row[5] = string.Empty;
            this._stubScvContentDataTable.Rows.Add();

            this._stubTicketEntity = new TicketEntity
            {
                Id = 1,
                BarCodeTypeDef = TicketBarCodeTypeEnum.CODE_128.ToString(),
                CodeNumber = 3,
                FileName = "UnitTest.csv",
                FolderGuid = "1234-1234-1234-1234-1234"
            };
        }

        /// <summary>
        /// 建立測試目標物件
        /// </summary>
        /// <returns>StubTicketService</returns>
        private StubTicketService CreateTargetInstance()
        {
            var target = new StubTicketService(
                this._stubTicketRepository,
                this._stubTicketValidator);

            return target;
        }
    }
}
