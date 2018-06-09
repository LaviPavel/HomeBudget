using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;
using log4net;


namespace BackEnd
{
    public enum UpdateAction { Add, Remove, Update }
    public class DbHandler
    {
        private static ILog _log = LogManager.GetLogger(typeof(DbHandler));
        //private SQLiteConnection _connection;
        //private readonly string _defaultDbInstance = "HomeBudgetDb.db";
        private readonly string _monthExpensesTableArgs =
            "(`Guid` TEXT NOT NULL UNIQUE, `Category` TEXT NOT NULL, `SubCategory` TEXT NOT NULL, `ExpectedAmount` INTEGER NOT NULL, `ActualAmount` INTEGER NOT NULL, `Description` TEXT )";
        private static readonly Object obj = new Object();

        public SQLiteConnection Connection;
        public readonly string DefaultDbInstance = "HomeBudgetDb.db";
        public string MonthTableName;
        public DbHandler(string dbFileName = null)
        {
            InitDataBaseConnection(dbFileName);
        }

        private void InitDataBaseConnection(string dbFilePath = null)
        {
            if (dbFilePath == null)
            {
                dbFilePath = DefaultDbInstance;
            }
            else
            {
                dbFilePath = dbFilePath + ".db";
            }
            
            //handle relevant native sqlite version to use (x86\x64)
            try
            {
                if (!File.Exists(Environment.CurrentDirectory + @"\SQLite.Interop.dll"))
                {
                    File.Copy(Environment.CurrentDirectory + @"\x64\SQLite.Interop.dll",
                        Environment.CurrentDirectory + @"\SQLite.Interop.dll");
                }
            }
            catch (Exception ex)
            {
                _log.Error("SQLite.Interop.dll moving failed: ", ex);
                throw;
            }

            //check for DB file and init connection
            if (!File.Exists(Environment.CurrentDirectory + @"\" + dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
            }

            Connection = new SQLiteConnection(@"Data Source=" + dbFilePath + ";FailIfMissing=True;");
            Connection.Open();
        }
        private bool IsTableExists(string tableName)
        {
            SQLiteCommand command = Connection.CreateCommand();
            command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='" + tableName + "'";
            
            try
            {
                if (command.ExecuteScalar()==null)
                {
                    _log.Warn("Table " + tableName + " was not found at the DB ");
                    return false;
                    
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error accrued while looking for " + tableName + " at the DataBase file " + DefaultDbInstance ,ex);
                throw;
            }
        }
        private void CreateTable(string tableName, string tableArgs)
        {
            SQLiteCommand command = Connection.CreateCommand();
            command.CommandText = "CREATE TABLE '" + tableName + "' " + tableArgs;

            if (!IsTableExists(tableName))
            {
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    _log.Error("Table " + tableName + " was not Created ", ex);
                    throw;
                }
            }
        }
        private void CreateMonthTable()
        {
            CreateTable(this.MonthTableName, _monthExpensesTableArgs);
            SQLiteCommand command = Connection.CreateCommand();
            command.CommandText = @"insert into " + this.MonthTableName + " (Guid, Category, SubCategory, ExpectedAmount, ActualAmount) " +
                                  "values ('" + Guid.NewGuid() + "', 'Income', 'Salary', 0, 1000)";
            Dbwriter(command);
        }
        public async Task Dbwriter(SQLiteCommand cmd)
        {
            lock (obj)
            {
                // critical section
                using (var tra = Connection.BeginTransaction())
                {
                    try
                    {
                        cmd.ExecuteNonQuery();
                        tra.Commit();
                        _log.Debug("Command executed successfully " + cmd.CommandText);
                    }
                    catch (Exception ex)
                    {
                        tra.Rollback();
                        _log.ErrorFormat("I did nothing, because something wrong happened: {0}", ex);
                    }
                }
            }
        }

        public void Close()
        {
            Connection.Close();
            SQLiteConnection.ClearAllPools();         
        }
        public ObservableCollection<ExpensesObj> GetMonthDataFromDb()
        {
            var expenses = new ObservableCollection<ExpensesObj>();
            if (MonthTableName == null)
            {
                return null;
            }

            if (!IsTableExists(this.MonthTableName))
            {
                CreateMonthTable();
            }
            
            using (SQLiteCommand command = Connection.CreateCommand())
            {
                command.CommandText = @"select * from " + this.MonthTableName;
                var rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    try
                    {
                        string desc = rdr.IsDBNull(5)? null : rdr.GetString(5);
                        expenses.Add(new ExpensesObj(rdr.GetString(1), rdr.GetString(2), rdr.GetDouble(3), rdr.GetDouble(4), rdr.GetGuid(0), desc));
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Error reading data from table {0}, {1}", this.MonthTableName, ex);
                        throw;
                    }
                    
                }
            }

            return expenses;
        }
        public async void UpdateObjInMonthTable(UpdateAction action, ExpensesObj newExpensesObj)
        {
            SQLiteCommand command = Connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SQLiteParameter("@IdGuid", newExpensesObj.IdGuid));
            command.Parameters.Add(new SQLiteParameter("@NewCategory", newExpensesObj.Category));
            command.Parameters.Add(new SQLiteParameter("@NewSubCategory", newExpensesObj.SubCategory));
            command.Parameters.Add(new SQLiteParameter("@NewExpectedAmount", newExpensesObj.ExpectedAmount));
            command.Parameters.Add(new SQLiteParameter("@NewActualAmount", newExpensesObj.ActualAmount));
            command.Parameters.Add(new SQLiteParameter("@NewDescription", newExpensesObj.Description));

            switch (action)
            {
                case UpdateAction.Add:
                    command.CommandText = @"insert into " + this.MonthTableName + " (Guid, Category, SubCategory, ExpectedAmount, ActualAmount, Description) values " +
                            "(@IdGuid, @NewCategory, @NewSubCategory, @NewExpectedAmount, @NewActualAmount, @NewDescription)";
                    break;

                case UpdateAction.Remove:
                    command.CommandText = @"DELETE FROM " + this.MonthTableName + " Where Guid='"+ newExpensesObj.IdGuid + "'";
                    break;

                case UpdateAction.Update:
                    command.CommandText = @"UPDATE " + this.MonthTableName +
                            " SET Category=@NewCategory, SubCategory=@NewSubCategory, ExpectedAmount=@NewExpectedAmount, ActualAmount=@NewActualAmount, Description=@NewDescription " +
                            "Where Guid=@IdGuid";
                    break;
            }

            await Dbwriter(command);
        }

        

    }
}
