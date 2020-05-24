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

        CustomerDB()  //생성자
        {
            connection = new MySqlConnection("Server=localhost;Port=3306;Database=library;Uid=root;Pwd=0000");
        }

        public void SelectAllCustomers(List<CustomerInformationVO> customerList)         //전체 도서 목록 가져옴
        {
            connection.Open();

            string selectQuery = "SELECT * FROM customer";
            MySqlCommand command = new MySqlCommand(selectQuery, connection);
            MySqlDataReader customers = command.ExecuteReader();

            while (customers.Read())
            {
                customerList.Add(new CustomerInformationVO(int.Parse(customers["no"].ToString()), customers["id"].ToString(), customers["password"].ToString(), customers["name"].ToString(), customers["phoneNumber"].ToString(), customers["adress"].ToString(), bool.Parse(customers["administrator"].ToString())));
            }

            connection.Close();
        }

        public int InsertNewCustomer(string id,string password, string name, string PhoneNumber,string adress)  //새 계정 생성
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

        public CustomerInformationVO SelectLoginCustomer(string id, string password, CustomerInformationVO logInCustomer)
        {
            connection.Open();

            string selectQuery = "SELECT * FROM customer WHERE id = '" +id + "' AND password = '" + password + "'";
            MySqlCommand command = new MySqlCommand(selectQuery, connection);
            MySqlDataReader customers = command.ExecuteReader();

            while (customers.Read())
            {
                logInCustomer = new CustomerInformationVO(int.Parse(customers["no"].ToString()), customers["id"].ToString(), customers["password"].ToString(), customers["name"].ToString(), customers["phoneNumber"].ToString(), customers["adress"].ToString(), bool.Parse(customers["administrator"].ToString()));
            }
            
            connection.Close();

            return logInCustomer;
        }

        public bool SelectSamePhoneNumber(string phoneNumber)
        {
            connection.Open();

            string selectQuery = "SELECT no FROM customer WHERE phoneNumber = '" + phoneNumber + "'";
            MySqlCommand command = new MySqlCommand(selectQuery, connection);
            MySqlDataReader customers = command.ExecuteReader();

            while (customers.Read())
            {
                connection.Close();
                return true;
            }

            connection.Close();

            return false;
        }

        public bool SelectSameId(string id)
        {
            connection.Open();

            string selectQuery = "SELECT no FROM customer WHERE id = '" + id + "'";
            MySqlCommand command = new MySqlCommand(selectQuery, connection);
            MySqlDataReader customers = command.ExecuteReader();

            while (customers.Read())
            {
                connection.Close();
                return true;
            }

            connection.Close();

            return false;
        }

        public void DeleteCusomter(int inputNumber)   //계정 삭제
        {
            connection.Open();

            string deleteQuery = "DELETE FROM customer WHERE no = " + inputNumber;

            MySqlCommand command = new MySqlCommand(deleteQuery, connection);

            command.ExecuteNonQuery();

            connection.Close();

        }

        public void UpdateMyPhoneNumber(int customerNumber, string modifiedPhoneNumber)       //폰번호 수정
        {
            connection.Open();
            
            string updateQuery = "UPDATE customer SET phoneNumber = \""+modifiedPhoneNumber+"\" WHERE customer.no = " + customerNumber;

            MySqlCommand command = new MySqlCommand(updateQuery, connection);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public void UpdateMyAdress(int customerNumber, string modifiedAdress)           //주소수정
        {
            connection.Open();

            string updateQuery = "UPDATE customer SET adress = \"" + modifiedAdress + "\" WHERE customer.no = " + customerNumber;

            MySqlCommand command = new MySqlCommand(updateQuery, connection);

            command.ExecuteNonQuery();

            connection.Close();

        }
    }
}
