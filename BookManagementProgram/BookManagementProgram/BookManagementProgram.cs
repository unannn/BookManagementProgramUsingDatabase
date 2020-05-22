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
            List<CustomerInformationVO> customerList = new List<CustomerInformationVO>();
            BookManagement bookManagement = new BookManagement();
            CustomerManagement customerMangement = new CustomerManagement();

            bookManagement.InitializeBookList(bookList);                  //도서와 고객정보 불러오기
            customerMangement.IntializeCustomerList(customerList);

            CustomerInformationVO logInCustomer = null;
            UI ui = new UI();
                                       
            while (true)
            {
                logInCustomer = ui.StartInitScene(customerList);      //  로그인, 계정만들기, 게임종료 선택          
                                                                   
                if(logInCustomer.IsAdministrator == true) ui.StartAdministratorScene(customerList, bookList, logInCustomer);  //관리자 아이디로 로그인시
                else ui.StartGeneralUserScene(customerList, bookList, logInCustomer);           //일반유저모드로 로그인시
            }           
        }
    }
}
