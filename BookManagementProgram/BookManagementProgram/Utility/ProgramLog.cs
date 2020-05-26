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
        private StreamWriter fileToWrite;
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
                
        public void CreateTextrwriter(string loginCustomerName, string data, string log)
        {
            fileToWrite = new StreamWriter(new FileStream("./log.txt", FileMode.Create), Encoding.UTF8);

            logForm = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]" + loginCustomerName + "'" +data + "'" + log ;
            fileToWrite.WriteLine(logForm);
            fileToWrite.Close();
        }
    }
}
