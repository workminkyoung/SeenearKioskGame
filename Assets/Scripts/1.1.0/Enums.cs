using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO : change app name 
namespace KioskApp.Tutorial
{
    public enum POINT_SIZE
    {
        S = 0,
        M,
        L
    }

    public enum ONBOARDING
    {
        START = 0,
        GAME,
        PROMISE,
        PROMISE1,
        SIGN
    }

    public enum CINEMA_TICKET
    {
        SELECT_BOOK = 0,
        RESERVE_MOVIE,
        RESERVE_TIME,
        RESERVE_NUMBER,
        RESERVE_SEAT,
        RESERVE_CHECK,
        SELECT_PAY,
        //NAVIGATION,
        END_PAGE
    }

    public enum CINEMA_RESERVATION
    {
        SELECT_BOOK = 0,
        CONFIRM_NUM,
        RESERVE_CHECK,
        END_PAGE
    }

    public enum CONTENT_TYPE
    {
        ONBOARDING = -1,
        TUTO = 0,
        FREE,
        REAL
    }

    public enum CINEMA_TYPE
    {
        //NONE = -1,
        TICKET = 0,
        RESERVATION
    }

    public enum QUEST_ANSWER
    {
        WRONG = 0,
        RIGHT
    }
}
