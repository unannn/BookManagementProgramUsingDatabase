using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace BookManagementProgram.Book
{
   
    class NaverAPI
    {
        private static NaverAPI instance = null;
        public static NaverAPI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NaverAPI();
                }
                return instance;
            }
        }
        
        public void SearchBooks(List<BookInformationVO> naverBookList ,string title,int searchingNumber)
        {
            naverBookList.Clear(); //List 초기화

            string clientId = "JP7J1mrKsWbV8xBbBqf0";
            string clientSecret = "2wEGmC5TjY";
            string name;        //책이름 최대20자
            string author;      //작가이름 최대10자 
            string publisher;   //출판사 최대10자
            
            int price;
            string pubDate;
            string isbn;
            string description;
            string url = "https://openapi.naver.com/v1/search/book_adv.json?d_titl=" + title + "&display=" + searchingNumber; // 결과가 JSON 포맷

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", clientId); // 클라이언트 아이디
            request.Headers.Add("X-Naver-Client-Secret", clientSecret);       // 클라이언트 시크릿
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string status = response.StatusCode.ToString();
            if (status == "OK")
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                
                string text;
                text = reader.ReadToEnd();
                JObject json = JObject.Parse(text);

                for(int bookNumber = 0;bookNumber < searchingNumber; bookNumber++)
                {
                    if (bookNumber == json["items"].Count())
                    {
                        break;
                    }

                    name = ExceptionHandling.Instance.DeleteHtmlTag(json["items"][bookNumber]["title"].ToString());
                    author = ExceptionHandling.Instance.DeleteHtmlTag(json["items"][bookNumber]["author"].ToString());
                    publisher = ExceptionHandling.Instance.DeleteHtmlTag(json["items"][bookNumber]["publisher"].ToString());
                    pubDate = ExceptionHandling.Instance.DeleteHtmlTag(json["items"][bookNumber]["pubdate"].ToString());
                    isbn = ExceptionHandling.Instance.DeleteHtmlTag(json["items"][bookNumber]["isbn"].ToString()).Substring(13);
                    description = ExceptionHandling.Instance.DeleteHtmlTag(json["items"][bookNumber]["description"].ToString());  
                    price = int.Parse(ExceptionHandling.Instance.DeleteHtmlTag(json["items"][bookNumber]["price"].ToString()));

                    ExceptionHandling.Instance.MakeQuotesUse(ref name, ref description);

                    naverBookList.Add(new BookInformationVO(name, author, publisher, pubDate, isbn, description, price));
                }
            }
            else
            {
                Console.WriteLine("Error 발생=" + status);
            }
        }
    }
}
