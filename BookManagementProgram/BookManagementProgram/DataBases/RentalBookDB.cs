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

        public void InitializeCustomerRentalBook(CustomerInformationVO customer)    //고객들의 대여 도서 초기화
        {
            connection.Open();

            string selectQuery = "SELECT book.no, title, author, publisher, quantity, maxQuntity FROM rentalInfo LEFT JOIN book ON rentalinfo.book_no = book.no WHERE rentalinfo.customer_no = "+ customer.No;
            MySqlCommand command = new MySqlCommand(selectQuery, connection);
            MySqlDataReader books = command.ExecuteReader();


            while (books.Read())
            {
                customer.RentedBook.Add(new BookInformationVO(int.Parse(books["no"].ToString()), books["title"].ToString(), books["author"].ToString(), books["publisher"].ToString(), int.Parse(books["quantity"].ToString()), int.Parse(books["maxQuntity"].ToString())));
            }

            connection.Close();            
        }

        public void InsertRentalBookInfo(int customerNumber,int bookNumber)         //어떤고객이 어떤 도서를 대여했는지 정보 추가
        {
            connection.Open();

            string insertQuery = "INSERT INTO rentalinfo(customer_no,book_no,rental_date,return_date) VALUES("+customerNumber+","+bookNumber + ",CURDATE(),DATE_ADD(CURDATE(),INTERVAL 7 day))";
            MySqlCommand command = new MySqlCommand(insertQuery, connection);
            command.ExecuteNonQuery();

            connection.Close();
        }

        public void DeleteRentalBookInfo(int customerNumber, int bookNumber)          //반납이 완료되면 대여 정보 삭제
        {
            connection.Open();

            string deleteQuery = "DELETE FROM rentalinfo WHERE customer_no = " +customerNumber + " AND book_no = " + bookNumber;
            MySqlCommand command = new MySqlCommand(deleteQuery, connection);
            command.ExecuteNonQuery();

            connection.Close();
        }

        public string[] SelectRentalAndReturnDate(int custmerNumber,int bookNumber)           //대여일과 반납일을 반환
        {
            string[] rentalAndReturnDate = new string[2];
            connection.Open();

            string selectQuery = "SELECT rental_date,return_date FROM rentalInfo WHERE customer_no = " + custmerNumber + " AND book_no = " + bookNumber;
            MySqlCommand command = new MySqlCommand(selectQuery, connection);
            MySqlDataReader date = command.ExecuteReader();

            date.Read();

            rentalAndReturnDate[0] = date["rental_date"].ToString();
            rentalAndReturnDate[1] = date["return_date"].ToString();
            
            connection.Close();

            return rentalAndReturnDate;      
        }
    }
}
