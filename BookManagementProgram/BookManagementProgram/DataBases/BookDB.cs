﻿using System;
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
                
        public void SelectAllBooks(List<BookInformationVO> bookList)         //전체 도서 목록 가져옴
        {
            connection.Open();

            string selectQuery = "SELECT * FROM book";
            MySqlCommand command = new MySqlCommand(selectQuery, connection);
            MySqlDataReader books = command.ExecuteReader();


            while (books.Read())
            {
                bookList.Add(new BookInformationVO(int.Parse(books["no"].ToString()), books["title"].ToString(), books["author"].ToString(), books["publisher"].ToString(), int.Parse(books["quantity"].ToString()), int.Parse(books["maxQuntity"].ToString()), int.Parse(books["book_price"].ToString())));
            }

            connection.Close();
        }

        public int UpdateRentalBook(int inputNumber)     //도서를 대여하면 여분도서 정보 업데이트
        {
            connection.Open();

            int rowNumber;
            string updateQuery = "UPDATE book SET quantity = quantity - 1 WHERE book.no = " + inputNumber;

            MySqlCommand command = new MySqlCommand(updateQuery, connection);

            rowNumber = command.ExecuteNonQuery();

            connection.Close();

            return rowNumber;  
        }

        public  int UpdateReturnBook(int inputNumber)                //도서를 반납하면 여분도서 정보 업데이트
        {
            connection.Open();

            int rowNumber;
            string updateQuery = "UPDATE book SET quantity = quantity + 1 WHERE book.no = " + inputNumber;

            MySqlCommand command = new MySqlCommand(updateQuery, connection);

            rowNumber = command.ExecuteNonQuery();

            connection.Close();

            return rowNumber;
        }

        public void UpdateBookQuantity(BookInformationVO book)
        {
            connection.Open();

            string updateQuery = "UPDATE book SET maxQuntity = " + book.MaxQuantity +", Quantity = " + book.Quantity +
                " WHERE book.no = " + book.No;

            MySqlCommand command = new MySqlCommand(updateQuery, connection);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public void UpdateBookPrice(BookInformationVO book)
        {
            connection.Open();

            string updateQuery = "UPDATE book SET book_price = " + book.Price +
                " WHERE book.no = " + book.No;

            MySqlCommand command = new MySqlCommand(updateQuery, connection);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public int InsertNewBook(string name, string author, string publisher, int quantity, int price)         //새 도서 등록
        {
            int no;
            
            connection.Open();

            ExceptionHandling.Instance.MakeQuotesUse(ref name, ref author, ref publisher);

            string insertQuery = "INSERT INTO book(title,author,publisher,quantity,maxQuntity,book_price) VALUES(\""
                + name + "\",\"" + author + "\",\"" + publisher + "\"," + quantity + "," + quantity + "," + price + ")";
            string selectQuery = "SELECT LAST_INSERT_ID()";   //방금 등록한 도서의 primary key 를 가져옴

            MySqlCommand command = new MySqlCommand(insertQuery, connection);

            command.ExecuteNonQuery();

            command = new MySqlCommand(selectQuery, connection);

            no = int.Parse(command.ExecuteScalar().ToString());

            connection.Close();

            return no;
        }

        public void DeleteBook(int inputNumber)             //도서 삭제
        {
            connection.Open();

            string deleteQuery = "DELETE FROM book WHERE no = " + inputNumber;

            MySqlCommand command = new MySqlCommand(deleteQuery, connection);

            command.ExecuteNonQuery();

            connection.Close();

        }

        public int AddNaverBook(BookInformationVO naverBook)
        {
            int no;

            connection.Open();
            
            string insertQuery = "INSERT INTO book(title,author,publisher,quantity,maxQuntity,book_price,pubdate,isbn,description) VALUES(\""
                + naverBook.Name + "\",\"" +  naverBook.Author + "\",\"" +  naverBook.Publisher + "\"," + naverBook.Quantity + "," 
                + naverBook.MaxQuantity + "," + naverBook.Price+ ",\"" + naverBook.PubDate + "\",\"" + naverBook.Isbn + "\",\"" + naverBook.Description + "\")";
            string selectQuery = "SELECT LAST_INSERT_ID()";   //방금 등록한 도서의 primary key 를 가져옴

            MySqlCommand command = new MySqlCommand(insertQuery, connection);

            command.ExecuteNonQuery();

            command = new MySqlCommand(selectQuery, connection);

            no = int.Parse(command.ExecuteScalar().ToString());

            connection.Close();

            return no;
        }
    }
}
