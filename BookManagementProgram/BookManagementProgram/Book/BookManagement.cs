using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BookManagementProgram
{
    class BookManagement:UITooI
    {       
        public void InitializeBookList(List<BookInformationVO> bookList)
        {
            MySqlDataReader books = BookDB.Instance.SelectAllBooks();

            while (books.Read())
            {
                bookList.Add(new BookInformationVO(int.Parse(books["no"].ToString()), books["title"].ToString(), books["author"].ToString(), books["publisher"].ToString(), int.Parse(books["quantity"].ToString()),int.Parse(books["maxQuntity"].ToString())));
            }
        }

        public void RentBook(CustomerInformationVO logInCustomer, List<BookInformationVO> bookList)  //책대여 함수
        {
            int inputNumber = 0;
            int rowNumber = 0;
            string inputNumberInString = null;

            

            Console.Write("대여할 책 번호 입력 : ");
            inputNumberInString = Console.ReadLine();
            inputNumber = ExceptionHandling.Instance.InputNumber(Constants.STARTING_NUMBER, Constants.BOOK_NUMBER_MAXIMUM, inputNumberInString);

            if (inputNumber != ExceptionHandling.wrongInput) //올바른 번호가 입력되고 남은 수량이 있으면
            {
                rowNumber = BookDB.Instance.UpdateRentalBook(inputNumber);

                if (rowNumber != 0)                              //중복대여여부 검사해야함
                {
                    foreach(BookInformationVO book in bookList)
                    {
                        if(book.No == inputNumber)
                        {
                           book.Quantity -= 1;
                            logInCustomer.RentedBook.Add(book);
                            PrintFailMessage("대여가 완료되었습니다.");
                            return;
                        }
                    }
                }
                else
                {
                    PrintFailMessage("해당 도서가 존재하지 않습니다.");
                }                
            }
            else
            {
                PrintFailMessage("해당 도서가 존재하지 않습니다.");
            }

        }
             

        public void PrintBookListForReturn(List<BookInformationVO> bookList)
        {
            string divisionLine = new String('-', 72);
            //string blank = null;

            for (int order = 0; order < bookList.Count; order++)
            {
                Console.WriteLine(divisionLine);

                OneSpace(bookList[order].No.ToString(), 3);
                OneSpace(bookList[order].Name, 30);
                OneSpace(bookList[order].Author, 20);
                OneSpace(bookList[order].Publisher, 10);
                                
                Console.WriteLine();
            }

            Console.WriteLine(divisionLine);
        }
               
        public List<BookInformationVO> SerchByTitle(List<BookInformationVO> bookList)
        {
            List<BookInformationVO> serchedBooks = new List<BookInformationVO>();
            string inputString = null;

            Console.Write("도서 이름 입력 : ");
            Console.Write("                  ");
            Console.SetCursorPosition(Console.CursorLeft - 18, Console.CursorTop);

            inputString = ExceptionHandling.Instance.InputString(1, 20);
            
            
            for (int book = 0; book < bookList.Count; book++)
            {
                if (!string.IsNullOrEmpty(inputString) && bookList[book].Name.Contains(inputString))   //검색된 문자열이 책이름에 포함되는 책리스트의 책이 있으면
                {
                    serchedBooks.Add((BookInformationVO)bookList[book]);   //복사해서 serchedBooks 리스트에 추가
                }
            }

            return serchedBooks;
        }

        public List<BookInformationVO> SerchByAuthor(List<BookInformationVO> bookList)
        {
            List<BookInformationVO> serchedBooks = new List<BookInformationVO>();
            string inputString = null;

            Console.Write("도서 저자 입력 : ");
            Console.Write("                  ");
            Console.SetCursorPosition(Console.CursorLeft - 18, Console.CursorTop);

            inputString = ExceptionHandling.Instance.InputString(1, 20);


            for (int book = 0; book < bookList.Count; book++)
            {
                if (!string.IsNullOrEmpty(inputString) && bookList[book].Author.Contains(inputString))   //검색된 문자열이 책이름에 포함되는 책리스트의 책이 있으면
                {
                    serchedBooks.Add((BookInformationVO)bookList[book]);   //복사해서 serchedBooks 리스트에 추가
                }
            }

            return serchedBooks;
        }

        public List<BookInformationVO> SerchedByPublisher(List<BookInformationVO> bookList)
        {
            List<BookInformationVO> serchedBooks = new List<BookInformationVO>();
            string inputString = null;

            Console.Write("도서 출판사 입력 : ");
            Console.Write("                  ");
            Console.SetCursorPosition(Console.CursorLeft - 18, Console.CursorTop);

            inputString = ExceptionHandling.Instance.InputString(1, 20);


            for (int book = 0; book < bookList.Count; book++)
            {
                if (inputString != null && bookList[book].Publisher.Contains(inputString))   //검색된 문자열이 책이름에 포함되는 책리스트의 책이 있으면
                {
                    serchedBooks.Add((BookInformationVO)bookList[book]);   //복사해서 serchedBooks 리스트에 추가
                }
            }

            return serchedBooks;
        }

    }
}
