using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsMajor
{

    public class MySqlConnector
    {
        public MySqlConnection connection;
        private string server;
        private string database;
        private string username;
        private string password;

        // constructor
        public MySqlConnector()
        {
            server = "localhost";
            database = "product_management";
            username = "root";
            password = "";
            string connectionString = $"server={server};database={database};uid={username};password={password};";
            connection = new MySqlConnection(connectionString);
        }

        // open the database connection
        public ConnectionState OpenConnection()
        {
            try
            {
                connection.Open();
                return connection.State;
            }
            catch (MySqlException ex)
            {
                // handle the exception
                return connection.State;
            }
        }

        // close the database connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                // handle the exception
                return false;
            }
        }
    }
}
