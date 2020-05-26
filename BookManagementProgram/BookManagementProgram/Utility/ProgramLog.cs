using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace BookManagementProgram.Utility
{

    class ProgramLog
    {
        private static ProgramLog instance = null;
        private string logForm;
        private string loginCustomerName;
        public string LoginCustomerName
        {
            get { return loginCustomerName; }
            set { loginCustomerName = value; }
        }
        private StreamWriter fileToWrite;
        private StreamReader fileToRead;
        private List<string> logList;

        public static ProgramLog Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProgramLog();
                }
                return instance;
            }
        }
                
        public void CreateTextrwriter(string data, string log)
        {
            logList = new List<string>();

            fileToRead = new StreamReader(new FileStream("./log.txt", FileMode.Open), Encoding.UTF8);
            string line;
            while((line = fileToRead.ReadLine()) != null) logList.Add(line);

            fileToRead.Close();
            
            fileToWrite = new StreamWriter(new FileStream("./log.txt", FileMode.Create), Encoding.UTF8);

            foreach(string oneLog in logList)
            {
                fileToWrite.WriteLine(oneLog);
            }

            logForm = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]" + loginCustomerName + "'" +data + "'" + log ;
            fileToWrite.WriteLine(logForm);
            ExceptionHandling.Instance.MakeQuotesUse(ref logForm);
            LogDB.Instance.InsertLog(logForm);
            fileToWrite.Close();
            logList.Clear();
        }

        public void InitializeLog()
        {
            fileToWrite = new StreamWriter(new FileStream("./log.txt", FileMode.Create), Encoding.UTF8);
            
            fileToWrite.WriteLine("");
            LogDB.Instance.InitializeLog();

            fileToWrite.Close();
        }
    }
}
