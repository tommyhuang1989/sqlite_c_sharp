using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace sqlite_c_shape {
    public class sqlite_helper {
        public static string DATABASE_NAME = "database.db";
        public static string DATABASE_VERSION = "3";
        public static bool DATABASE_NEW = true;
        public static bool DATABASE_COMPRESS = true;

        private static sqlite_helper helper = null;
        private static SQLiteConnection connection = null;
        private sqlite_helper()
        {
            connection = CreateConnection();
        }

        public static sqlite_helper get_instance()
        {
            if (helper == null)
            {
                helper = new sqlite_helper();
            }

            return helper;
        }

        private static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            string connect_string = "Data Source={0}; Version = {1}; New = {2}; Compress = {3};";
            sqlite_conn = new SQLiteConnection(String.Format(connect_string, DATABASE_NAME, DATABASE_VERSION, DATABASE_NEW, DATABASE_COMPRESS));
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            return sqlite_conn;
        }

        /// <summary>
        /// use IF NOT EXISTS, just create table once.
        /// </summary>
        public void CreateTable()
        {
            SQLiteCommand sqlite_cmd;
            string Createsql = "CREATE TABLE IF NOT EXISTS SampleTable (Col1 VARCHAR(20), Col2 INT)";
            string Createsql1 = "CREATE TABLE IF NOT EXISTS SampleTable1 (Col1 VARCHAR(20), Col2 INT)";
            sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = Createsql1;
            sqlite_cmd.ExecuteNonQuery();
        }

        public void InsertData()
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable (Col1, Col2) VALUES('Test Text ', 1); ";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable (Col1, Col2) VALUES('Test1 Text1 ', 2); ";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable  (Col1, Col2) VALUES('Test2 Text2 ', 3); ";
            sqlite_cmd.ExecuteNonQuery();


            sqlite_cmd.CommandText = "INSERT INTO SampleTable1 (Col1, Col2) VALUES('Test3 Text3 ', 3); ";
            sqlite_cmd.ExecuteNonQuery();

        }

        public void ReadData()
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM SampleTable";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }
            connection.Close();
        }
    }
}
