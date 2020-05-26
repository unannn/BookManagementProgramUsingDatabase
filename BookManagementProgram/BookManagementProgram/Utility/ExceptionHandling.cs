using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BookManagementProgram
{  
    class ExceptionHandling    //에러메세지 빼자.
    {
        private static ExceptionHandling instance = null;

        public static ExceptionHandling Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ExceptionHandling();
                }
                return instance;
            }
        }

        public const int wrongInput = -1;

        public int InputNumber(int start, int end, string numberInString) //여러자리 가능하지만 익셉션발생 수정해야함
        {
            int inputNumber = wrongInput;
            int number = 0;

            if (!string.IsNullOrEmpty(numberInString) && numberInString.Length < 10)
            {
                for (number = 0; number < numberInString.Length; number++)
                {
                    if (numberInString[number] < '0' || numberInString[number] > '9') break;
                }

                if (number == numberInString.Length && number != 0)
                {
                    inputNumber = int.Parse(numberInString);
                }
            }

            if (inputNumber < start || inputNumber > end) inputNumber = wrongInput;
                        
            return inputNumber;
        }

        //도서대여 예외처리
        public int RentBook(CustomerInformationVO logInCustomer, List<BookInformationVO> bookList, int inputNumber)
        {
            if (inputNumber == ExceptionHandling.wrongInput)  //음수나 최대 책 개수 이상 입력
            {
                return Constants.NOT_EXIST;
            }

            if (logInCustomer.RentedBook.Count >= Constants.RENT_BOOK_MAXIMUM)   //최대 대여가능 도서수 제한
            {
                return Constants.OVER_MAXIMUM_RENTAL_BOOK;
            }

            foreach (BookInformationVO rentedBooks in logInCustomer.RentedBook)  //중복대여 여부 검사
            {
                if (rentedBooks.No == inputNumber)
                {
                    return Constants.ALEADY_RENTAL_BOOK;
                }
            }

            foreach (BookInformationVO book in bookList)
            {
                if (book.No == inputNumber)
                {
                    if (book.Quantity < 1) //대여할 책이 남아있는지 검사
                    {
                        return Constants.NO_OVERLEFT_BOOK;
                    }                   
                }
            }



            return Constants.RENTAL_SUCESS;   //위 조건들을 모두 통과하면 대여
        }

        public string InputYesOrNo(string yesOrNo)   //문자열이 y 인지 n인지 아님 다른값이 들어왔는지 판단 후 반환
        {
            if (!string.IsNullOrEmpty(yesOrNo) && yesOrNo.Length == 1)
            {
                if (string.Compare(yesOrNo, "y") == 0)
                {
                    return yesOrNo;
                }
                else if(string.Compare(yesOrNo,"n") == 0)
                {
                    return yesOrNo;
                }

            }

            return null;
        }

        public string InputId()   //id입력 예외처리
        {
            string id = null;
            bool isSpecial = false;
                        
            id = Console.ReadLine();

            if (id == "q") return id;     //q입력시 종료

            isSpecial = Regex.IsMatch(id, @"[^a-zA-Z0-9가-힣]");     //정규식
            

            if (!string.IsNullOrEmpty(id) && id.Length >= 2 && id.Length <= 11)       //두 글자 이상 열한글자 이하이고
            {
                if (!id.Contains(" ") && !isSpecial)            //띄어쓰기가 없어야 함
                {

                    if (CustomerDB.Instance.SelectSameId(id))
                    {
                        PrintErrorMessage("이미 존재하는 아이디 입니다.");
                        return null;
                    }

                    ClearErrorMessage();
                    return id;
                }
            }

            PrintErrorMessage("다시 입력해주세요.");
            return null;            
        }

        public string InputPassword(string password)   //패스워드는 별로 표시
        {           
            ConsoleKeyInfo ckey;            
            int number = 0;
            string inputPassword = "";
            
            while (true)
            {
                ckey = Console.ReadKey();

                if (ckey.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();

                    if (inputPassword == "q") return inputPassword;

                    if (inputPassword.Length <= 11 && inputPassword.Length >= 2)
                    {
                        if (inputPassword != password && password != null)
                        {
                            
                            PrintErrorMessage("비밀번호가 서로 다릅니다.");
                            return null;
                        }

                        ClearErrorMessage();
                        return inputPassword;
                    }
                    else
                    {
                        PrintErrorMessage("잘못된 입력입니다.");
                        return null;
                    }
                }
                else if (Console.CursorLeft < 2)
                {
                    inputPassword = "";
                    Console.Write(" ");
                }
                else if(ckey.Key == ConsoleKey.Backspace)
                {                    
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    Console.Write("  ");
                    Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop);
                    if (Console.CursorLeft != 1) Console.Write("*");
                    else Console.Write(" ");
                    inputPassword = inputPassword.Remove(number - 1, 1);
                    --number;
                }                
                else if(Char.IsDigit(ckey.KeyChar) || Char.IsLower(ckey.KeyChar) || Char.IsUpper(ckey.KeyChar))  //특수문자입력 X                  
                {
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    Console.Write("*");

                    inputPassword += ckey.KeyChar;
                    ++number;
                }
                else   //특수기호나 한글 입력시 입력되지 않게 처리
                {
                    if(Encoding.Default.GetByteCount(ckey.KeyChar.ToString()) == 2)            //한글입력시
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop);
                        Console.Write("  ");
                        Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop);
                    }
                    else                                                                      //기타 특수문자 입력시
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    }

                }
            }
        }

        public string InputPhoneNumber() //전화번호 입력후 11자리 숫자가 입력되면 string으로 반환 아니면 null 반환
        {
            string phoneNumber;
            int number;

            phoneNumber = Console.ReadLine();

            if (phoneNumber == "q") return phoneNumber;

            if(phoneNumber.Length == 11)
            {

                for (number = 0; number < 11; number++)
                {
                    if (phoneNumber[number] < '0' || phoneNumber[number] > '9')      //숫자만 11자 입력
                    {
                        break;
                    }
                }

                if (number != 11)
                {
                    PrintErrorMessage("잘못된 입력입니다.");
                    return null;
                }

                if (CustomerDB.Instance.SelectSamePhoneNumber(phoneNumber))
                {
                    PrintErrorMessage("이미 존재하는 번호 입니다.");
                    return null;
                }


                ClearErrorMessage();
                return phoneNumber;
            }
            else
            {
                PrintErrorMessage("잘못된 입력입니다.");
                return null;
            }
        }

        public string InputName() 
        {
            string name;

            name = Console.ReadLine();

            if (name == "q") return name;


            if (!string.IsNullOrEmpty(name) && name.Length >= 2 && name.Length <= 11)  
            {
                ClearErrorMessage();
                return name;
            }
            else
            {
                PrintErrorMessage("잘못된 입력입니다.");
                return null;
            }            
        }

        public string InputAdress()
        {
            string adress;
            bool isCollect = false;

            adress = Console.ReadLine();
            
            if (adress == "q") return adress;

            isCollect = Regex.IsMatch(adress, @"[가-힣]{2,4}시\s[가-힣]{1,3}구\s[가-힣0-9]{1,10}");

            if (!string.IsNullOrEmpty(adress) && adress.Length >= 2 && adress.Length <= 20 && isCollect)
            {
                ClearErrorMessage();
                return adress;
            }
            else
            {
                PrintErrorMessage("잘못된 입력입니다.");
                return null;
            }
        }


        public string InputString(int above, int below) //above 이상 below 이하 만큼 크기의 문자열 입력 실패시 널 반환
        {
            string inputString = null;
            
            inputString = Console.ReadLine();
            int letter;

            if (inputString == "q") return inputString;
        
            for(letter = 0;letter < inputString.Length; letter++)
            {
                if (inputString[letter] != ' ') break;
            }

            if (letter == inputString.Length) return null;

            if (!string.IsNullOrEmpty(inputString) && inputString.Length >= above && inputString.Length <= below)       // above 이상 below 이하의 길이 일때
            {
                return inputString;
            }
            
            return null;
        }      
        
        public int GoPrivious(string userRequest)
        {
            string whiteSpace = new string(' ', 40);

            if (userRequest == "y" || userRequest == "Y" || userRequest == "yes")
            {                
                Console.Clear();
                return Constants.YES;
            }
            else if (userRequest == "n" || userRequest == "N" || userRequest == "no")
            {
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                Console.WriteLine(whiteSpace);
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                return Constants.NO;
            }
            else
            {
                Console.SetCursorPosition(36, Console.CursorTop - 1);
                Console.WriteLine(whiteSpace);
                Console.SetCursorPosition(36, Console.CursorTop - 1);

                return Constants.WRONG_INPUT;
            }                  
        }

        private void PrintErrorMessage(string message)
        {
            Console.SetCursorPosition(35, Console.CursorTop - 1);
            Console.WriteLine(new string(' ', 30));
            Console.SetCursorPosition(35, Console.CursorTop - 1);
            Console.Write(message);
            Console.SetCursorPosition(2, Console.CursorTop);
            Console.Write(new string(' ', Constants.ERROR_MESSAGE_LOCATION_X - 2));
        }

        private void ClearErrorMessage()
        {
            Console.SetCursorPosition(35, Console.CursorTop - 1);
            Console.Write(new string(' ',30));
        }

        public void MakeQuotesUse(ref string name, ref string author, ref string publisher)   //입력 정보에 작은따옴표나 큰따옴표가 포함되는지 확인
        {
            if(Regex.IsMatch(name, @"[""]{1,50}")) name = ConvertToQuote(name);
            if(Regex.IsMatch(author, @"[""]{1,50}")) author = ConvertToQuote(author);
            if(Regex.IsMatch(publisher, @"[""]{1,50}")) publisher = ConvertToQuote(publisher);
        }

        public void MakeQuotesUse(ref string name, ref string description)   //입력 정보에 작은따옴표나 큰따옴표가 포함되는지 확인
        {
            if (Regex.IsMatch(name, @"[""]{1,50}")) name = ConvertToQuote(name);
            if (Regex.IsMatch(description, @"[""]{1,300}")) description = ConvertToQuote(description);
        }

        private string ConvertToQuote(string inputString)     //문자열에 포함된 큰따옴표냐 작은 따옴표를 mysql에서 저장할수있도록 변환
        {
            for(int letter = 0;letter < inputString.Length; letter++)
            {               
                if (inputString[letter] == '\"')
                {
                    inputString = inputString.Insert(letter, "\"");
                    ++letter;
                }
            }

            return inputString;
        }

        public string DeleteHtmlTag(string text)
        {
            Regex regex = new Regex(@"<(.|\n)*?>");
            if (text.Contains("\n")) text = text.Replace("\n", "");
            return regex.Replace(text, "");
        }
        
    }
}
