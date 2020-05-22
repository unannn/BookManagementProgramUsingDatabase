using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BookManagementProgram
{
    class BookDB
    {
        private static BookDB instance = null;
        private static MySqlConnection connection;
        
        public static BookDB Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new BookDB();
                }
                return instance;
            }
        }
        BookDB()
        {
            connection = new MySqlConnection("Server=localhost;Port=3306;Database=library;Uid=root;Pwd=0000");
            connection.Open();
        }


        ~BookDB()
        {
            connection.Close();
        }

        public MySqlDataReader SelectAllBooks()
        {
            string selectQuery = "SELECT * FROM book";
            MySqlCommand command = new MySqlCommand(selectQuery, connection);
            MySqlDataReader books = command.ExecuteReader();

            return books;
        }

        public int UpdateRentalBook(int inputNumber)
        {
            int rowNumber;
            string updateQuery = "UPDATE book SET quantity = quantity - 1 WHERE book.no = " + inputNumber;

            MySqlCommand command = new MySqlCommand(updateQuery, connection);

            rowNumber = command.ExecuteNonQuery();

            return rowNumber;  
        }


    }
}
