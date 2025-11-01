using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorsodCoding_WPF_Admin
{
    class ConnectToDatabase
    {
        private string databaseName = "";
        private string userName = "";
        private string password = "";
        private string serverName = "";
        static public MySqlConnection connection;
        private string connectionString = "";

        public ConnectToDatabase() { }

        public ConnectToDatabase(string serverName, string userName, string password, string databaseName)
        {
            try
            {
                connectionString = $"server={serverName};UID={userName};password='{password}';database={databaseName};Convert Zero Datetime=True;Allow Zero Datetime=True;SslMode=Disabled";
                connection = new MySqlConnection(connectionString);
                connection.Open();
                connection.Close();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }

        }
        

    }
}
