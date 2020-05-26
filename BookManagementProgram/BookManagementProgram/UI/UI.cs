using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BookManagementProgram
{
    class UI : UIModule
    {
        private Customer.Management customerManagement;
        private Book.Management bookManagement;
        private List<BookInformationVO> naverBookList;

        private string[] initialMenu;
        private string[] adminMainMenu;
        private string[] userMenu;
        private string[] bookMenu;
        private string[] modifyMenu;
        private string[] bookModifyMenu;
        public UI()
        {
            customerManagement = new Customer.Management();
            bookManagement = new Book.Management();
            naverBookList = new List<BookInformationVO>();

        initialMenu = new string[] {  "1. 로그인", "2. 회원가입", "3. 프로그램종료" };
            adminMainMenu = new string[]
            {
               "     1. 도서 리스트 보기/검색/대여",
               "     2. 도서 반납",
               "     3. 도서 등록",
               "     4. 네이버에서 검색-등록",
               "     5. 도서 삭제",
               "     6. 도서 수정",
               "     7. 회원 리스트 보기",
               "     8. 회원 삭제",
               "     9. 내 정보 수정",
               "     10. 로그아웃",
               "     11. 프로그램 종료"
            };
            userMenu = new string[]
            {
               "     1. 도서 리스트 보기/검색/대여",
               "     2. 도서 반납",
               "     3. 내 정보 수정",
               "     4. 로그아웃",
               "     5. 프로그램 종료"
            };
            bookMenu = new string[]
            {
               "     1. 도서 이름 검색",
               "     2. 도서 저자 검색",
               "     3. 도서 출판사 검색",
               "     4. 도서 대여",
               "     5. 나가기"
            };
            modifyMenu = new string[]
            {
                "    1. 휴대폰번호",
                "    2. 주소",
                "    3. 종료"
            };
            bookModifyMenu = new string[]
            {
                "    1. 도서 수량 수정",
                "    2. 도서 가격 수정",
                "    3. 종료"
            };
        }

        //초기화면
        public CustomerInformationVO StartInitScene()
        {
            int selectedNumber = 0;
            bool loginSucessful = false;
            CustomerInformationVO logInCustomer = null;
            CustomerInformationVO createdAccount;

            while (!loginSucessful)            //로그인 성공 시 까지 반복
            {
                Console.SetWindowSize(Constants.INIT_WIDTH, Constants.INIT_HEIGHT);

                selectedNumber = LogInOrCreateAccount();

                switch (selectedNumber)
                {
                    case Constants.LOGIN:
                        logInCustomer = LoginCustomer();
                        if (logInCustomer == null) continue;
                        loginSucessful = true;
                        break;

                    case Constants.CREATE_ACCOUNT:
                        Console.SetWindowSize(Constants.CREATE_ACCOUNT_WIDTH, Constants.CREATE_ACCOUNT_HEIGHT);
                        createdAccount = CreateCustomerAccount();
                        if (createdAccount == null) continue;  //뒤로가기
                        
                        break;

                    case Constants.PROGRAM_END:
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }
            }

            return logInCustomer;
        }

        public void StartAdministratorScene(List<BookInformationVO> bookList, CustomerInformationVO logInCustomer)
        {
            bool loginSucessful = true;
            int selectedNumber;
            List<CustomerInformationVO> customerList = new List<CustomerInformationVO>();
            CustomerDB.Instance.SelectAllCustomers(customerList);

            foreach (CustomerInformationVO customer in customerList)  //회원들이 대여한 책들 리스트 초기화
            {
                RentalBookDB.Instance.InitializeCustomerRentalBook(customer);
            }

            while (loginSucessful)
            {
                Console.Clear();
                Console.SetWindowSize(Constants.INIT_WIDTH, Constants.INIT_HEIGHT);

                selectedNumber = PrintAdministratorUserMenu();  //관리자 모드 실행

                switch (selectedNumber)
                {
                    case Constants.BOOK_SEARCHING_RENTAL:
                        Console.SetWindowSize(Constants.BASIC_WIDTH, Constants.BASIC_HEIGHT);
                        PrintAndSerchAndRentBook(bookList, logInCustomer);      //도서 출력, 검색, 대여                               
                        break;

                    case Constants.BOOK_RETURN:
                        Console.SetWindowSize(Constants.BASIC_WIDTH, Constants.BASIC_HEIGHT);
                        PrintBookReturn(logInCustomer, bookList);   //도서 반납
                        break;

                    case Constants.BOOK_REGISTRATION:             //도서 등록
                        ResisterBook(bookList);
                        break;

                    case Constants.SEARCHING_ON_NAVER:
                        SearchOnNaver(bookList);
                        break;

                    case Constants.BOOK_DELETE:               //도서 삭제
                        Console.SetWindowSize(Constants.BASIC_WIDTH, Constants.BASIC_HEIGHT);
                        DeleteBook(bookList);
                        break;

                    case Constants.BOOK_MODIFYING:               //도서 수정
                        Console.SetWindowSize(Constants.BASIC_WIDTH, Constants.BASIC_HEIGHT);
                        ModifyBook(bookList);
                        break;

                    case Constants.CUSOMTER_LIST:            //회원리스트 보기
                        Console.SetWindowSize(Constants.CUSTOMER_PROCESS_WIDTH, Constants.CUSTOMER_PROCESS_HEIGHT);
                        ShowCustomerList(customerList);
                        break;

                    case Constants.CUSTOMER_DELETE:           //회원 삭제
                        Console.SetWindowSize(Constants.CUSTOMER_PROCESS_WIDTH, Constants.CUSTOMER_PROCESS_HEIGHT);
                        DeleteCustomer(customerList);
                        break;

                    case Constants.MY_DATA_MODIFYING:           //내정보 수정
                        Console.SetWindowSize(Constants.BASIC_WIDTH, Constants.BASIC_HEIGHT);
                        ModifyMyData(logInCustomer);
                        break;

                    case Constants.LOGOUT:           //로그아웃
                        loginSucessful = false;
                        break;

                    case Constants.PROGRAM_END_ADMIN:           //프로그램 종료
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }
            }
        }

        public void StartGeneralUserScene(List<BookInformationVO> bookList, CustomerInformationVO logInCustomer)
        {
            bool loginSucessful = true;
            int selectedNumber;

            while (loginSucessful)
            {
                Console.SetWindowSize(Constants.INIT_WIDTH, Constants.INIT_HEIGHT);

                selectedNumber = PrintUserMenu();  //관리자 모드 실행

                switch (selectedNumber)
                {
                    case Constants.BOOK_SEARCHING_RENTAL:
                        Console.SetWindowSize(Constants.BASIC_WIDTH, Constants.BASIC_HEIGHT);
                        PrintAndSerchAndRentBook(bookList, logInCustomer);
                        break;

                    case Constants.BOOK_RETURN:
                        Console.SetWindowSize(Constants.BASIC_WIDTH, Constants.BASIC_HEIGHT);
                        PrintBookReturn(logInCustomer, bookList);
                        break;

                    case Constants.MY_DATA_MODIFYING_USER:           //내정보 수정
                        Console.SetWindowSize(Constants.BASIC_WIDTH, Constants.BASIC_HEIGHT);
                        ModifyMyData(logInCustomer);
                        break;

                    case Constants.LOGOUT_USER:           //로그아웃
                        loginSucessful = false;
                        break;

                    case Constants.PROGRAM_END_USER:           //프로그램 종료
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }
            }
        }

        private int LogInOrCreateAccount()  //로그인이나 아이디 생성을 위한 초기 화면 출력
        {
            string inputNumberInString = null;
            int inputNumber = -2;
           
            while (inputNumber < 0) // 올바른 입력시 탈출
            {
                Console.Clear();

                PrintTitle(Constants.BASIC_LOCATION);

                PrintMenuList(initialMenu);
                if (inputNumber == ExceptionHandling.wrongInput) Console.Write("\n다시 입력해 주세요.");

                Console.Write("선택 메뉴 번호 입력 : ");

                inputNumberInString = Console.ReadLine();

                inputNumber = ExceptionHandling.Instance.InputNumber(Constants.STARTING_NUMBER, initialMenu.Length, inputNumberInString);  //정수입력

                Console.Clear();
            }

            return inputNumber;
        }

        private CustomerInformationVO LoginCustomer()  //로그인 함수
        {
            string id = null;
            string password = null;
            CustomerInformationVO logInCustomer = null;
            int inputInspection = 0;

            while (logInCustomer == null)
            {
                PrintTitle(Constants.BASIC_LOCATION);

                PrintInputBox("아이디");

                PrintInputBox("비밀번호");

                InputIdAndPassword(ref id, ref password, inputInspection);    //아이디와 비밀번호 입력

                if (id == null && password == null) return null;             //y 입력

                logInCustomer = CustomerDB.Instance.SelectLoginCustomer(id, password, logInCustomer); //입력한 아이디비번의 계정이 있는지 디비에서 검색

                if(logInCustomer != null)RentalBookDB.Instance.InitializeCustomerRentalBook(logInCustomer);

                inputInspection = -1;
                Console.Clear();
            }

            return logInCustomer;
        }

        private CustomerInformationVO CreateCustomerAccount()  //계정생성
        {
            CustomerInformationVO customerToBeAdded = new CustomerInformationVO();

            Console.Clear();

            PrintTitle(Constants.BASIC_LOCATION);

            Console.WriteLine("q입력 시 나가기\n");

            PrintInputBox("아이디 (특수문자 없이 2~11글자)");

            PrintInputBox("비밀번호 (특수문자 없이 2~11글자)");
            
            PrintInputBox("비밀번호확인 (2~11글자)");

            PrintInputBox("이름 (1~20글자)");

            PrintInputBox("휴대폰번호 ('-'없이 11글자)");

            PrintInputBox("주소 (1~20글자 ○○시 ○○구 ○○○ 형식)");

            customerToBeAdded = customerManagement.InputCustomerAccountInformation();

            return customerToBeAdded;
        }

        private int PrintAdministratorUserMenu()  //관리자 모드일 떄 메뉴 화면
        {              
            int inputNumber = ExceptionHandling.wrongInput;  //inputNumber 초기화
            string inputNumberInString = null;

            while (inputNumber == ExceptionHandling.wrongInput)
            {
                PrintTitle(Constants.BASIC_LOCATION);

                Console.WriteLine("관리자 모드 입니다.\n");

                PrintMenuList(adminMainMenu);

                Console.Write("1 ~ 9입력 : ");

                inputNumberInString = Console.ReadLine();
                inputNumber = ExceptionHandling.Instance.InputNumber(Constants.STARTING_NUMBER, adminMainMenu.Length, inputNumberInString);

                Console.Clear();
            }

            return inputNumber;
        }

        private int PrintUserMenu()
        {          
            int inputNumber = ExceptionHandling.wrongInput;  //inputNumber 초기화
            string inputNumberInString = null;

            while (inputNumber == ExceptionHandling.wrongInput)
            {
                PrintTitle(Constants.BASIC_LOCATION);

                PrintMenuList(userMenu);

                Console.Write("1 ~ 5입력 : ");

                inputNumberInString = Console.ReadLine();
                inputNumber = ExceptionHandling.Instance.InputNumber(Constants.STARTING_NUMBER, userMenu.Length, inputNumberInString);

                Console.Clear();
            }

            return inputNumber;
        }

        private void PrintAndSerchAndRentBook(List<BookInformationVO> bookList, CustomerInformationVO logInCustomer)  //도서 리스트 출력후 검색 또는 대여 화면
        {
            List<BookInformationVO> serchingBookList = new List<BookInformationVO>();
            int rentalControl;
            int inputNumber = 0;  //inputNumber 초기화
            string inputNumberInString = null;
            bool isEnd = false;

            while (!isEnd)
            {
                PrintTitle(Constants.BOOK_RENT_LOCATION);

                PrintBookList(bookList);

                PrintMenuList(bookMenu);

                Console.Write("1~6 정수 입력 : ");

                if (inputNumber == ExceptionHandling.wrongInput)
                {
                    Console.Write("\n다시 입력해 주세요");
                    Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop - 1);
                }

                inputNumberInString = Console.ReadLine();
                inputNumber = ExceptionHandling.Instance.InputNumber(Constants.STARTING_NUMBER, bookMenu.Length, inputNumberInString);  //메뉴선택

                if (inputNumber == ExceptionHandling.wrongInput)
                {
                    Console.Clear();
                    continue;
                }

                PrintSearchingType(inputNumber);

                switch (inputNumber)
                {
                    case Constants.SEARCHING_BY_TITLE:       //이름으로 검색
                        serchingBookList = bookManagement.SerchByTitle(bookList);
                        CheckBookExist(logInCustomer, serchingBookList, bookManagement);
                        break;

                    case Constants.SEARCHING_BY_AUTHOER:        //저자로 검색
                        serchingBookList = bookManagement.SerchByAuthor(bookList);
                        CheckBookExist(logInCustomer, serchingBookList, bookManagement);
                        break;

                    case Constants.SEARCHING_BY_PUBLISHER:  //출판사로 검색
                        serchingBookList = bookManagement.SerchedByPublisher(bookList);
                        CheckBookExist(logInCustomer, serchingBookList, bookManagement);

                        break;

                    case Constants.RENT_BOOK:    //도서대여
                        Console.Write("대여할 책 번호 입력 : ");
                        rentalControl = bookManagement.RentBook(logInCustomer, bookList);
                        PrintRentalBookMessage(rentalControl);
                        break;
                   
                    case Constants.EXIT_SEARCHING:
                        isEnd = true;
                        break;

                    default:
                        break;
                }

                Console.Clear();
            }
        }
        private void SearchOnNaver(List<BookInformationVO> bookList)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            string bookTitle;
            bool isEnd = false;
            int searchingNumber;
            string SearchingNumberInString;
            int bookNumber;
            string bookNumberInString;
            while (!isEnd)
            { 
                Console.Clear();

                PrintTitle(Console.LargestWindowWidth/2 - 20);
                Console.WriteLine("네이버에서 검색");

                Console.WriteLine("\nq입력시 나가기\n");

                Console.Write("도서 제목(두글자 이상 입력) : ");
                bookTitle = ExceptionHandling.Instance.InputString(Constants.STARTING_NUMBER, Constants.BOOK_NAME_LENGTH_MAXIMUM);
                if (bookTitle == "q") return;
                Console.Write("검색 개수 : ");
                SearchingNumberInString = Console.ReadLine();
                if (SearchingNumberInString == "q") return;
                searchingNumber = ExceptionHandling.Instance.InputNumber(Constants.STARTING_NUMBER, Constants.NABER_SEARCHING_MAXNUMBER, SearchingNumberInString);

                if (searchingNumber == ExceptionHandling.wrongInput)
                {
                    PrintFailMessage("다시입력해주세요.");
                    continue;
                }

                Book.NaverAPI.Instance.SearchBooks(naverBookList, bookTitle, searchingNumber);
                PrintNaverBooks(naverBookList);
                
                Console.Write("\n\n추가할 도서 번호 입력 : ");
                SearchingNumberInString = Console.ReadLine();
                if (SearchingNumberInString == "q") return;
                searchingNumber = ExceptionHandling.Instance.InputNumber(Constants.STARTING_NUMBER, naverBookList.Count, SearchingNumberInString);

                if(searchingNumber == ExceptionHandling.wrongInput)
                {
                    PrintFailMessage("존재하지않는 도서입니다.");
                    continue;
                }

                Console.Write("\n추가할 도서 개수 입력(최대 10권) : ");
                bookNumberInString = Console.ReadLine();
                if (bookNumberInString == "q") return;
                bookNumber = ExceptionHandling.Instance.InputNumber(Constants.STARTING_NUMBER, Constants.BOOK_QUANTITY_MAXIMUM, SearchingNumberInString);

                if (bookNumber == ExceptionHandling.wrongInput)
                {
                    PrintFailMessage("잘못된 입력 입니다.");
                    continue;
                }

                naverBookList[searchingNumber-1].Quantity = bookNumber;           //도서추가
                naverBookList[searchingNumber-1].MaxQuantity = bookNumber;

                //데베에추가
                naverBookList[searchingNumber-1].No = BookDB.Instance.AddNaverBook(naverBookList[searchingNumber-1]);
                bookList.Add(naverBookList[searchingNumber-1]);
            }
        }
        //해당 도서의 존재유무 검사
        private void CheckBookExist(CustomerInformationVO logInCustomer, List<BookInformationVO> serchingBookList, Book.Management bookManageMent)
        {
            int rentalControl;
            Console.Clear();
            PrintTitle(Constants.BOOK_RENT_LOCATION);

            if (serchingBookList.Count == 0)
            {
                PrintFailMessage("해당 도서가 존재하지 않습니다");
                return;
            }

            PrintBookList(serchingBookList);
            Console.Write("대여할 책 번호 입력 : ");
            rentalControl = bookManageMent.RentBook(logInCustomer, serchingBookList);
            PrintRentalBookMessage(rentalControl);

        }

        private void PrintBookReturn(CustomerInformationVO logInCustomer, List<BookInformationVO> bookList)   //대여한 도서를 반납하는함수
        {
            string inputNumberInString;
            int inputNumber = 0;
            bool isEnd = false;
            string confirmationMessage = null;
            int bookIndex;

            while (!isEnd)
            {
                Console.Clear();

                PrintTitle(Constants.BOOK_RENT_LOCATION);

                if (logInCustomer.RentedBook.Count == 0)   //대여한 도서가 없을경우 메소드 종료
                {
                    PrintFailMessage("대여한 도서가 없습니다.");

                    Console.Clear();
                    return;
                }

                Console.WriteLine("대여중인 도서 목록\n");
                Console.WriteLine("q입력시 나가기\n");

                PrintBookListForReturn(logInCustomer);

                Console.Write("반납 할 책 번호 입력 : ");
                
                if (inputNumber == ExceptionHandling.wrongInput)      //잘못된 입력시
                {
                    Console.WriteLine();
                    PrintFailMessage("선택한 도서가 없습니다.");
                    Console.Clear();

                    break;
                }

                inputNumberInString = Console.ReadLine();
                if (inputNumberInString == "q") return;

                inputNumber = ExceptionHandling.Instance.InputNumber(Constants.STARTING_NUMBER, Constants.BOOK_NUMBER_MAXIMUM, inputNumberInString);

                for (bookIndex = 0; bookIndex < logInCustomer.RentedBook.Count; bookIndex++)   //해당도서가 존재하는지 조사
                {
                    if (logInCustomer.RentedBook[bookIndex].No == inputNumber) break;
                }

                if (bookIndex == logInCustomer.RentedBook.Count)
                {
                    PrintFailMessage("해당 도서가 존재하지 않습니다.");
                    Console.Clear();
                    return;
                }

                if (inputNumber == ExceptionHandling.wrongInput) PrintFailMessage("해당 도서가 존재하지 않습니다.");

                while (true)
                {
                    Console.Write("정말로 반납 하시겠습니까?[y,n]");
                    confirmationMessage = ExceptionHandling.Instance.InputString(1, 1);

                    if (confirmationMessage != "y" && confirmationMessage != "n")
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write(new string(' ', 80));
                        Console.SetCursorPosition(0, Console.CursorTop);

                        continue;
                    }
                    break;
                }

                if (confirmationMessage == "n") continue;

                foreach (BookInformationVO book in bookList)
                {
                    if (book.No == inputNumber)
                    {
                        book.Quantity += 1;    //같은 제목의 책을 리스트에찾아 반납
                        BookDB.Instance.UpdateReturnBook(inputNumber);
                        for (int rentalIndex = 0; rentalIndex < logInCustomer.RentedBook.Count; rentalIndex++) //대여한 도서 목록에서 삭제
                        {
                            if (logInCustomer.RentedBook[rentalIndex].No == book.No)
                            {
                                logInCustomer.RentedBook.RemoveAt(rentalIndex);
                                break;
                            }
                        }
                        RentalBookDB.Instance.DeleteRentalBookInfo(logInCustomer.No, book.No); //도서대여정보 삭제
                        PrintFailMessage("반납이 완료 됐습니다.");
                        Console.Clear();

                        break;
                    }
                }
            }
        }

        private void ModifyMyData(CustomerInformationVO logInCustomer)   //고객의 개인정보 수정
        {
            bool isEnd = false;
            bool isSucessful = false;
            int inputNumber = 0;
            string inputNumberInString = null;
          
            while (!isEnd)
            {
                Console.Clear();

                PrintTitle(Constants.MODIFY_MYDATA_LOCATION);
                
                PrintMyInfo(logInCustomer);

                PrintMenuList(modifyMenu);

                Console.Write("\n    1~3정수 입력 : ");

                inputNumberInString = Console.ReadLine();
                inputNumber = ExceptionHandling.Instance.InputNumber(Constants.STARTING_NUMBER, modifyMenu.Length, inputNumberInString);

                if (inputNumber != ExceptionHandling.wrongInput)
                {
                    switch (inputNumber)
                    {
                        case Constants.MODIFYING_PHONENUMBER:
                            PrintModifyingAdresss(logInCustomer);
                            isSucessful = customerManagement.ModifyPhoneNumber(logInCustomer);
                            break;

                        case Constants.MODIFYING_ADRESS:
                            PrintModifyingPhoneNumber(logInCustomer);
                            isSucessful = customerManagement.ModifyAdress(logInCustomer);
                            break;

                        case Constants.MODIFYING_EXIT:
                            isEnd = true;
                            break;

                        default:
                            break;
                    }
                    
                    Console.Clear();

                }
            }
        }

        private void ResisterBook(List<BookInformationVO> bookList)   //책 등록, 반복되는 코드 함수화 필요
        {
            bool isEnd = false;
            BookInformationVO newBook = new BookInformationVO();

            string name, author, publisher, quantityInString, priceString;
            int quantity, price;

            while (!isEnd)
            {
                Console.Clear();

                PrintTitle(Constants.BASIC_LOCATION);

                Console.WriteLine("\n      새 도서 등록\n");
                //책정보입력
                Console.WriteLine();
                Console.WriteLine("q 입력시 도서등록 종료");

                Console.WriteLine();
                Console.Write("(20글자까지)도서 이름 : ");
                name = ExceptionHandling.Instance.InputString(Constants.STARTING_NUMBER, Constants.BOOK_NAME_LENGTH_MAXIMUM);
                if (name == null)
                {
                    PrintFailMessage("잘못된 입력 입니다.");

                    continue;
                }
                else if (name == "q")
                {
                    break;
                }

                Console.WriteLine();
                Console.Write("(15글자까지)도서 저자 : ");
                author = ExceptionHandling.Instance.InputString(Constants.STARTING_NUMBER, Constants.BOOK_AUTHOER_LENGTH_MAXIMUM);

                if (author == null)
                {
                    PrintFailMessage("잘못된 입력 입니다.");

                    continue;
                }
                else if (author == "q")
                {
                    break;
                }

                Console.WriteLine();
                Console.Write("(10글자까지)도서 출판사 : ");
                publisher = ExceptionHandling.Instance.InputString(Constants.STARTING_NUMBER, Constants.BOOK_PUBLISHER_LENGTH_MAXIMUM);
                if (publisher == null)
                {
                    PrintFailMessage("잘못된 입력 입니다.");

                    continue;
                }
                else if (publisher == "q")
                {
                    break;
                }

                Console.WriteLine();
                Console.Write("(10권까지)권수 : ");
                quantityInString = Console.ReadLine();
                if (quantityInString == "q") break;

                quantity = ExceptionHandling.Instance.InputNumber(Constants.STARTING_NUMBER, Constants.BOOK_QUANTITY_MAXIMUM, quantityInString);

                if (quantity == ExceptionHandling.wrongInput)
                {
                    PrintFailMessage("잘못된 입력 입니다.");

                    continue;
                }

                Console.WriteLine();
                Console.Write("(권당 1000 ~ 99999)가격 : ");
                priceString = Console.ReadLine();
                if (quantityInString == "q") break;

                price = ExceptionHandling.Instance.InputNumber(Constants.BOOK_PRICE_MINIMUM, Constants.BOOK_PRICE_MAXIMUM, priceString);

                if (quantity == ExceptionHandling.wrongInput)
                {
                    PrintFailMessage("잘못된 입력 입니다.");

                    continue;
                }
                
                newBook.Name = name;
                newBook.Author = author;
                newBook.Publisher = publisher;
                newBook.Quantity = quantity;
                newBook.MaxQuantity = quantity;
                newBook.Price = price;

                newBook.No = BookDB.Instance.InsertNewBook(name, author, publisher, quantity, price);  //데베에 도서정보 삽입
                bookList.Add(newBook);
                Console.WriteLine();

                PrintFailMessage("도서가 등록 됐습니다.");

                Console.Clear();
                isEnd = true;
            }
        }

        private void DeleteBook(List<BookInformationVO> bookList) //등록되어있는 책들 삭제하는 함수
        {
            string inpuNumberInString;
            int inputNumber;
            string confirmationMessage = null;
            int bookIndex;
            bool isEnd = false;

            while (!isEnd)
            {
                Console.Clear();

                PrintTitle(Constants.BOOK_RENT_LOCATION);

                Console.WriteLine();
                Console.WriteLine("    도서 삭제\n");
                Console.WriteLine("q입력시 나가기\n");

                PrintBookList(bookList);

                Console.Write(" 삭제할 도서 번호 입력 : ");
                inpuNumberInString = Console.ReadLine();
                if (inpuNumberInString == "q") return;            //q입력시 종료
                inputNumber = ExceptionHandling.Instance.InputNumber(Constants.STARTING_NUMBER, Constants.BOOK_NUMBER_MAXIMUM, inpuNumberInString);

                for (bookIndex = 0; bookIndex < bookList.Count; bookIndex++)  //해당 도서가 있는지 검사
                {
                    if (bookList[bookIndex].No == inputNumber) break;
                }

                if (bookIndex == bookList.Count)
                {
                    PrintFailMessage("해당 도서가 존재하지 않습니다.");
                    continue;
                }

                while (true)
                {
                    Console.Write("정말로 삭제 하시겠습니까?[y,n]");
                    confirmationMessage = ExceptionHandling.Instance.InputString(1, 1);

                    if (confirmationMessage != "y" && confirmationMessage != "n")
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write(new string(' ', 76));
                        Console.SetCursorPosition(0, Console.CursorTop);

                        continue;
                    }

                    break;
                }

                if (confirmationMessage == "n") continue;

                if (inputNumber == ExceptionHandling.wrongInput)
                {
                    PrintFailMessage("해당 도서가 존재하지 않습니다.");
                    continue;
                }

                bookIndex = 0;

                foreach (BookInformationVO book in bookList)
                {
                    if (book.No == inputNumber)
                    {
                        if (book.Quantity != book.MaxQuantity)
                        {
                            PrintFailMessage("반납되지 않은 도서가 있습니다.");
                            Console.Clear();
                            break;
                        }


                        bookList.RemoveAt(bookIndex);
                        Console.WriteLine();
                        BookDB.Instance.DeleteBook(inputNumber);
                        PrintFailMessage("해당 도서가 삭제됐습니다.");

                        Console.Clear();
                        break;

                    }

                    ++bookIndex;
                }
            }
        }

        private void ModifyBook(List<BookInformationVO> bookList) //등록되어있는 책들 삭제하는 함수
        {
            string inpuNumberInString;
            int inputNumber;
            int bookIndex;
            bool isEnd = false;
            bool isSucessful = false;

            while (!isEnd)
            {
                Console.Clear();

                PrintTitle(Constants.BOOK_RENT_LOCATION);

                Console.WriteLine();
                Console.WriteLine("    도서 수정\n");
                Console.WriteLine("q입력시 나가기\n");

                PrintBookList(bookList);

                Console.Write(" 수정할 도서 번호 입력 : ");

                inpuNumberInString = Console.ReadLine();
                if (inpuNumberInString == "q") return;            //q입력시 종료
                inputNumber = ExceptionHandling.Instance.InputNumber(Constants.STARTING_NUMBER, Constants.BOOK_NUMBER_MAXIMUM, inpuNumberInString);
                
                if (inputNumber == ExceptionHandling.wrongInput)
                {
                    PrintFailMessage("해당 도서가 존재하지 않습니다.");
                    continue;
                }

                for (bookIndex = 0; bookIndex < bookList.Count; bookIndex++)  //해당 도서가 있는지 검사
                {
                    if (bookList[bookIndex].No == inputNumber) break;
                }

                if (bookIndex == bookList.Count)
                {
                    PrintFailMessage("해당 도서가 존재하지 않습니다.");
                    continue;
                }

                PrintMenuList(bookModifyMenu);
                Console.Write(" 수정할 정보 선택 : ");
                inpuNumberInString = Console.ReadLine();
                inputNumber = ExceptionHandling.Instance.InputNumber(Constants.STARTING_NUMBER, bookModifyMenu.Length, inpuNumberInString);

                switch (inputNumber)
                {
                    case Constants.BOOK_NUMBER_MODIFYING:
                        Console.WriteLine("선택 도서 총수량 : {0}",bookList[bookIndex].MaxQuantity);
                        Console.Write("수정할 도서 개수 입력(대여도서 포함) : ");
                        isSucessful =  bookManagement.ModifyBookNumber(bookList[bookIndex]);
                        if(isSucessful)PrintFailMessage("수정을 완료했습니다.");
                        else PrintFailMessage("잘못된 입력입니다.");
                        break;

                    case Constants.PRICE_MODIFYING:
                        Console.WriteLine("선택 도서 현재가격 : {0}", bookList[bookIndex].Price);
                        Console.Write("수정할 가격 입력(1000~99999) : ");
                        isSucessful = bookManagement.ModifyBookPrice(bookList[bookIndex]);
                        if (isSucessful) PrintFailMessage("수정을 완료했습니다.");
                        else PrintFailMessage("잘못된 입력입니다.");
                        break;

                    case Constants.MODIFYING_END:
                        break;

                    default:
                        break;
                }

            }
        }

        private void ShowCustomerList(List<CustomerInformationVO> customerList)
        {
            Console.Clear();

            PrintTitle(46);

            Console.WriteLine();
            Console.WriteLine("      회원 목록\n");

            PrintAllCustomer(customerList);

            Console.WriteLine();
            Console.WriteLine("Press Any Key...");
            Console.ReadKey();
            Console.Clear();

            Console.SetWindowSize(Constants.BASIC_WIDTH, Constants.BASIC_HEIGHT);
        }

        private void DeleteCustomer(List<CustomerInformationVO> customerList)  //계정삭제
        {
            string inputId = null;
            string message;
            PrintTitle(46);

            Console.WriteLine();

            PrintAllCustomer(customerList);

            Console.Write("      회원 아이디 입력 : ");
            inputId = ExceptionHandling.Instance.InputString(1, 11);

            if (inputId != null)
            {
                for (int customer = 0; customer < customerList.Count; customer++)
                {
                    if (customerList[customer].Id == inputId)
                    {
                        message = customerManagement.CheckId(customer, customerList);
                        PrintFailMessage(message);
                        Console.Clear();
                        return;
                    }
                }
            }

            PrintFailMessage("해당 아이디가 없습니다.");

            Console.Clear();
        }
    }
}
