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
        }


        //~BookDB()
        //{
        //    connection.Close();
        //}

        public MySqlDataReader SelectAllBooks(List<BookInformationVO> bookList)
        {
            connection.Open();

            string selectQuery = "SELECT * FROM book";
            MySqlCommand command = new MySqlCommand(selectQuery, connection);
            MySqlDataReader books = command.ExecuteReader();


            while (books.Read())
            {
                bookList.Add(new BookInformationVO(int.Parse(books["no"].ToString()), books["title"].ToString(), books["author"].ToString(), books["publisher"].ToString(), int.Parse(books["quantity"].ToString()), int.Parse(books["maxQuntity"].ToString())));
            }

            connection.Close();

            return books;
        }

        public int UpdateRentalBook(int inputNumber)
        {
            connection.Open();

            int rowNumber;
            string updateQuery = "UPDATE book SET quantity = quantity - 1 WHERE book.no = " + inputNumber;

            MySqlCommand command = new MySqlCommand(updateQuery, connection);

            rowNumber = command.ExecuteNonQuery();

            connection.Close();

            return rowNumber;  
        }

        public  int UpdateReturnBook(int inputNumber)
        {
            connection.Open();

            int rowNumber;
            string updateQuery = "UPDATE book SET quantity = quantity + 1 WHERE book.no = " + inputNumber;

            MySqlCommand command = new MySqlCommand(updateQuery, connection);

            rowNumber = command.ExecuteNonQuery();

            connection.Close();

            return rowNumber;
        }
    }
}
