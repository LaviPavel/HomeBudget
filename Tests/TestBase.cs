using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using BackEnd;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    public class TestBase
    {
        private static ILog _log = LogManager.GetLogger(typeof(TestBase));
        private static Random rnd = new Random();

        public static string TableName = "monthTableTest";
        public static DbHandler TestDb;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {

        }

        //public static ObservableCollection<ExpensesObj> RunBasicFlow(UpdateAction action, string categoryName = null, string subCategoryName = null, double expectedExpenseAmount = -1, double actualExpenseAmount = -1, string description = null)
        //{
        //    // create object according to test scenario
        //    ExpensesObj testobj = new ExpensesObj(
        //        categoryName ?? "testCategory",
        //        subCategoryName ?? "testSubCategory",
        //        expectedExpenseAmount == -1 ? rnd.Next(1, 500000) : expectedExpenseAmount,
        //        actualExpenseAmount == -1 ? rnd.Next(1, 500000) : actualExpenseAmount,
        //        new Guid(),
        //        description ?? "desc"
        //    );
        //    var tempExpensesObj = new ExpensesObj("test", "t", 0, 0, new Guid());


        //    _log.DebugFormat("Test Scenario object is: {0}", testobj);
            

        //    if (CreateNewTable()) return null;

        //    if (action != UpdateAction.Add)
        //    {
        //        //prep table with simple obj
        //        //TestDb.UpdateObjInMonthTable(UpdateAction.Add, tempExpensesObj);
        //        //todo: replace with wait for async db writer
        //        Thread.Sleep(TimeSpan.FromSeconds(2));

        //        //align guids - db check match based on guids
        //        testobj.IdGuid = tempExpensesObj.IdGuid;
        //    }

        //    // update db with relevant action - add, update or remove 
        //    //TestDb.UpdateObjInMonthTable(action, testobj);
        //    //todo: replace with wait for async db writer
        //    Thread.Sleep(TimeSpan.FromSeconds(2));

        //    // get all rows from db and return it
        //    //return TestDb.GetMonthDataFromDb();
        //}

        //private static bool CreateNewTable()
        //{
        //    //create db table and verify it is empty
        //    //var tableDataFromDb = TestDb.GetMonthDataFromDb();
        //    if (tableDataFromDb == null)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        _log.ErrorFormat("DB table isn't empty, printing content");
        //        int count = 1;
        //        foreach (var item in tableDataFromDb)
        //        {
        //            _log.DebugFormat("table content object {0}: {1}", count, item);
        //            count++;
        //        }

        //        return false;
        //    }
        //}

    }
}
