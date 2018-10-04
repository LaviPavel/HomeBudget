using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using BackEnd;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class BackEndTests : TestBase
    {
        private static ILog _log = LogManager.GetLogger(typeof(BackEndTests));
        
        public TestContext TestContext { get; set; }

        public new static void ClassInit(TestContext context)
        {
            TestBase.ClassInit(context);
        }

        [TestInitialize]
        public void TestInit()
        {
            TestDb = new DbHandler { MonthTableName = TableName };
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            SQLiteCommand command = TestDb.Connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "drop table " + TableName;

            TestDb.Dbwriter(command);
            TestDb.Close();
        }


        [TestMethod]
        [TestCategory("Sanity")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestData\\SanityTestParams.xml", "test", DataAccessMethod.Sequential)]
        public void ActionsOnObjInDb()
        {
            UpdateAction actionType;
            Enum.TryParse((string)TestContext.DataRow["actionType"], out actionType);
            int expectedObjectsCount = Convert.ToInt32(TestContext.DataRow["ResultsCount"]);
            _log.InfoFormat("Test params are, Type: {0} Count: {1}", actionType, expectedObjectsCount);

            var actualResult = RunBasicFlow(actionType);

            Assert.AreEqual(expectedObjectsCount, actualResult.Count, "Action type is: " + actionType);
        }

        [TestMethod]
        [TestCategory("Functionality")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestData\\Localization.xml", "test", DataAccessMethod.Sequential)]
        public void Localization()
        {
            string itemName = (string)TestContext.DataRow["language"];
            _log.InfoFormat("Test params are, language: {0}", itemName);

            var actualResult = RunBasicFlow(UpdateAction.Update, itemName);

            Assert.AreEqual(itemName, actualResult.FirstOrDefault()?.Category, "Expected value: " + itemName);
        }

        [TestMethod]
        [TestCategory("Functionality")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestData\\SpecialChars.xml", "test", DataAccessMethod.Sequential)]
        public void SpecialChars()
        {
            string itemName = (string)TestContext.DataRow["chars"];
            _log.InfoFormat("Test params are, Special chars: {0}", itemName);

            var actualResult = RunBasicFlow(UpdateAction.Update, null, itemName);

            Assert.AreEqual(itemName, actualResult.FirstOrDefault()?.SubCategory, "Expected value: " + itemName);
        }


        [TestMethod]
        [TestCategory("Functionality")]
        public void CreateDefaultDb()
        {
            var isFileExists = File.Exists(Environment.CurrentDirectory + "\\" + TestDb.DefaultDbInstance);

            Assert.IsTrue(isFileExists);
        }

        [TestMethod]
        [TestCategory("Functionality")]
        public void CreateGivenDb()
        {
            string dbFileName = "test";
            var testDb = new DbHandler(dbFileName);
            var isFileExists = File.Exists(Environment.CurrentDirectory + "\\" + testDb.DefaultDbInstance);

            Assert.IsTrue(isFileExists);
        }

        [TestMethod]
        [TestCategory("Functionality")]
        public void CloseSqlConnection()
        {
            TestDb.Close();

            Assert.IsTrue(TestDb.Connection.State == ConnectionState.Closed);
        }

        [TestMethod]
        [TestCategory("Functionality")]
        public void CreateMonthTableTest()
        {
           Assert.IsNotNull(TestDb.GetMonthDataFromDb());
        }
        

    }
}
