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
        public const int BASIC_WIDTH = 130;
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
        public const int BOOK_NAME_LENGTH_MAXIMUM = 40;
        public const int BOOK_AUTHOER_LENGTH_MAXIMUM = 30;
        public const int BOOK_PUBLISHER_LENGTH_MAXIMUM = 20;
        public const int DATE_LENGTH_MAXIMUM = 10;
        public const int BOOK_NUMBER_LENGTH_MAXIMUM = 6;
        
        //관리자모드 메뉴
        public const int BOOK_SEARCHING_RENTAL = 1;
        public const int BOOK_RETURN = 2;
        public const int BOOK_REGISTRATION = 3;
        public const int BOOK_DELETE = 4;
        public const int BOOK_MODIFYING = 5;
        public const int CUSOMTER_LIST = 6;
        public const int CUSTOMER_DELETE = 7;
        public const int MY_DATA_MODIFYING = 8;
        public const int LOGOUT = 9;
        public const int PROGRAM_END_ADMIN = 10;

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
        public const int MODIFY_MYDATA_LOCATION = 27;
        public const int BOOK_RENT_LOCATION = 47;

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

        //책 정보 테이블 너비

        public const int BOOK_TABLE_WIDTH = 117;
        public const int RETURN_BOOK_TABLE_WIDTH = 126;
        //책 대여 예외처리

        public const int NOT_EXIST = 1;
        public const int OVER_MAXIMUM_RENTAL_BOOK = 2;
        public const int ALEADY_RENTAL_BOOK = 3;
        public const int NO_OVERLEFT_BOOK = 4;
        public const int RENTAL_SUCESS = 5;

        //사용자 예아니오

        public const int YES = 1;
        public const int NO = 2;
        public const int WRONG_INPUT = 3;

        //책 정보 수정

        public const int BOOK_NUMBER_MODIFYING = 1;
        public const int PRICE_MODIFYING = 2;
        public const int MODIFYING_END = 3;
    }
}
