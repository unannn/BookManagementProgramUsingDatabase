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
        public const int BASIC_HEIGHT = 49;

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
        
        //계정 생성시 커서위치
        public const int INPUTID_LOCATION_Y = 10;
        public const int ERROR_MESSAGE_LOCATION_X = 35;
        public const int PASSWORD_LOCATION_Y = 14;
        public const int PASSWORD_CONF_LOCATION_Y = 18;
        public const int NAME_LOCATION_Y = 22;
        public const int PHONE_LOCATION_Y = 26;
        public const int ADRESS_LOCATION_Y = 30;

        //로그인시
        public const int LOCATION_X = 2;
        public const int LOGIN_ID_LOCATION_Y = 8;
        public const int LOGIN_PASSWORD_LOCATION_Y = 12;
        
        //입력 범위
        public const int STARTING_NUMBER = 1;
        public const int BOOK_QUANTITY_MAXIMUM = 10;
        public const int BOOK_NUMBER_MAXIMUM = 10000;
        public const int BOOK_PRICE_MINIMUM = 1000;

        public const int BOOK_PRICE_MAXIMUM = 99999;
        public const int RENT_BOOK_MAXIMUM = 5;
        public const int BOOK_NAME_LENGTH_MAXIMUM = 20;
        public const int BOOK_AUTHOER_LENGTH_MAXIMUM = 15;
        public const int BOOK_PUBLISHER_LENGTH_MAXIMUM = 10;
        public const int DATE_LENGTH_MAXIMUM = 10;
        
        //관리자모드 메뉴
        public const int BOOK_SERCHING_RENTAL = 1;
        public const int BOOK_RETURN = 2;
        public const int BOOK_REGISTRATION = 3;
        public const int BOOK_DELETE = 4;
        public const int CUSOMTER_LIST = 5;
        public const int CUSTOMER_DELETE = 6;
        public const int MY_DATA_MODIFYING = 7;
        public const int LOGOUT = 8;
        public const int PROGRAM_END_ADMIN = 9;

        //일반유저 모드 메뉴        
        public const int MY_DATA_MODIFYING_USER = 3;
        public const int LOGOUT_USER = 4;
        public const int PROGRAM_END_USER = 5;

        //새 계정 생성
        public const int INPUT_ID = 0;
        public const int INPUT_PASSWORD = 1;
        public const int INPUT_PASSWORD_CONFIRM = 2;
        public const int INPUT_NAME = 3;
        public const int INPUT_PHONENUMBER = 4;
        public const int INPUT_ADRESS = 5;

        //제목위치
        public const int BASIC_LOCATION = 7;
        public const int BOOK_RENT_LOCATION = 27;

        //도서검색및 대여 메뉴
        public const int SEARCHING_BY_TITLE = 1;
        public const int SEARCHING_BY_AUTHOER = 2;
        public const int SEARCHING_BY_PUBLISHER = 3;
        public const int RENT_BOOK = 4;
        public const int EXIT_SEARCHING = 5;

        //내정보수정 메뉴

        public const int MODIFYING_PHONENUMBER = 1;
        public const int MODIFYING_ADRESS = 2;
        public const int MODIFYING_EXIT = 3;


        
    }
}
