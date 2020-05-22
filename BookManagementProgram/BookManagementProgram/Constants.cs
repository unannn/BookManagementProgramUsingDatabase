using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementProgram
{
    static class Constants
    {
        //화면 사이즈
        public const int BASIC_WIDTH = 90;
        public const int BASIC_HEIGHT = 36;

        public const int INIT_WIDTH = 50;
        public const int INIT_HEIGHT = 30;

        public const int CREATE_ACCOUNT_WIDTH = 64;
        public const int CREATE_ACCOUNT_HEIGHT = 40;

        public const int CUSTOMER_PROCESS_WIDTH = 130;
        public const int CUSTOMER_PROCESS_HEIGHT = 36;

        //초기화면
        public const int LOGIN = 1;
        public const int CREATE_ACCOUNT = 2;
        public const int PROGRAM_END = 3;
        
        //계정 생성시
        public const int INPUTID_LOCATION_Y = 10;
        public const int ERROR_MESSAGE_LOCATION_X = 35;
        public const int PASSWORD_LOCATION_Y = 14;
        public const int PASSWORD_CONF_LOCATION_Y = 18;
        public const int NAME_LOCATION_Y = 22;
        public const int PHONE_LOCATION_Y = 26;
        public const int ADRESS_LOCATION_Y = 30;

        //입력 범위
        public const int STARTING_NUMBER = 1;
        public const int BOOK_QUANTITY_MAXIMUM = 10;
        public const int BOOK_NUMBER_MAXIMUM = 10000;
        public const int RENT_BOOK_MAXIMUM = 5;
        public const int BOOK_NAME_LENGTH_MAXIMUM = 20;
        public const int BOOK_AUTHOER_LENGTH_MAXIMUM = 15;
        public const int BOOK_PUBLISHER_LENGTH_MAXIMUM = 10;
    }
}
