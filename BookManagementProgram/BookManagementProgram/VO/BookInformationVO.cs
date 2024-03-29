﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementProgram
{
    class BookInformationVO:ICloneable
    {
        private int no;             //고유 책 번호
        private string name;        //책이름 최대20자
        private string author;      //작가이름 최대10자 
        private string publisher;   //출판사 최대10자
        private int quantity;       //최대 9권
        private int maxQuantity;
        private int price;
        private string pubDate;
        private string isbn;
        private string description;

        public int No
        {
            get { return no; }
            set { no = value; }
        }

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

        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        public string PubDate
        {
            get { return pubDate; }
            set { pubDate = value; }
        }

        public string Isbn
        {
            get { return isbn; }
            set { isbn = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        public BookInformationVO()
        {
            name = null;
            author = null;
            publisher = null;
            pubDate = null;
            isbn = null;
            description = null;
            quantity = 0;
        }

        public BookInformationVO(int no, string name, string author, string publisher, int quantity, int maxQuantity, int price)
        {
            this.no = no;
            this.name = name;
            this.author = author;
            this.publisher = publisher;
            this.quantity = quantity;
            this.maxQuantity = maxQuantity;
            this.price = price;
        }
        //네이버에서 추가할 떄
        public BookInformationVO(string name, string author, string publisher, string pubDate, string isbn, string description, int price)
        {
            this.name = name;
            this.author = author;
            this.publisher = publisher;
            this.pubDate = pubDate;
            this.isbn = isbn;
            this.description = description;
            this.price = price;
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