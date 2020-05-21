using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementProgram
{
    class BookInformationVO:ICloneable
    {
        private string name;        //책이름 최대20자
        private string author;      //작가이름 최대10자 
        private string publisher;   //출판사 최대10자
        private int quantity;       //최대 9권
        private int maxQuantity;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        public string Publisher
        {
            get { return publisher; }
            set { publisher = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public int MaxQuantity
        {
            get { return maxQuantity; }
            set { maxQuantity = value; }
        }

        public BookInformationVO()
        {
            name = null;
            author = null;
            publisher = null;
            quantity = 0;
        }

        public BookInformationVO(string name, string author, string publisher, int quantity)
        {
            this.name = name;
            this.author = author;
            this.publisher = publisher;
            this.quantity = quantity;
            this.maxQuantity = quantity;
        }

        public object Clone()
        {
            BookInformationVO bookInformation = new BookInformationVO();

            bookInformation.Name = name;
            bookInformation.Author = author;
            bookInformation.Publisher = publisher;
            bookInformation.quantity = quantity;

            return bookInformation;
        }
    }

    
}