using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementProgram
{
    class CustomerManagement : UITooI
    {   
        public CustomerInformationVO InputCustomerAccountInformation( List<CustomerInformationVO> customerList)   //등록하고자하는 계정정보 반환
        {
            CustomerInformationVO newCustomer = new CustomerInformationVO();
            string id = null;
            string password = null, passwordConfirmation = null;
            string name = null;
            string phoneNumber = null;
            string adress = null;
            int createOrder = 0;
            while (createOrder < 6)
            {
                switch (createOrder)
                {
                    case 0:
                        Console.SetCursorPosition(2, Constants.INPUTID_LOCATION_Y);
                        id = ExceptionHandling.Instance.InputId(customerList);   //id 입력
                        if (id == null) --createOrder;
                        else if (id == "q") return null;
                        break;

                    case 1:                       
                        Console.SetCursorPosition(2, Constants.PASSWORD_LOCATION_Y);
                        password = ExceptionHandling.Instance.InputPassword(null);  //password 입력
                        if (password == null) --createOrder;
                        else if (password == "q") return null;
                        break;

                    case 2:
                        Console.SetCursorPosition(2, Constants.PASSWORD_CONF_LOCATION_Y);
                        passwordConfirmation = ExceptionHandling.Instance.InputPassword(password);  //비밀번호 확인 입력
                        if (passwordConfirmation == null) --createOrder;                       
                        else if (passwordConfirmation == "q") return null;
                        break;

                    case 3:
                        Console.SetCursorPosition(2, Constants.NAME_LOCATION_Y);
                        name = ExceptionHandling.Instance.InputName();
                        if (name == null) --createOrder;
                        else if (name == "q") return null;
                        break;

                    case 4:
                        Console.SetCursorPosition(2, Constants.PHONE_LOCATION_Y);
                        phoneNumber = ExceptionHandling.Instance.InputPhoneNumber();  //휴대번호 입력
                        if (phoneNumber == null) --createOrder;
                        else if (phoneNumber == "q") return null;
                        break;

                    case 5:
                        Console.SetCursorPosition(2, Constants.ADRESS_LOCATION_Y);                        ;
                        adress = ExceptionHandling.Instance.InputAdress();       //주소 입력
                        if (adress == null) --createOrder;
                        else if (adress == "q") return null;
                        break;
                }
                ++createOrder;              
            }      

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

            modifiedAdress = ExceptionHandling.Instance.InputString(2, 20);
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

            modifiedPhoneNumber = ExceptionHandling.Instance.InputPhoneNumber();
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
