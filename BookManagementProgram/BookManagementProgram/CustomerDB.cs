using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BookManagementProgram
{
    class CustomerDB
    {
        private static CustomerDB instance = null;
        private static MySqlConnection connection;

        public static CustomerDB Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CustomerDB();
                }
                return instance;
            }
        }

        CustomerDB()
        {
            connection = new MySqlConnection("Server=localhost;Port=3306;Database=library;Uid=root;Pwd=0000");
        }

        public int InsertNewCustomer(string id,string password, string name, string PhoneNumber,string adress)
        {
            int no;

            connection.Open();

            string insertQuery = "INSERT INTO customer(id, password, name, phoneNumber, adress, administrator)"
                +"VALUES(\"" + id + "\",\"" + password + "\",\"" + name + "\",\"" + PhoneNumber + "\",\"" + adress + "\",0)";
            string selectQuery = "SELECT LAST_INSERT_ID()";   //방금 등록한 회원의 primary key 를 가져옴

            MySqlCommand command = new MySqlCommand(insertQuery, connection);

            command.ExecuteNonQuery(); 

            command = new MySqlCommand(selectQuery, connection);

            no = int.Parse(command.ExecuteScalar().ToString());

            connection.Close();

            return no;
        }

        public void DeleteCusomter(int inputNumber)
        {
            connection.Open();

            string deleteQuery = "DELETE FROM customer WHERE no = " + inputNumber;

            MySqlCommand command = new MySqlCommand(deleteQuery, connection);

            command.ExecuteNonQuery();

            connection.Close();

        }

        public void UpdateMyPhoneNumber(int customerNumber, string modifiedPhoneNumber)
        {
            connection.Open();
            
            string updateQuery = "UPDATE customer SET phoneNumber = \""+modifiedPhoneNumber+"\" WHERE customer.no = " + customerNumber;

            MySqlCommand command = new MySqlCommand(updateQuery, connection);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public void UpdateMyAdress(int customerNumber, string modifiedAdress)
        {
            connection.Open();

            string updateQuery = "UPDATE customer SET adress = \"" + modifiedAdress + "\" WHERE customer.no = " + customerNumber;

            MySqlCommand command = new MySqlCommand(updateQuery, connection);

            command.ExecuteNonQuery();

            connection.Close();

        }
    }
}
