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
            Console.SetWindowSize(90,36);

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
            CustomerInformationVO logInCustomer = null, createdAccount;
            UI ui = new UI();
            int selectedNumber = ExceptionHandling.wrongInput;
            bool loginSucessful = false;
            bool endOfProgram = false;
                        
            while (!endOfProgram)
            {
                while (!loginSucessful)            //로그인 성공 시 까지 반복
                {
                    Console.SetWindowSize(50, 30);

                    selectedNumber = ui.LogInOrCreateAccount();

                    if (selectedNumber == User.logIn)
                    {
                        Console.WriteLine(customerList[1].RentedBook.Count);
                        logInCustomer = ui.LoginCustomer(customerList); //뒤로가기

                        if (logInCustomer == null) continue;
                        else loginSucessful = true;
                    }
                    else if (selectedNumber == User.createAccount)
                    {
                        Console.SetWindowSize(50, 40);

                        createdAccount = ui.CreateCustomerAccount(customerList);

                        if (createdAccount == null) continue;  //뒤로가기

                        customerList.Add(createdAccount);
                    }
                    else if(selectedNumber == User.endProgram)
                    {
                        endOfProgram = true;
                        break;
                    }
                }

                if (endOfProgram == true) break;

                selectedNumber = ExceptionHandling.wrongInput; //seletedNumber 다시 초기화
                
                if(logInCustomer.IsAdministrator == true)
                {
                    while (loginSucessful)
                    {
                        Console.SetWindowSize(50, 30);

                        selectedNumber = ui.PrintAdministratorUserMenu();  //관리자 모드 실행

                        switch (selectedNumber)
                        {
                            case 1:       
                                Console.SetWindowSize(90,36);
                                ui.PrintAndSerchAndRentBook(bookList,logInCustomer);      //도서 출력, 검색, 대여                               
                                break;
                            case 2:
                                Console.SetWindowSize(90, 36);
                                ui.PrintBookReturn(logInCustomer, bookList);   //도서 반납
                                break;
                            case 3:              //도서 등록
                                ui.ResisterBook(bookList);
                                break;
                            case 4:               //도서 삭제
                                Console.SetWindowSize(90, 36);
                                ui.DeleteBook(bookList);
                                break;
                            case 5:            //회원리스트 보기
                                Console.SetWindowSize(130, 36);
                                ui.ShowCustomerList(customerList);
                                break;
                            case 6:           //회원 삭제
                                Console.SetWindowSize(130, 36);
                                ui.DeleteCustomer(customerList);
                                break;
                            case 7:           //내정보 수정
                                Console.SetWindowSize(90, 36);
                                ui.ModifyMyData(logInCustomer);
                                break;
                            case 8:           //로그아웃
                                loginSucessful = false;
                                break;
                            case 9:           //프로그램 종료
                                loginSucessful = false;
                                endOfProgram = true;
                                break;
                            default:
                                break;

                        }
                        
                    }
                }
                else
                {
                    while (loginSucessful)
                    {
                        Console.SetWindowSize(50, 30);

                        selectedNumber = ui.PrintUserMenu();  //일반사용자모드 실행

                        
                        switch (selectedNumber)
                        {
                            case 1:
                                Console.SetWindowSize(90, 36);
                                ui.PrintAndSerchAndRentBook(bookList, logInCustomer);
                               
                                break;
                            case 2:
                                Console.SetWindowSize(90, 36);
                                ui.PrintBookReturn(logInCustomer, bookList);
                                break;                            
                            case 3:           //내정보 수정
                                Console.SetWindowSize(90, 36);
                                ui.ModifyMyData(logInCustomer);
                                break;
                            case 4:           //로그아웃
                                loginSucessful = false;
                                break;
                            case 5:           //프로그램 종료
                                loginSucessful = false;
                                endOfProgram = true;
                                break;
                            default:
                                break;

                        }
                    }
                }
            }           
        }
    }
}
