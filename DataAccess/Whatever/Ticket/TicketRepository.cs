using System;
using System.Data;
using System.Threading;
using FirstUnitTest.BL.BE.Ticket;

namespace FirstUnitTest.DA.Whatever.Ticket
{
    /// <summary>
    /// TicketRepository
    /// </summary>
    public class TicketRepository : ITicketRepository
    {
        /// <summary>
        /// 取得票券主檔資料
        /// </summary>
        /// <param name="id">序號</param>
        /// <returns>票券主檔資料</returns>
        public TicketEntity Get(long id)
        {
            return new TicketEntity
            {
                Id = 1,
                BarCodeTypeDef = TicketBarCodeTypeEnum.CODE_128.ToString(),
                CodeNumber = 3,
                FileName = "ticketDemoFile.csv",
                FolderGuid = "fdaa7198-e633-41c5-b61e-92654c3af699"
            };
        }

        /// <summary>
        /// 新增票券子檔資料
        /// </summary>
        /// <param name="dataTable">票券子檔資料</param>
        public void InsertSlave(DataTable dataTable)
        {
            Console.WriteLine("Data Insert DB Start");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine(
                    $"條碼1: {row["TicketSlave_Code1"]}, " +
                    $"條碼2: {row["TicketSlave_Code2"]}, " +
                    $"條碼3: {row["TicketSlave_Code3"]}, " +
                    $"狀態: {row["TicketSlave_StatusTypeDef"]}, " +
                    $"無效資料: {row["TicketSlave_InvalidationData"]} ");

                Thread.Sleep(100);
            }

            Console.WriteLine("Data Insert DB End");
        }
    }
}
