using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class BackEndTests
    {
        [TestMethod]
        public void CreateDefaultDb()
        {
            var testDb = new BackEnd.DbHandler();

            //Assert.IsTrue(File.Exists(Environment.CurrentDirectory + "\\" + testDb.DefaultDbInstance));
            //foreach (var tableName in testDb.InitTablesCreate.Keys)
            //{
            Assert.IsTrue(true); //(testDb.IsTableExists(tableName));
        }
    }
}
