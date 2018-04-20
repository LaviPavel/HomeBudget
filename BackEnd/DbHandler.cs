using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using log4net;


namespace BackEnd
{
    public class DbHandler
    {
        private static ILog _log = LogManager.GetLogger(typeof(DbHandler));
        private SQLiteConnection _connection;
        private readonly string _defaultDbInstance = "HomeBudgetDb.db";
        private readonly string _monthExpensesTableArgs =
            "( `CategoryID` INTEGER NOT NULL, `SubCategoryID` INTEGER NOT NULL, `ExpectedAmount` INTEGER NOT NULL, `ActualAmount` INTEGER NOT NULL, `Description` TEXT )";
        private readonly Dictionary<string, string> _initTablesCreate = new Dictionary<string, string>
        {
            {"Categories",  "( `Id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, `Name` TEXT )"},
            {"SubCategories", "( `Id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, `CategoryId` INTEGER NOT NULL, `Name` TEXT )" }
        };
        

        public DbHandler(string dbFilePath = null)
        {
            InitDataBaseConnection(dbFilePath);
            InitBaseTables();
        }

        private void InitDataBaseConnection(string dbFilePath = null)
        {
            //handle relevant native sqlite version to use (x86\x64)
            try
            {
                if (File.Exists(Environment.CurrentDirectory))
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

            //check for DB => create DB, init connection
            if (dbFilePath == null)
            {
                SQLiteConnection.CreateFile(_defaultDbInstance);
                dbFilePath = _defaultDbInstance;
            }

            _connection = new SQLiteConnection(@"Data Source=" + dbFilePath + ";FailIfMissing=True;");
            _connection.Open();
        }

        private void InitBaseTables()
        {
            foreach (var tableToCreate in _initTablesCreate)
            {
                CreateTable(tableToCreate.Key, tableToCreate.Value);
            }
        }

        private bool IsTableExists(string tableName)
        {
            SQLiteCommand command = _connection.CreateCommand();
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
                _log.Error("Error accrued while looking for " + tableName + " at the DataBase file " + _defaultDbInstance ,ex);
                throw;
            }
        }

        private void CreateTable(string tableName, string tableArgs)
        {
            SQLiteCommand command = _connection.CreateCommand();
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
        public void Close()
        {
            _connection.Close();
        }
        
        public Collection<ExpensesObj> GetMonthDataFromDb(string tableName)
        {
            var expenses = new Collection<ExpensesObj>();

            if (!IsTableExists(tableName))
            {
                CreateMonthTable(tableName);
            }
            
            using (SQLiteCommand command = _connection.CreateCommand())
            {
                command.CommandText = @"select b.Name, c.Name as CategoryName, a.ActualAmount as SubCategoryName, a.ExpectedAmount, a.Description 
                from " + tableName + @" AS a
                inner join Categories AS b on a.CategoryID = b.Id
                inner join SubCategories AS c on a.SubCategoryID = c.Id";

                var rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    try
                    {
                        string desc = rdr.IsDBNull(4)? null : rdr.GetString(4);

                        expenses.Add(new ExpensesObj(rdr.GetString(0), rdr.GetString(1), rdr.GetDouble(2), rdr.GetDouble(3), desc));
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Error reading data from table {0}, {1}", tableName, ex);
                        throw;
                    }
                    
                }
            }

            return expenses;
        }

        private void CreateMonthTable(string tableName)
        {
            CreateTable(tableName, _monthExpensesTableArgs);
            SQLiteCommand command = _connection.CreateCommand();
            command.CommandText = @"insert into Categories (Name) values ('Income')";
            command.ExecuteNonQuery();

            command.CommandText = @"insert into SubCategories (CategoryId, Name) values ((select Id from Categories where Name = 'Income'),'Salary')";
            command.ExecuteNonQuery();

            command.CommandText = @"insert into " + tableName + " (CategoryID, SubCategoryID, ExpectedAmount, ActualAmount) values (" +
                                  "(select Id from Categories where Name = 'Income'),(select Id from SubCategories where Name = 'Salary'),0,0)";
            command.ExecuteNonQuery();

        }

    }
}
