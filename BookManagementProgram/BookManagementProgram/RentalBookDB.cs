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
            
        }

        //~RentalBookDB()
        //{
        //    connection.Close();
        //}

        public void InsertRentalBookInfo(int customerNumber,int bookNumber)
        {
            connection.Open();

            string insertQuery = "INSERT INTO rentalinfo(customer_no,book_no,rental_date,return_date) VALUES("+customerNumber+","+bookNumber + ",CURDATE(),DATE_ADD(CURDATE(),INTERVAL 7 day))";
            MySqlCommand command = new MySqlCommand(insertQuery, connection);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
