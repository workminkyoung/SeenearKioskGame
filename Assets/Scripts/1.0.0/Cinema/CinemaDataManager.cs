using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CinemaDataManager : MonoBehaviour
{
    public static CinemaDataManager instance = null; 

    //씬 정보------------------------
    public int sceneID = 0; //0=튜토리얼, 1=자유학습, 2=실전엽습
    public string reservationNumber = "1234567890";
    public int cinemaPracticeCase = 0; //0=티켓예매문제, 1=티켓구매문제

    //-------------------------------

    //영화 구매 정보
    public int movieThumnailID = 0;
    public string movieInfo = "";
    public int[] headCount = {0,0};
    public string seatNumber = "";
    public int payAmount = 0;

    private void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
            Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }
    }

    public void DataInitialize()
    {
        movieThumnailID = 0;
        movieInfo = "";
        headCount[0] = 0;
        headCount[1] = 0;
        seatNumber = "";
        payAmount = 0;
    }

    public void MovieInfoSave(string i, int id)
    {
        movieInfo = i;
        movieThumnailID = id;
    }

    public void HeadCountSave(int adult, int teenager)
    {
        headCount[0] = adult;
        headCount[1] = teenager;

        //청소년 12000원, 일반 14000원
        payAmount = (adult * 14000) + (teenager * 12000); 
    }

}
