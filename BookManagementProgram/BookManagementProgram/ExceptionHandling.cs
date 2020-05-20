﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BookManagementProgram
{  
    class ExceptionHandling
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

        public string InputId(List<CustomerInformationVO> customerList)   //id입력 예외처리
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
                    foreach (CustomerInformationVO customer in customerList)
                    {
                        if (customer.Id == id)
                        {
                            PrintErrorMessage("이미 존재하는 아이디 입니다.");
                            return null;
                        }
                    }
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
                    if (inputPassword.Length <= 11 && inputPassword.Length >= 2)
                    {
                        if (inputPassword != password && password != null)
                        {
                            
                            PrintErrorMessage("비밀번호가 서로 다릅니다.");
                            return null;
                        }
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
                    phoneNumber = null;
                }
            }
            else
            {
                PrintErrorMessage("잘못된 입력입니다.");
                phoneNumber = null;
            }

            return phoneNumber;
        }

        public string InputName() //
        {
            string name;

            name = Console.ReadLine();

            if (name == "q") return name;


            if (!string.IsNullOrEmpty(name) && name.Length >= 2 && name.Length <= 11)       // above 이상 below 이하의 길이 일때
            {
                return name;
            }
            else
            {
                PrintErrorMessage("잘못된 입력입니다.");
                return null;
            }

            return name;
        }
        public string InputString(int above, int below) //above 이상 below 이하 만큼 크기의 문자열 입력 실패시 널 반환
        {
            string inputString = null;
            
            inputString = Console.ReadLine();

            if (inputString == "q") return inputString;
        
            if (!string.IsNullOrEmpty(inputString) && inputString.Length >= above && inputString.Length <= below)       // above 이상 below 이하의 길이 일때
            {
                return inputString;
            }
            
            return null;

        }      
        
        private void PrintErrorMessage(string message)
        {
            Console.SetCursorPosition(35, Console.CursorTop - 1);
            Console.Write(message);
            Console.SetCursorPosition(2, Console.CursorTop);
            Console.Write(new string(' ', Constants.ERROR_MESSAGE_LOCATION_X - 2));
        }
    }
}
