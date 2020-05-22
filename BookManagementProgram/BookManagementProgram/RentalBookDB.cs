using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BookManagementProgram
{
    class RentalBookDB
    {
        private static RentalBookDB instance = null;
        private static MySqlConnection connection;

        public static RentalBookDB Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RentalBookDB();
                }
                return instance;
            }
        }
        RentalBookDB()
        {
            connection = new MySqlConnection("Server=localhost;Port=3306;Database=library;Uid=root;Pwd=0000");
            connection.Open();
        }

        ~RentalBookDB()
        {
            connection.Close();
        }

        public void InsertRentalBookInfo(int customer_no)
        {

        }
    }
}
