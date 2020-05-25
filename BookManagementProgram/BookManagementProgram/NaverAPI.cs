using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace BookManagementProgram
{
    class Book
    {      

    }
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

        public void SearchBooks(string title,int searchingNumber)
        {
            string clientId = "JP7J1mrKsWbV8xBbBqf0";
            string clientSecret = "2wEGmC5TjY";

            string url = "https://openapi.naver.com/v1/search/book_adv.json?d_titl=" + title + "&display="+searchingNumber; // 결과가 JSON 포맷

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
                        Console.Write("더이상 책이 검색되지 않습니다1.");
                        break;
                    }

                    Console.Write(ExceptionHandling.Instance.DeleteHtmlTag(json["items"][bookNumber]["title"].ToString()));
                    Console.WriteLine();
                    Console.Write(ExceptionHandling.Instance.DeleteHtmlTag(json["items"][bookNumber]["author"].ToString()));
                    Console.WriteLine();
                    Console.Write(json["items"][bookNumber]["publisher"]);
                    Console.WriteLine();
                    Console.Write(json["items"][bookNumber]["pubdate"]);
                    Console.WriteLine();
                    Console.Write(json["items"][bookNumber]["isbn"]);
                    Console.WriteLine();
                    Console.Write(ExceptionHandling.Instance.DeleteHtmlTag(json["items"][bookNumber]["description"].ToString()));
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();


                    
                }

            }
            else
            {
                Console.WriteLine("Error 발생=" + status);
            }
        }
    }
}
