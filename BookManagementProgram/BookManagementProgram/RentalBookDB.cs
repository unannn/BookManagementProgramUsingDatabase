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
        public void InitializeCustomerRentalBook(CustomerInformationVO logInCustomer)
        {
            connection.Open();

            string selectQuery = "SELECT book.no, title, author, publisher, quantity, maxQuntity FROM rentalInfo LEFT JOIN book ON rentalinfo.book_no = book.no WHERE rentalinfo.customer_no = "+logInCustomer.No;
            MySqlCommand command = new MySqlCommand(selectQuery, connection);
            MySqlDataReader books = command.ExecuteReader();


            while (books.Read())
            {
                logInCustomer.RentedBook.Add(new BookInformationVO(int.Parse(books["no"].ToString()), books["title"].ToString(), books["author"].ToString(), books["publisher"].ToString(), int.Parse(books["quantity"].ToString()), int.Parse(books["maxQuntity"].ToString())));
            }

            connection.Close();            
        }

        public void InsertRentalBookInfo(int customerNumber,int bookNumber)
        {
            connection.Open();

            string insertQuery = "INSERT INTO rentalinfo(customer_no,book_no,rental_date,return_date) VALUES("+customerNumber+","+bookNumber + ",CURDATE(),DATE_ADD(CURDATE(),INTERVAL 7 day))";
            MySqlCommand command = new MySqlCommand(insertQuery, connection);
            command.ExecuteNonQuery();

            connection.Close();
        }

        public void DeleteRentalBookInfo(int customerNumber, int bookNumber)
        {
            connection.Open();

            string deleteQuery = "DELETE FROM rentalinfo WHERE customer_no = " +customerNumber + " AND book_no = " + bookNumber;
            MySqlCommand command = new MySqlCommand(deleteQuery, connection);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
