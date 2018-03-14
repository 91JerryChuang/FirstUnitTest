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
            //// Arrange
            var stubTicketEntity = new TicketEntity
            {
                BarCodeTypeDef = "CODE_128",
                CodeNumber = 3
            };

            var sourceDataTable = this.GeneratorThreeBarCode128BaseDataTable();

            var expectedDataTable = this.GeneratorThreeBarCode128BaseDataTable();

            var target = new TicketValidator();

            //// Act
            target.CheckFileData(stubTicketEntity, sourceDataTable);

            //// Assert
            var expectedItemArray = expectedDataTable.AsEnumerable().Select(x => x.ItemArray);
            var actualItemArray = sourceDataTable.AsEnumerable().Select(x => x.ItemArray);

            actualItemArray.ShouldBeEquivalentTo(expectedItemArray);
        }

        /// <summary>
        /// 驗證檢查碼檔案資料，三組條碼格式為CODE_128，其中一筆資料長度超過上限
        /// </summary>
        [Fact]
        public void Test_CheckFileData_ThreeCode128_HasOneData_CodeLengthOverLimit_Invalidation()
        {
            //// Arrange
            var stubTicketEntity = new TicketEntity
            {
                BarCodeTypeDef = "CODE_128",
                CodeNumber = 3
            };

            var sourceDataTable = this.GeneratorThreeBarCode128BaseDataTable();

            sourceDataTable.Rows[1]["TicketSlave_Code2"] = "123456789012345678901";

            var expectedDataTable = this.GeneratorThreeBarCode128BaseDataTable();
            expectedDataTable.Rows[1]["TicketSlave_Code2"] = "123456789012345678901";

            expectedDataTable.Rows[1]["TicketSlave_StatusTypeDef"] =
                TicketSlaveStatusEnum.CodeLengthOverLimit;

            expectedDataTable.Rows[1]["TicketSlave_InvalidationData"] =
                string.Format(
                    "{0},{1},{2}",
                    expectedDataTable.Rows[1]["TicketSlave_Code1"],
                    expectedDataTable.Rows[1]["TicketSlave_Code2"],
                    expectedDataTable.Rows[1]["TicketSlave_Code3"]);

            var target = new TicketValidator();

            //// Act
            target.CheckFileData(stubTicketEntity, sourceDataTable);

            //// Assert
            var expectedItemArray = expectedDataTable.AsEnumerable().Select(x => x.ItemArray);
            var actualItemArray = sourceDataTable.AsEnumerable().Select(x => x.ItemArray);

            actualItemArray.ShouldBeEquivalentTo(expectedItemArray);
        }

        /// <summary>
        /// 驗證檢查條碼檔案資料，三組條碼格式為CODE_128，其中一筆資料不符合CODE_128的格式
        /// </summary>
        [Fact]
        public void Test_CheckFileData_ThreeCode128_HasOneData_InvalidationCodeFormat_Invalidation()
        {
            //// Arrange
            var stubTicketEntity = new TicketEntity
            {
                BarCodeTypeDef = "CODE_128",
                CodeNumber = 3
            };

            var sourceDataTable = this.GeneratorThreeBarCode128BaseDataTable();

            sourceDataTable.Rows[1]["TicketSlave_Code2"] = "九易App0001";

            var expectedDataTable = this.GeneratorThreeBarCode128BaseDataTable();
            expectedDataTable.Rows[1]["TicketSlave_Code2"] = "九易App0001";

            expectedDataTable.Rows[1]["TicketSlave_StatusTypeDef"] =
                TicketSlaveStatusEnum.InvalidationCodeFormat;

            expectedDataTable.Rows[1]["TicketSlave_InvalidationData"] =
                string.Format(
                    "{0},{1},{2}",
                    expectedDataTable.Rows[1]["TicketSlave_Code1"],
                    expectedDataTable.Rows[1]["TicketSlave_Code2"],
                    expectedDataTable.Rows[1]["TicketSlave_Code3"]);

            var target = new TicketValidator();

            //// Act
            target.CheckFileData(stubTicketEntity, sourceDataTable);

            //// Assert
            var expectedItemArray = expectedDataTable.AsEnumerable().Select(x => x.ItemArray);
            var actualItemArray = sourceDataTable.AsEnumerable().Select(x => x.ItemArray);

            actualItemArray.ShouldBeEquivalentTo(expectedItemArray);
        }

        /// <summary>
        /// 驗證檢查條碼檔案資料，三組條碼格式為CODE_128，其中一筆資料長度超過上限且不符合CODE_128的格式，只會有長度超過上限的錯誤
        /// </summary>
        [Fact]
        public void Test_CheckFileData_ThreeCode128_HasOneData_CodeLengthOverLimit_And_InvalidationCodeFormat_Invalidation()
        {
            //// Arrange
            var stubTicketEntity = new TicketEntity
            {
                BarCodeTypeDef = "CODE_128",
                CodeNumber = 3
            };

            var sourceDataTable = this.GeneratorThreeBarCode128BaseDataTable();

            sourceDataTable.Rows[1]["TicketSlave_Code2"] = "12345678901234567890九易App0001";

            var expectedDataTable = this.GeneratorThreeBarCode128BaseDataTable();
            expectedDataTable.Rows[1]["TicketSlave_Code2"] = "12345678901234567890九易App0001";

            expectedDataTable.Rows[1]["TicketSlave_StatusTypeDef"] =
                TicketSlaveStatusEnum.CodeLengthOverLimit;

            expectedDataTable.Rows[1]["TicketSlave_InvalidationData"] =
                string.Format(
                    "{0},{1},{2}",
                    expectedDataTable.Rows[1]["TicketSlave_Code1"],
                    expectedDataTable.Rows[1]["TicketSlave_Code2"],
                    expectedDataTable.Rows[1]["TicketSlave_Code3"]);

            var target = new TicketValidator();

            //// Act
            target.CheckFileData(stubTicketEntity, sourceDataTable);

            //// Assert
            var expectedItemArray = expectedDataTable.AsEnumerable().Select(x => x.ItemArray);
            var actualItemArray = sourceDataTable.AsEnumerable().Select(x => x.ItemArray);

            actualItemArray.ShouldBeEquivalentTo(expectedItemArray);
        }

        /// <summary>
        /// 驗證檢查條碼檔案資料，三組條碼格式為CODE_128，略過無效資料
        /// </summary>
        [Fact]
        public void Test_CheckFileData_ThreeCode128_Ignore_InvalidationData()
        {
            //// Arrange
            var stubTicketEntity = new TicketEntity
            {
                BarCodeTypeDef = "CODE_128",
                CodeNumber = 3
            };

            var sourceDataTable = this.GeneratorThreeBarCode128BaseDataTable();
            sourceDataTable.Rows[1]["TicketSlave_Code1"] = string.Empty;
            sourceDataTable.Rows[1]["TicketSlave_Code2"] = string.Empty;
            sourceDataTable.Rows[1]["TicketSlave_Code3"] = string.Empty;
            sourceDataTable.Rows[1]["TicketSlave_StatusTypeDef"] =
                TicketSlaveStatusEnum.InvalidationData;
            sourceDataTable.Rows[1]["TicketSlave_InvalidationData"] =
                "無效資料,無效資料,無效資料,無效資料";

            var expectedDataTable = this.GeneratorThreeBarCode128BaseDataTable();
            expectedDataTable.Rows[1]["TicketSlave_Code1"] = string.Empty;
            expectedDataTable.Rows[1]["TicketSlave_Code2"] = string.Empty;
            expectedDataTable.Rows[1]["TicketSlave_Code3"] = string.Empty;
            expectedDataTable.Rows[1]["TicketSlave_StatusTypeDef"] =
                TicketSlaveStatusEnum.InvalidationData;
            expectedDataTable.Rows[1]["TicketSlave_InvalidationData"] =
                "無效資料,無效資料,無效資料,無效資料";

            var target = new TicketValidator();

            //// Act
            target.CheckFileData(stubTicketEntity, sourceDataTable);

            //// Assert
            var expectedItemArray = expectedDataTable.AsEnumerable().Select(x => x.ItemArray);
            var actualItemArray = sourceDataTable.AsEnumerable().Select(x => x.ItemArray);

            actualItemArray.ShouldBeEquivalentTo(expectedItemArray);
        }

        /// <summary>
        /// 驗證檢查條碼檔案資料，其中一筆資料已存在於同份檔案
        /// </summary>
        [Fact]
        public void Test_CheckFileData_HasOneData_ExistInSameFile_Invalidation()
        {
            //// Arrange
            var stubTicketEntity = new TicketEntity
            {
                BarCodeTypeDef = "CODE_39",
                CodeNumber = 3
            };

            var sourceDataTable = this.GeneratorThreeBarCode39BaseDataTable();

            sourceDataTable.Rows[0]["TicketSlave_Code1"] = "AB2018";
            sourceDataTable.Rows[0]["TicketSlave_Code2"] = "0102";
            sourceDataTable.Rows[0]["TicketSlave_Code3"] = "/+00001";
            sourceDataTable.Rows[1]["TicketSlave_Code1"] = "AB2018";
            sourceDataTable.Rows[1]["TicketSlave_Code2"] = "0102";
            sourceDataTable.Rows[1]["TicketSlave_Code3"] = "/+00001";

            var expectedDataTable = this.GeneratorThreeBarCode39BaseDataTable();
            expectedDataTable.Rows[0]["TicketSlave_Code1"] = "AB2018";
            expectedDataTable.Rows[0]["TicketSlave_Code2"] = "0102";
            expectedDataTable.Rows[0]["TicketSlave_Code3"] = "/+00001";
            expectedDataTable.Rows[1]["TicketSlave_Code1"] = "AB2018";
            expectedDataTable.Rows[1]["TicketSlave_Code2"] = "0102";
            expectedDataTable.Rows[1]["TicketSlave_Code3"] = "/+00001";

            expectedDataTable.Rows[1]["TicketSlave_StatusTypeDef"] =
                TicketSlaveStatusEnum.ExistInSameFile;

            expectedDataTable.Rows[1]["TicketSlave_InvalidationData"] =
                string.Format(
                    "{0},{1},{2}",
                    expectedDataTable.Rows[1]["TicketSlave_Code1"],
                    expectedDataTable.Rows[1]["TicketSlave_Code2"],
                    expectedDataTable.Rows[1]["TicketSlave_Code3"]);

            var target = new TicketValidator();

            //// Act
            target.CheckFileData(stubTicketEntity, sourceDataTable);

            //// Assert
            var expectedItemArray = expectedDataTable.AsEnumerable().Select(x => x.ItemArray);
            var actualItemArray = sourceDataTable.AsEnumerable().Select(x => x.ItemArray);

            actualItemArray.ShouldBeEquivalentTo(expectedItemArray);
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
        /// 產生條碼檔案資料，三組條碼格式為CODE_39的基本資料
        /// </summary>
        /// <returns>條碼檔案資料，兩組條碼格式為CODE_39的基本資料</returns>
        private DataTable GeneratorThreeBarCode39BaseDataTable()
        {
            var dataTable = this.GeneratorBarCodBaseDataTable();

            string[] code1List = new string[]
            {
                "AB2018",
                "AB2018",
                "AB2018",
                "AB2018",
                "AB2018"
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
                "/+00001",
                "/+00002",
                "/+00003",
                "/+00004",
                "/+00005"
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
