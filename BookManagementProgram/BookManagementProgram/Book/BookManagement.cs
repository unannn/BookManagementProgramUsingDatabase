using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BookManagementProgram
{
    class BookManagement
    { 
        public int RentBook(CustomerInformationVO logInCustomer, List<BookInformationVO> bookList)  //책대여 함수
        {
            int inputNumber = 0;
            int rowNumber = 0;
            int rentalControl;
            string inputNumberInString = null;
            

            inputNumberInString = Console.ReadLine();
            inputNumber = ExceptionHandling.Instance.InputNumber(Constants.STARTING_NUMBER, Constants.BOOK_NUMBER_MAXIMUM, inputNumberInString);

            rentalControl = ExceptionHandling.Instance.RentBook(logInCustomer,bookList,inputNumber);

            if (rentalControl == Constants.RENTAL_SUCESS)
            {
                foreach (BookInformationVO book in bookList)
                {
                    if (book.No == inputNumber)
                    {
                        rowNumber = BookDB.Instance.UpdateRentalBook(inputNumber);
                        RentalBookDB.Instance.InsertRentalBookInfo(logInCustomer.No, inputNumber);
                        book.Quantity -= 1;
                        logInCustomer.RentedBook.Add(book);
                        return rentalControl;
                    }
                }

                return Constants.NOT_EXIST;
            }          

            return rentalControl;
        }
            
               
        public List<BookInformationVO> SerchByTitle(List<BookInformationVO> bookList)
        {
            List<BookInformationVO> serchedBooks = new List<BookInformationVO>();
            string inputString = null;
            
            inputString = ExceptionHandling.Instance.InputString(1, 20);
                        
            for (int book = 0; book < bookList.Count; book++)
            {
                if (!string.IsNullOrEmpty(inputString) && bookList[book].Name.Contains(inputString))   //검색된 문자열이 책이름에 포함되는 책리스트의 책이 있으면
                {
                    serchedBooks.Add(bookList[book]);   //복사해서 serchedBooks 리스트에 추가
                }
            }

            return serchedBooks;
        }

        public List<BookInformationVO> SerchByAuthor(List<BookInformationVO> bookList)
        {
            List<BookInformationVO> serchedBooks = new List<BookInformationVO>();
            string inputString = null;
            
            inputString = ExceptionHandling.Instance.InputString(1, 20);


            for (int book = 0; book < bookList.Count; book++)
            {
                if (!string.IsNullOrEmpty(inputString) && bookList[book].Author.Contains(inputString))   //검색된 문자열이 책이름에 포함되는 책리스트의 책이 있으면
                {
                    serchedBooks.Add(bookList[book]);   //복사해서 serchedBooks 리스트에 추가
                }
            }

            return serchedBooks;
        }

        public List<BookInformationVO> SerchedByPublisher(List<BookInformationVO> bookList)
        {
            List<BookInformationVO> serchedBooks = new List<BookInformationVO>();
            string inputString = null;

            inputString = ExceptionHandling.Instance.InputString(1, 20);
            
            for (int book = 0; book < bookList.Count; book++)
            {
                if (inputString != null && bookList[book].Publisher.Contains(inputString))   //검색된 문자열이 책이름에 포함되는 책리스트의 책이 있으면
                {
                    serchedBooks.Add(bookList[book]);   //복사해서 serchedBooks 리스트에 추가
                }
            }

            return serchedBooks;
        }

    }
}
