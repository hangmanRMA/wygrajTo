using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Allegro.Utility;
using Allegro.Models;

namespace Allegro.Repositories
{
    public class LoggerRepository
    {
        const int LogIdIndex = 0, UserEmailIndex = 1, MessageIndex = 2, StackTraceIndex = 3;
        SqlConnection connection;

        public LoggerRepository()
        {
            connection = ConnectionPool.Instance.Connection;
        }

        public List<ErrorLog> GetErrorLogs(string userEmail)
        {
            string commandString = "SELECT * FROM Log WHERE UserEmail=@email";
            DataTable table = new DataTable();
            SqlCommand command = new SqlCommand(commandString, connection);
            command.Parameters.AddWithValue("@email", userEmail);
            connection.Open();
            var reader = command.ExecuteReader();

            List<ErrorLog> list = new List<ErrorLog>();
            using (reader)
            {
                while (reader.Read())
                {
                    var log = new ErrorLog();
                    log.LogId = (int)reader[LogIdIndex];
                    log.UserEmail = (string)reader[UserEmailIndex];
                    log.Message = (string)reader[MessageIndex];
                    log.StackTrace = (string)reader[StackTraceIndex];

                    list.Add(log);
                }
                connection.Close();
                return list;
            }
            
        }

        public void InsertErrorLog(ErrorLog log)
        {
            string commandString = "INSERT INTO Log VALUES(@userEmail,@message,@stackTrace)";
            SqlCommand command = new SqlCommand(commandString, connection);

            command.Parameters.AddWithValue("@message", log.Message);
            command.Parameters.AddWithValue("@stackTrace", log.StackTrace);
            if (log.UserEmail == null)
            {
                command.Parameters.AddWithValue("@userEmail", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@userEmail", log.UserEmail);
            }

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
