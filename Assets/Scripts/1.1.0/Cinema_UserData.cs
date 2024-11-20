using KioskApp.Tutorial;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KioskApp.Cinema
{
    public class Cinema_UserData : SingletonBehaviour<Cinema_UserData>
    {
        public Movie _movie = new Movie();
        public Movie _questMovie = new Movie();

        public Reservation _reservation = new Reservation();
        public Reservation _questReservation = new Reservation();

        public QUEST_ANSWER _answer;
        public CINEMA_TYPE _cinemaType;
        public CONTENT_TYPE _contentType;

        protected override void Init()
        {

        }
    }

    [Serializable]
    public class Movie
    {
        public string _name;
        public Texture _thumbnail;
        public string _time;
        public int _adultNum;
        public int _teenNum;
        public List<string> _selecSeats = new List<string>();
    }

    public class TextData
    {
        #region ticket
        public static string[] _names =
        {
            "위대한 소맨",
            "외로운 갈매기",
            "하지만, 꽃"
        };

        public static string[] _times =
        {
            "10:20~12:20",
            "13:50~15:50",
            "15:30~17:30"
        };

        public const int _numMax = 4;

        public static string[] _seatAlphabet =
        {
            "A", "B", "C", "D", "E"
        };

        public const int _seatMax = 8;

        public static string[] _questTitle =
        {
            "영화제목",
            "영화시간",
            "관람인원",
            "좌석위치"
        };

        public static string[] _occupiedSeats =
        {
            "B7", "B8",
            "C4","C5",
            "E4", "E5","E7", "E8"
        };
        #endregion

        #region reservation
        public const string _tutoReserveNum = "02184772";
        #endregion
    }

    enum QUEST
    {
        TITLE = 0,
        TIME,
        NUM,
        SEAT
    }

    [Serializable]
    public class Reservation
    {
        public string _number;
    }
}