using System;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Experiment2_API.DB
{
    public class DbConnector : DbConnectorBase
    {
        private string _ConnectionString;
        private MySqlConnection _Connection;
        private int _Result;

        public int ExecuteNonQueryScript(string SqlScript)
        {
            InitializeConnection();
            try
            {
                Console.WriteLine("Starting Db communication");
                _Connection.Open();
                var Command = new MySqlCommand(SqlScript, _Connection);
                _Result = Command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            _Connection.Close();
            Console.WriteLine("Connection closed successfully. Script was executed");
            return _Result;
        }

        public void InitializeConnection()
        {
            _ConnectionString = "server=" + server + ";user=" + user + ";database=" + database + ";port=" + port + ";password=" + password;
            _Connection = new MySqlConnection(_ConnectionString);
        }
    }
}