using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementProgram
{
    class CustomerManagement : UITooI
    {   
        public CustomerInformationVO InputCustomerAccountInformation(NewAccountException newAccountException)   //등록하고자하는 계정정보 반환
        {
            CustomerInformationVO newCustomer = new CustomerInformationVO();
            string id = null;
            string password = null, passwordConfirmation = null;
            string name = null;
            string phoneNumber = null;
            string adress = null;
            string yesOrNo = null;
            int exceptionNumber = 0;

            Console.WriteLine("아이디 (특수문자 없이 2~11글자)");
            PrintInputBox("");

            Console.WriteLine("비밀번호(특수문자 없이 2~11글자)");
            PrintInputBox("");


            Console.WriteLine("비밀번호확인 (2~11글자)");
            PrintInputBox("");

            Console.WriteLine("이름 1~20글자 (2~11글자)");
            PrintInputBox("");

            Console.WriteLine("휴대폰번호('-'없이 11글자)");
            PrintInputBox("");

            Console.WriteLine("주소 1~20글자");
            PrintInputBox("");

            if (newAccountException.sameId == true)
            {
                Console.WriteLine("아이디가 이미 존재 합니다.");
                newAccountException.initialize(false);
                exceptionNumber = 1;
            }
            else if (newAccountException.wrongId == true)
            {
                Console.WriteLine("아이디를 다시 입력해주세요.");
                newAccountException.initialize(false);
                exceptionNumber = 1;
            }
            else if (newAccountException.wrongPassword == true)
            {
                Console.WriteLine("비밀번호를 다시 입력해주세요.");
                newAccountException.initialize(false);
                exceptionNumber = 1;
            }
            else if (newAccountException.wrongName == true)
            {
                Console.WriteLine("이름을 다시 입력해주세요.");
                newAccountException.initialize(false);
                exceptionNumber = 1;
            }
            else if (newAccountException.wrongPhoneNumber == true)
            {
                Console.WriteLine("휴대번호를 다시 입력해주세요.");
                newAccountException.initialize(false);
                exceptionNumber = 1;
            }
            else if (newAccountException.wrongAdress == true)
            {
                Console.WriteLine("주소를 다시 입력해주세요.");
                newAccountException.initialize(false);
                exceptionNumber = 1;
            }
            
            if(exceptionNumber == 1)
            {
                Console.Write("초기화면으로 돌아가시겠습니까?[y,n] ");
                yesOrNo = ExceptionHandling.InputString(1, 1);
                if(yesOrNo != null)
                {
                    if (yesOrNo == "y") newAccountException.previousOrStay = "previous";
                    else if(yesOrNo == "n")newAccountException.previousOrStay = "stay";                   
                }

                return newCustomer;
            }


            Console.SetCursorPosition(Console.CursorLeft + 2, Console.CursorTop - 22 - exceptionNumber);

            id = ExceptionHandling.InputId();   //id 입력

            MoveCursor();
            password = ExceptionHandling.InputPassword();  //password 입력

            MoveCursor();
            passwordConfirmation = ExceptionHandling.InputPassword();  //비밀번호 확인 입력

            MoveCursor();
            name = ExceptionHandling.InputString(2, 20);              //이름 입력

            MoveCursor();
            phoneNumber = ExceptionHandling.InputPhoneNumber();  //휴대번호 입력

            MoveCursor();
            adress = ExceptionHandling.InputString(1, 30);       //주소 입력

            newCustomer.Id = id;
            newCustomer.Password = password;
            newCustomer.Name = name;
            newCustomer.PhoneNumber = phoneNumber;
            newCustomer.Adress = adress;

            return newCustomer;
        }
        public void ModifyAdress(CustomerInformationVO logInCustomer)
        {
            string modifiedAdress = null;

            Console.WriteLine();
            Console.WriteLine("          현재 주소 : " + logInCustomer.Adress);
            Console.WriteLine();
            Console.Write("바꿀 주소(2~20글자) : ");

            modifiedAdress = ExceptionHandling.InputString(2, 20);
            if(modifiedAdress != null)
            {
                logInCustomer.Adress = modifiedAdress;
               
                PrintFailMessage("주소가 변경되었습니다.");

            }
            else
            {
                PrintFailMessage("잘못된 입력입니다.");


            }
        }

        public void ModifyPhoneNumber(CustomerInformationVO logInCustomer)
        {
            string modifiedPhoneNumber = null;

            Console.WriteLine();
            Console.WriteLine("현재 번호 : " + logInCustomer.PhoneNumber);
            Console.WriteLine();
            Console.Write("바꿀 번호 : ");

            modifiedPhoneNumber = ExceptionHandling.InputPhoneNumber();
            if (modifiedPhoneNumber != null)
            {
                logInCustomer.PhoneNumber = modifiedPhoneNumber;
                PrintFailMessage("번호가 변경되었습니다.");
            }
            else
            {
                PrintFailMessage("잘못된 입력입니다.");
            }

        }

        public List<CustomerInformationVO> SerchCustomer(List<CustomerInformationVO> customer,string serchString)
        {
            List<CustomerInformationVO> serchedCustomers = new List<CustomerInformationVO>();

            return  serchedCustomers;
        }
        public void PrintAllCustomer(List<CustomerInformationVO> customerList)
        {
            string divisionLine = new String('-', 125);

            Console.WriteLine(divisionLine);

            OneSpace(("번호").ToString(), 5);
            OneSpace("아이디", 23);
            OneSpace("이름", 23);
            OneSpace("휴대폰번호", 23);
            OneSpace("주소", 40);
            Console.WriteLine();

            for (int order = 0; order < customerList.Count; order++)
            {
                Console.WriteLine(divisionLine);

                OneSpace((order + 1).ToString(), 5);
                OneSpace(customerList[order].Id, 23);
                OneSpace(customerList[order].Name, 23);
                OneSpace(customerList[order].PhoneNumber, 23);
                OneSpace(customerList[order].Adress, 40);
                
                Console.WriteLine();
            }
            Console.WriteLine(divisionLine);

        }

        public void CheckId(int customer, List<CustomerInformationVO> customerList)
        {
            if (customerList[customer].IsAdministrator == false)
            {
                if (customerList[customer].RentedBook.Count == 0)   //왜 대여 했을때도 Count 가 0인지 모르겠음
                {
                    customerList.RemoveAt(customer);
                    PrintFailMessage("해당 아이디가 삭제 됐습니다.");
                }
                else
                {
                    PrintFailMessage("반납하지 않은 도서가 있는 아이디입니다.");
                }
            }
            else
            {
                PrintFailMessage("관리자의 아이디 입니다.");
            }
        }
    }
}
