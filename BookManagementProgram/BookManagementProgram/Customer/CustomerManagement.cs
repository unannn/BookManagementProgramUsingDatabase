using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BookManagementProgram
{
    class CustomerManagement
    { 
        public CustomerInformationVO InputCustomerAccountInformation()   //등록하고자하는 계정정보 반환
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
                    case Constants.INPUT_ID:
                        Console.SetCursorPosition(Constants.LOCATION_X, Constants.INPUTID_LOCATION_Y);
                        id = ExceptionHandling.Instance.InputId();   //id 입력
                        if (id == null) --createOrder;
                        else if (id == "q") return null;
                        break;

                    case Constants.INPUT_PASSWORD:                       
                        Console.SetCursorPosition(Constants.LOCATION_X, Constants.PASSWORD_LOCATION_Y);
                        password = ExceptionHandling.Instance.InputPassword(null);  //password 입력
                        if (password == null) --createOrder;
                        else if (password == "q") return null;
                        break;

                    case Constants.INPUT_PASSWORD_CONFIRM:
                        Console.SetCursorPosition(Constants.LOCATION_X, Constants.PASSWORD_CONF_LOCATION_Y);
                        passwordConfirmation = ExceptionHandling.Instance.InputPassword(password);  //비밀번호 확인 입력
                        if (passwordConfirmation == null) --createOrder;                       
                        else if (passwordConfirmation == "q") return null;
                        break;

                    case Constants.INPUT_NAME:
                        Console.SetCursorPosition(Constants.LOCATION_X, Constants.NAME_LOCATION_Y);
                        name = ExceptionHandling.Instance.InputName();                //이름입력
                        if (name == null) --createOrder;
                        else if (name == "q") return null;
                        break;

                    case Constants.INPUT_PHONENUMBER:
                        Console.SetCursorPosition(Constants.LOCATION_X, Constants.PHONE_LOCATION_Y);
                        phoneNumber = ExceptionHandling.Instance.InputPhoneNumber();  //휴대번호 입력
                        if (phoneNumber == null) --createOrder;
                        else if (phoneNumber == "q") return null;
                        break;

                    case Constants.INPUT_ADRESS:
                        Console.SetCursorPosition(Constants.LOCATION_X, Constants.ADRESS_LOCATION_Y);                        ;
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
            newCustomer.No = CustomerDB.Instance.InsertNewCustomer(id, password, name, phoneNumber, adress); //계정의 고유번호

            return newCustomer;
        }

        public bool ModifyAdress(CustomerInformationVO logInCustomer)
        {
            string modifiedAdress = null;
                        
            modifiedAdress = ExceptionHandling.Instance.InputAdress();

            if (modifiedAdress != null)
            {
                logInCustomer.Adress = modifiedAdress;
                CustomerDB.Instance.UpdateMyAdress(logInCustomer.No, modifiedAdress);

                return true;
            }
                        
            return false;
        }

        public bool ModifyPhoneNumber(CustomerInformationVO logInCustomer)
        {
            string modifiedPhoneNumber = null;
            
            modifiedPhoneNumber = ExceptionHandling.Instance.InputPhoneNumber();

            if (modifiedPhoneNumber != null)
            {
                logInCustomer.PhoneNumber = modifiedPhoneNumber;
                CustomerDB.Instance.UpdateMyPhoneNumber(logInCustomer.No, modifiedPhoneNumber);
                            
                return true;
            }

            return false;
        }

        public string CheckId(int customer, List<CustomerInformationVO> customerList)
        {
            if (customerList[customer].IsAdministrator) return "관리자의 아이디 입니다.";
            
            if (customerList[customer].RentedBook.Count != 0) return "반납하지 않은 도서가 있는 아이디입니다.";
            
            CustomerDB.Instance.DeleteCusomter(customerList[customer].No);
            customerList.RemoveAt(customer);

            return "해당 아이디가 삭제 됐습니다.";
        }
    }
}
