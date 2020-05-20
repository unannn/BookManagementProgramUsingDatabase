using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementProgram
{  
    class UITooI
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
            string whiteSpace = new string(' ', 40);
            string yesOrNo = null;
            Console.WriteLine("아이디 (2~11글자)");

            PrintInputBox("");

            PrintInputBox("비밀번호");

            if (inputInspection == ExceptionHandling.wrongInput)             //입력 오류 체크
            {
                Console.WriteLine("가입하지 않은 아이디이거나, 잘못된 비밀번호입니다.");
                Console.Write("초기화면으로 돌아가시겠습니까?[y,n] ");

                while (true)
                {
                    yesOrNo = Console.ReadLine();
                    if (yesOrNo == "y")
                    {
                        id = null;
                        password = null;
                        Console.Clear();
                        return;
                    }
                    else if (yesOrNo == "n")
                    {
                        Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                        Console.WriteLine(whiteSpace);
                        Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);

                        break;
                    }
                    else
                    {
                        Console.SetCursorPosition(36, Console.CursorTop - 1 );
                        Console.WriteLine(whiteSpace);
                        Console.SetCursorPosition(36, Console.CursorTop - 1);
                    }
                }
               
            }

            Console.SetCursorPosition(Console.CursorLeft + 2, Console.CursorTop - 5 + inputInspection);  //입력받을 때 커서위치 이동후 입력받음
            id = Console.ReadLine();

            Console.SetCursorPosition(Console.CursorLeft + 2, Console.CursorTop + 2);
             Console.Write(whiteSpace);
            Console.SetCursorPosition(2, Console.CursorTop);
            password = ExceptionHandling.InputPassword();                       
        }
              
        protected void PrintInputBox(string inputData)        //사각박스 출력
        {
            string loginBar = new String('-', 35);
            string whiteSpace = new String(' ', 50 );

            Console.WriteLine(loginBar);
            Console.Write("  {0} ", inputData);
            Console.Write(whiteSpace);
            Console.WriteLine();
            Console.WriteLine(loginBar);
        }

        protected void PrintMenuList(List<string> menuList) //매개변수로받은 문자열 리스트를 한줄씩 출력함
        {
            Console.WriteLine();
            foreach(string item in menuList)
            {
                Console.WriteLine(item + "\n");
            }
        }

        protected void OneSpace(string bookeInformation,int limit) //도서, 고객 목록 출력시 칸 정렬
        {
            Console.Write(" " + bookeInformation);
            Console.Write(new String(' ', limit - Encoding.Default.GetByteCount(bookeInformation)));  //한글영어숫자구분 위해 바이트단위로 계산
            Console.Write("|");
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
            Console.SetCursorPosition(2, Console.CursorTop);
        }
    }
}
