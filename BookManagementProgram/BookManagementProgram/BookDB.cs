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
                
        public void SelectAllBooks(List<BookInformationVO> bookList)
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

        public int InsertNewBook(string name, string author, string publisher, int quantity)
        {
            int no;

            connection.Open();

            string insertQuery = "INSERT INTO book(title,author,publisher,quantity,maxQuntity) VALUES(\""
                + name + "\",\"" + author + "\",\"" + publisher + "\"," + quantity + "," + quantity + ")";
            string selectQuery = "SELECT LAST_INSERT_ID()";   //방금 등록한 도서의 primary key 를 가져옴

            MySqlCommand command = new MySqlCommand(insertQuery, connection);

            command.ExecuteNonQuery();

            command = new MySqlCommand(selectQuery, connection);

            no = int.Parse(command.ExecuteScalar().ToString());    

            connection.Close();

            return no;
        }

        public void DeleteBook(int inputNumber)
        {
            connection.Open();

            string deleteQuery = "DELETE FROM book WHERE no = " + inputNumber;

            MySqlCommand command = new MySqlCommand(deleteQuery, connection);

            command.ExecuteNonQuery();

            connection.Close();

        }
    }
}
