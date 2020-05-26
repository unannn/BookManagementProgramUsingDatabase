using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BookManagementProgram
{  
    class UIModule
    {
        protected void PrintTitle(int spaceNumber) //제목 출력
        {
            string bar = new string('-', 33);
            string whiteSpace = new string(' ', spaceNumber);

            Console.WriteLine("\n");
            Console.Write(whiteSpace);
            Console.WriteLine("+" +bar+"+");
            Console.Write(whiteSpace);
            Console.WriteLine("|     BOOK MANAGEMENT PROGRAM     |");
            Console.Write(whiteSpace);
            Console.WriteLine("+" + bar+ "+");
            Console.WriteLine();
        }

        protected void InputIdAndPassword(ref string id, ref string password,int inputInspection)//아이디와 비밀번호를 입력받음
        {
            string userInput = null;
            int userRequest;
            bool isEnd = false;

            if (inputInspection == ExceptionHandling.wrongInput)             //입력 오류 체크
            {
                Console.WriteLine("가입하지 않은 아이디 이거나, 잘못된 비밀번호입니다.");
                Console.Write("초기화면으로 돌아가시겠습니까?[y,n] ");

                while (!isEnd)
                {
                    userInput = Console.ReadLine();

                    userRequest = ExceptionHandling.Instance.GoPrivious(userInput);

                    switch (userRequest)
                    {
                        case Constants.YES:
                            id = null;
                            password = null;
                            Console.Clear();
                            return;

                        case Constants.NO:
                            isEnd = true;
                            break;

                        default:
                            break;
                    }                    
                }               
            }

            Console.SetCursorPosition(Constants.LOCATION_X, Constants.LOGIN_ID_LOCATION_Y);  //입력받을 때 커서위치 이동후 입력받음
            id = Console.ReadLine();
                       
            Console.SetCursorPosition(Constants.LOCATION_X, Constants.LOGIN_PASSWORD_LOCATION_Y);
            password = ExceptionHandling.Instance.InputPassword(null);                       
        }
              
        protected void PrintInputBox(string inputData)        //사각박스 출력
        {
            string loginBar = new String('-', 35);
            string whiteSpace = new String(' ', 50 );

            Console.WriteLine("  {0} ", inputData);
            Console.WriteLine(loginBar);
            Console.WriteLine(whiteSpace);
            Console.WriteLine(loginBar);
        }

        protected void PrintMenuList(string[] menuList) //매개변수로받은 문자열 리스트를 한줄씩 출력함
        {
            Console.WriteLine();
            foreach(string item in menuList)
            {
                Console.WriteLine(item + "\n");
            }
        }

        protected void OneSpace(string bookeInformation,int limit) //도서, 고객 목록 출력시 칸 정렬
        {
            int whiteSpace = limit - Encoding.Default.GetByteCount(bookeInformation);

            if (whiteSpace < 0) whiteSpace = 0;

            if (bookeInformation.Length * 2 > limit && Regex.IsMatch(bookeInformation,@"[a-zA-Z가-힣]")) bookeInformation = bookeInformation.Substring(0, limit/2) + "...";

            Console.Write(" " + bookeInformation);
            Console.Write(new String(' ', whiteSpace));  //한글영어숫자구분 위해 바이트단위로 계산
        }

        protected void PrintFailMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Press Any Key...");
            Console.ReadKey();
        }

        protected void MoveCursor()
        {
            string whiteSpace = new String(' ', 50);

            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop + 3);
            Console.Write(whiteSpace);
            Console.SetCursorPosition(Constants.LOCATION_X, Console.CursorTop);
        }

        protected void PrintBookList(List<BookInformationVO> bookList)
        {
            string divisionLine = new String('-', Constants.BOOK_TABLE_WIDTH) + "+";

            Console.WriteLine(divisionLine);

            OneSpace("NO", Constants.BOOK_NUMBER_MAXIMUM.ToString().Length);
            OneSpace("제목", Constants.BOOK_NAME_LENGTH_MAXIMUM);
            OneSpace("저자", Constants.BOOK_AUTHOER_LENGTH_MAXIMUM);
            OneSpace("출판사", Constants.BOOK_PUBLISHER_LENGTH_MAXIMUM);
            OneSpace("권수", Constants.BOOK_NUMBER_LENGTH_MAXIMUM);
            OneSpace("가격", Constants.BOOK_PRICE_MAXIMUM.ToString().Length);

            Console.WriteLine();

            for (int order = 0; order < bookList.Count; order++)
            {
                Console.WriteLine(divisionLine);

                OneSpace(bookList[order].No.ToString(), Constants.BOOK_NUMBER_MAXIMUM.ToString().Length);
                OneSpace(bookList[order].Name, Constants.BOOK_NAME_LENGTH_MAXIMUM);
                OneSpace(bookList[order].Author, Constants.BOOK_AUTHOER_LENGTH_MAXIMUM);
                OneSpace(bookList[order].Publisher, Constants.BOOK_PUBLISHER_LENGTH_MAXIMUM);
                OneSpace(bookList[order].Quantity.ToString(), Constants.BOOK_NUMBER_LENGTH_MAXIMUM);
                OneSpace(bookList[order].Price.ToString(), Constants.BOOK_PRICE_MAXIMUM.ToString().Length);

                Console.WriteLine();
            }

            Console.WriteLine(divisionLine);
        }

        protected void PrintMyInfo(CustomerInformationVO logInCustomer)
        {
            Console.WriteLine("          내 정보\n");
            Console.Write("\n    이름 : " + logInCustomer.Name);
            Console.WriteLine("  휴대폰번호 : " + logInCustomer.PhoneNumber);
            Console.WriteLine("\n    주소 : " + logInCustomer.Adress);
        }

        protected void PrintModifyingAdresss(CustomerInformationVO logInCustomer)
        {
            Console.WriteLine();
            Console.WriteLine("          현재 주소 : " + logInCustomer.Adress);
            Console.WriteLine();
            Console.Write("바꿀 주소(2~20글자) : ");
        }

        protected void PrintModifyingPhoneNumber(CustomerInformationVO logInCustomer)
        {
            Console.WriteLine();
            Console.WriteLine("    현재 번호 : " + logInCustomer.PhoneNumber);
            Console.WriteLine();
            Console.Write("    바꿀 번호 : ");
        }

        protected void PrintAllCustomer(List<CustomerInformationVO> customerList)
        {
            string divisionLine = new String('-', 123) + "+";

            Console.WriteLine(divisionLine);

            OneSpace(("번호").ToString(), 5);
            OneSpace("아이디", 23);
            OneSpace("이름", 23);
            OneSpace("휴대폰번호", 23);
            OneSpace("주소", 40);
            Console.WriteLine();

            for (int order = 0; order < customerList.Count; order++)  //고객 정보 리스트 출력
            {
                Console.WriteLine(divisionLine);

                OneSpace(customerList[order].No.ToString(), 5);
                OneSpace(customerList[order].Id, 23);
                OneSpace(customerList[order].Name, 23);
                OneSpace(customerList[order].PhoneNumber, 23);
                OneSpace(customerList[order].Adress, 40);

                Console.WriteLine();
            }

            Console.WriteLine(divisionLine);
        }

        protected void PrintBookListForReturn(CustomerInformationVO logInCustomer)  //대여일과 반납일 추가하기
        {
            string divisionLine = new String('-', Constants.RETURN_BOOK_TABLE_WIDTH) + "+";
            string[] rentalAndReturnDate;
            Console.WriteLine(divisionLine);

            OneSpace("NO", Constants.BOOK_NUMBER_MAXIMUM.ToString().Length);  //도서의 요소 종류 출력
            OneSpace("이름", Constants.BOOK_NAME_LENGTH_MAXIMUM);
            OneSpace("저자", Constants.BOOK_AUTHOER_LENGTH_MAXIMUM);
            OneSpace("출판사", Constants.BOOK_PUBLISHER_LENGTH_MAXIMUM);
            OneSpace("대여일", Constants.DATE_LENGTH_MAXIMUM);
            OneSpace("반납일", Constants.DATE_LENGTH_MAXIMUM);

            Console.WriteLine();

            for (int order = 0; order < logInCustomer.RentedBook.Count; order++)  //반납해야하는 도서리스트 출력
            {
                Console.WriteLine(divisionLine);

                OneSpace(logInCustomer.RentedBook[order].No.ToString(), Constants.BOOK_NUMBER_MAXIMUM.ToString().Length);
                OneSpace(logInCustomer.RentedBook[order].Name, Constants.BOOK_NAME_LENGTH_MAXIMUM);
                OneSpace(logInCustomer.RentedBook[order].Author, Constants.BOOK_AUTHOER_LENGTH_MAXIMUM);
                OneSpace(logInCustomer.RentedBook[order].Publisher, Constants.BOOK_PUBLISHER_LENGTH_MAXIMUM);
                rentalAndReturnDate = RentalBookDB.Instance.SelectRentalAndReturnDate(logInCustomer.No, logInCustomer.RentedBook[order].No);
                OneSpace(rentalAndReturnDate[0].Substring(0, Constants.DATE_LENGTH_MAXIMUM), Constants.DATE_LENGTH_MAXIMUM);
                OneSpace(rentalAndReturnDate[1].Substring(0, Constants.DATE_LENGTH_MAXIMUM), Constants.DATE_LENGTH_MAXIMUM);
                Console.WriteLine();
            }

            Console.WriteLine(divisionLine);
        }

        protected void PrintNaverBooks(List<BookInformationVO> naverBookList)  //대여일과 반납일 추가하기
        {
            string divisionLine = new String('-', 163) + "+";
            Console.WriteLine(divisionLine);

            OneSpace("NO", Constants.BOOK_NUMBER_MAXIMUM.ToString().Length);  //도서의 요소 종류 출력
            OneSpace("이름", Constants.BOOK_NAME_LENGTH_MAXIMUM + 27);
            OneSpace("저자", Constants.BOOK_AUTHOER_LENGTH_MAXIMUM);
            OneSpace("출판사", Constants.BOOK_PUBLISHER_LENGTH_MAXIMUM);
            OneSpace("출판일", 10);
            OneSpace("isbn번호", 13);
            OneSpace("가격", Constants.BOOK_PRICE_MAXIMUM.ToString().Length) ;

            Console.WriteLine();

            for (int order = 0; order < naverBookList.Count; order++)  //반납해야하는 도서리스트 출력
            {
                Console.WriteLine(divisionLine);

                OneSpace((order+1).ToString(), Constants.BOOK_NUMBER_MAXIMUM.ToString().Length);
                OneSpace(naverBookList[order].Name, Constants.BOOK_NAME_LENGTH_MAXIMUM + 27);
                OneSpace(naverBookList[order].Author, Constants.BOOK_AUTHOER_LENGTH_MAXIMUM);
                OneSpace(naverBookList[order].Publisher, Constants.BOOK_PUBLISHER_LENGTH_MAXIMUM);
                OneSpace(naverBookList[order].PubDate, 10);
                OneSpace(naverBookList[order].Isbn, 13);
                OneSpace(naverBookList[order].Price.ToString(), Constants.BOOK_PRICE_MAXIMUM.ToString().Length);

                Console.WriteLine();
                Console.WriteLine(divisionLine);

                OneSpace("description", Constants.BOOK_DESC_LENGTH_MAXIMUM);
                Console.WriteLine();

                Console.WriteLine(divisionLine);

                OneSpace(naverBookList[order].Description, Constants.BOOK_DESC_LENGTH_MAXIMUM);
                Console.WriteLine();


            }

            Console.WriteLine(divisionLine);
        }

        protected void PrintRentalBookMessage(int rentalControl)
        {
            switch (rentalControl)
            {
                case Constants.NOT_EXIST:
                    PrintFailMessage("해당 도서가 존재하지 않습니다.");
                    break;

                case Constants.OVER_MAXIMUM_RENTAL_BOOK:
                    PrintFailMessage("더이상 대여할 수 없습니다(최대 5권 대여가능).");
                    break;

                case Constants.ALEADY_RENTAL_BOOK:
                    PrintFailMessage("이미 대여한 도서입니다.");
                    break;

                case Constants.NO_OVERLEFT_BOOK:
                    PrintFailMessage("대여할 도서가 없습니다.");
                    break;

                case Constants.RENTAL_SUCESS:
                    PrintFailMessage("대여를 완료 했습니다.");
                    break;
            }
        }

        protected void PrintSearchingType(int inputNumber)
        {
            if (inputNumber < Constants.SEARCHING_BY_TITLE || inputNumber > Constants.SEARCHING_BY_PUBLISHER) return;
            string[] types = new string[] { "제목", "저자", "출판사" };
            Console.Write("도서 {0} 입력 : ", types[inputNumber - 1]);
            Console.Write("                  ");
            Console.SetCursorPosition(Console.CursorLeft - 18, Console.CursorTop);
        }
    }
}
