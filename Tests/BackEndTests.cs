using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading;
using BackEnd;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class BackEndTests
    {
        private ExpensesObj _testObj = new ExpensesObj("testcat", "testsubcat", 100, 50, Guid.NewGuid(), "pipi");
        private static string _tableName = "monthTableTest";
        private DbHandler _testDb = new DbHandler { MonthTableName = _tableName };

        [TestCleanup]
        public void TestCleanUp()
        {
            SQLiteCommand command = _testDb.Connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "drop table " + _tableName;

            _testDb.Dbwriter(command);
            _testDb.Close();
        }


        [TestMethod]
        [TestCategory("Functionality")]
        public void CreateDefaultDb()
        {
            var isFileExists = File.Exists(Environment.CurrentDirectory + "\\" + _testDb.DefaultDbInstance);

            Assert.IsTrue(isFileExists);
        }

        [TestMethod]
        [TestCategory("Functionality")]
        public void CreateGivenDb()
        {
            string dbFileName = "test";
            var testDb = new BackEnd.DbHandler(dbFileName);
            var isFileExists = File.Exists(Environment.CurrentDirectory + "\\" + testDb.DefaultDbInstance);

            Assert.IsTrue(isFileExists);
        }

        [TestMethod]
        [TestCategory("Functionality")]
        public void CloseSqlConnection()
        {
            _testDb.Close();

            Assert.IsTrue(_testDb.Connection.State == ConnectionState.Closed);
        }

        [TestMethod]
        [TestCategory("Functionality")]
        public void CreateMonthTableTest()
        {
           Assert.IsNotNull(_testDb.GetMonthDataFromDb());
        }

        [TestMethod]
        [TestCategory("Functionality")]
        public void UpdateObjInMonthTableAddTest()
        {
            var bla = _testDb.GetMonthDataFromDb();
            _testDb.UpdateObjInMonthTable(UpdateAction.Add, _testObj);
            Thread.Sleep(TimeSpan.FromSeconds(2));

            Assert.AreEqual(_testDb.GetMonthDataFromDb().Count, 2);
        }

        [TestMethod]
        [TestCategory("Functionality")]
        public void UpdateObjInMonthTableUpdateTest()
        {
            ExpensesObj testobj = _testDb.GetMonthDataFromDb().First();
            testobj.Category = "updatedObj";

            _testDb.UpdateObjInMonthTable(UpdateAction.Update, testobj);
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Assert.AreEqual(_testDb.GetMonthDataFromDb().Count, 1);
        }

        [TestMethod]
        [TestCategory("Functionality")]
        public void UpdateObjInMonthTableRemoveTest()
        {
            ExpensesObj testobj = _testDb.GetMonthDataFromDb().First();
            
            _testDb.UpdateObjInMonthTable(UpdateAction.Remove, testobj);
            Thread.Sleep(TimeSpan.FromSeconds(2));

            Assert.AreEqual(0, _testDb.GetMonthDataFromDb().Count);
        }

    }
}
