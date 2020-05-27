using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BookManagementProgram
{
    class LogDB
    {
        private static LogDB instance = null;
        private static MySqlConnection connection;

        public static LogDB Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogDB();
                }
                return instance;
            }
        }

        LogDB()  //생성자
        {
            connection = new MySqlConnection("Server=localhost;Port=3306;Database=library;Uid=root;Pwd=0000");
        }

        public void InsertLog(string log)         //어떤고객이 어떤 도서를 대여했는지 정보 추가
        {
            connection.Open();

            string insertQuery = "INSERT INTO log(log) VALUES(\""+log+"\")";
            MySqlCommand command = new MySqlCommand(insertQuery, connection);
            command.ExecuteNonQuery();

            connection.Close();
        }

        public void InitializeLog()
        {
            connection.Open();

            string deleteQuery = "DELETE FROM log";
            MySqlCommand command = new MySqlCommand(deleteQuery, connection);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
