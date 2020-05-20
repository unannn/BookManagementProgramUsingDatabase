using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace BookManagementProgram
{
    class NewAccountException //정규식 추가하면 좋을듯
    {
        public bool sameId;
        public bool wrongId;   //isIdlect
        public bool wrongPassword;
        public bool wrongName;
        public bool wrongPhoneNumber;
        public bool wrongAdress;
        public string previousOrStay;

        public NewAccountException()
        {
            sameId = false;
            wrongId = false;
            wrongPassword = false;
            wrongName = false;
            wrongPhoneNumber = false;
            wrongAdress = false;
            previousOrStay = " ";
        }

        public void initialize(bool trueOrFalse)
        {
            sameId = trueOrFalse;
            wrongId = trueOrFalse;
            wrongPassword = trueOrFalse;
            wrongName = trueOrFalse;
            wrongPhoneNumber = trueOrFalse;
            wrongAdress = trueOrFalse;
            previousOrStay = " ";
        }
    }
   
    static class ExceptionHandling
    {
        public const int wrongInput = -1;
               
        static public int InputNumber(int start, int end, string numberInString) //여러자리 가능하지만 익셉션발생 수정해야함
        {
            int inputNumber = wrongInput;
            int number = 0;

            if (!string.IsNullOrEmpty(numberInString))
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

        static public string InputYesOrNo(string yesOrNo)   //문자열이 y 인지 n인지 아님 다른값이 들어왔는지 판단 후 반환
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

        static public string InputId()   //id입력 예외처리
        {
            string id = null;
            bool isSpecial = false;
                        
            id = Console.ReadLine();
            isSpecial = Regex.IsMatch(id, @"[^a-zA-Z0-9가-힣]");     //정규식
            

            if (!string.IsNullOrEmpty(id) && id.Length >= 2 && id.Length <= 11)       //두 글자 이상 열한글자 이하이고
            {
                if(!id.Contains(" ") && !isSpecial)            //띄어쓰기가 없어야 함
                {
                    return id;
                }
            }

            return null;
        }

        static public string InputPassword()   //패스워드는 별로 표시
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

                    if (inputPassword.Length <= 11) return inputPassword;
                    else return null;
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
                else
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

        static public string InputPhoneNumber() //전화번호 입력후 11자리 숫자가 입력되면 string으로 반환 아니면 null 반환
        {
            string phoneNumber;
            int number;

            phoneNumber = Console.ReadLine();

            if(phoneNumber.Length == 11)
            {

                for (number = 0; number < 11; number++)
                {
                    if (phoneNumber[number] < '0' || phoneNumber[number] > '9')      //숫자만 11자 입력
                    {
                        break;
                    }
                }

                if (number != 11) phoneNumber = null;
            }
            else
            {
                phoneNumber = null;
            }

            return phoneNumber;
        }

        static public string InputString(int above, int below) //above 이상 below 이하 만큼 크기의 문자열 입력 실패시 널 반환
        {
            string inputString = null;
                      

            inputString = Console.ReadLine();

            if (!string.IsNullOrEmpty(inputString) && inputString.Length >= above && inputString.Length <= below)       // above 이상 below 이하의 길이 일때
            {
                return inputString;
            }

            return null;

        }
    }
}
