using System.Data;
using FirstUnitTest.BL.BE.Ticket;
using FirstUnitTest.BL.Services.Ticket;
using FluentAssertions;
using Xunit;

namespace FirstUnitTest.BL.Services.Test.Ticket
{
    /// <summary>
    /// 測試TicketValidator
    /// </summary>
    public class TicketValidatorTest
    {
        /// <summary>
        /// 驗證檢查條碼檔案資料，三組條碼格式為CODE_128，驗證通過
        /// </summary>
        [Fact]
        public void Test_CheckFileData_ThreeCode128_Validation()
        {
        }

        /// <summary>
        /// 產生條碼檔案資料，三組條碼格式為CODE_128的基本資料
        /// </summary>
        /// <returns>條碼檔案資料，三組條碼格式為CODE_128的基本資料</returns>
        private DataTable GeneratorThreeBarCode128BaseDataTable()
        {
            var dataTable = this.GeneratorBarCodBaseDataTable();

            string[] code1List = new string[]
            {
                "xy2018",
                "xy2018",
                "xy2018",
                "xy2018",
                "xy2018"
            };

            string[] code2List = new string[]
            {
                "0102",
                "0102",
                "0102",
                "0102",
                "0102"
            };

            string[] code3List = new string[]
            {
                "XY00001",
                "XY00002",
                "XY00003",
                "XY00004",
                "XY00005"
            };

            for (int i = 0; i < code1List.Length; i++)
            {
                var row = dataTable.NewRow();

                row[0] = 1L;
                row[1] = code1List[i];
                row[2] = code2List[i];
                row[3] = code3List[i];
                row[4] = "Normal";
                row[5] = string.Empty;

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        /// <summary>
        /// 產生條碼檔案資料的基本欄位(不含資料)
        /// </summary>
        /// <returns>條碼檔案資料的基本欄位</returns>
        private DataTable GeneratorBarCodBaseDataTable()
        {
            var dataTable = new DataTable("TicketSlave");

            dataTable.Columns.Add("TicketSlave_Id", typeof(long));
            dataTable.Columns.Add("TicketSlave_Code1", typeof(string));
            dataTable.Columns.Add("TicketSlave_Code2", typeof(string));
            dataTable.Columns.Add("TicketSlave_Code3", typeof(string));
            dataTable.Columns.Add("TicketSlave_StatusTypeDef", typeof(string));
            dataTable.Columns.Add("TicketSlave_InvalidationData", typeof(string));

            return dataTable;
        }
    }
}
