using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BookManagementProgram
{
    class BookManagementProgram
    {
        public void StartProgram()
        {
            Console.Title = "BookManagementProgram";
            
            List<BookInformationVO> bookList = new List<BookInformationVO>();
            //List<CustomerInformationVO> customerList = new List<CustomerInformationVO>();
            CustomerInformationVO logInCustomer = null;
            UI ui = new UI();

            BookDB.Instance.SelectAllBooks(bookList);             //도서와 고객정보 db 에서 불러오기
            //CustomerDB.Instance.SelectAllCustomers(customerList);
           
            //foreach (CustomerInformationVO customer in customerList)  //회원들이 대여한 책들 리스트 초기화
            //{
            //    RentalBookDB.Instance.InitializeCustomerRentalBook(customer);
            //}
                                                 
            while (true)
            {
                logInCustomer = ui.StartInitScene();      //  로그인, 계정만들기, 게임종료 선택          
                         
                if(logInCustomer.IsAdministrator) ui.StartAdministratorScene(bookList, logInCustomer);  //관리자 아이디로 로그인시
                else ui.StartGeneralUserScene(bookList, logInCustomer);           //일반유저모드로 로그인시
            }           
        }
    }
}
