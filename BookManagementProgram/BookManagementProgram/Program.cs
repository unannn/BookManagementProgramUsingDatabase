using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
/*
    관리자모드 
    id : admin  password : 0000

    일반사용자모드 
    id : asdf  password : asdf
    id : as  password : as     
*/
namespace BookManagementProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            BookManagementProgram bookManagementProgram = new BookManagementProgram();
            bookManagementProgram.StartProgram();
        }
    }
}
