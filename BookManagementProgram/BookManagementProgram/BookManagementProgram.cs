using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementProgram
{
    class BookManagementProgram
    {
        public void StartProgram()
        {
            Console.Title = "BookManagementProgram";

            List<BookInformationVO> bookList = new List<BookInformationVO>() {
                new BookInformationVO("투명 드래곤","이윤환","한빛미디어",5),
                new BookInformationVO("어쩌구일찐 짱","김일진","퍼스트북",2),
                new BookInformationVO("어쩌구일찐 꽝","김일진","퍼스트북",2),
                new BookInformationVO("자고싶다i want to sleep","어쩌구","퍼스트북",2),
                new BookInformationVO("123456789","김11","퍼스트북",2),

            };
            List<CustomerInformationVO> customerList = new List<CustomerInformationVO>()
            {
                new CustomerInformationVO("asdf","asdf","이윤환","01049075608","서울시 도봉구 방학3동 신동아아파트",true), //생성자 만들고 로그인 해보기
                new CustomerInformationVO("as","as","백종원","01012345608","서울시 도봉구 방학1동 신동아아파트",false)
            };
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
