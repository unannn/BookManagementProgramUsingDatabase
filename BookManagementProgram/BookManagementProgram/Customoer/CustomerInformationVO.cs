using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementProgram
{
    class CustomerInformationVO :ICloneable
    {
        private int no;
        private string id;
        private string password;
        private string name;
        private string phoneNumber;
        private string adress;
        private bool isAdministrator;
        private List<BookInformationVO> rentedBook;

        public int No
        {
            get { return no; }
            set { no = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public string Adress
        {
            get { return adress; }
            set { adress = value; }
        }

        public bool IsAdministrator
        {
            get { return isAdministrator; }
            set { isAdministrator = value; }
        }

        public List<BookInformationVO> RentedBook
        {
            get { return rentedBook; }
            set { rentedBook = value; }
        }

        public CustomerInformationVO()
        {
            this.id = null;
            this.password = null;
            this.name = null;
            this.phoneNumber = null;
            this.adress = null;
            this.isAdministrator = false;
            this.rentedBook = new List<BookInformationVO>();
        }

        public CustomerInformationVO(string id, string password, string name, string phoneNumber, string adress,bool isAdministrator)
        {
            this.id = id;
            this.password = password;
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.adress = adress;
            this.isAdministrator = isAdministrator;
            this.rentedBook = new List<BookInformationVO>();
        }

        public CustomerInformationVO(int no, string id, string password, string name, string phoneNumber, string adress, bool isAdministrator)
        {
            this.no = no;
            this.id = id;
            this.password = password;
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.adress = adress;
            this.isAdministrator = isAdministrator;
            this.rentedBook = new List<BookInformationVO>();
        }

        public object Clone()
        {
            CustomerInformationVO customer = new CustomerInformationVO();

            customer.id = this.id;
            customer.password = this.password;
            customer.name = this.name;
            customer.phoneNumber = this.phoneNumber;
            customer.adress = this.adress;
            customer.isAdministrator = this.isAdministrator;

            return customer;
        }
    }
    
}
