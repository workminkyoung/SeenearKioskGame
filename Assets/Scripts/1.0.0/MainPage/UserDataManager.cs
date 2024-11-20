using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserDataManager : MonoBehaviour
{
    public static UserData userData = new UserData();
    public static UserDataManager instance = null;

    private void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
            DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }

        DataLoad();
    }
    
    public void DataLoad()
    {
        userData.isFirstStart = Convert.ToBoolean(PlayerPrefs.GetInt("isFirstStart", Convert.ToInt16(true))); // true=1, false=0

        if (!userData.isFirstStart)
        {
            // get value from each type of contents
            for (int i = 0; i < (int)eContent.Length; i++)
            {
                for (int j = 0; j < (int)eTrainingType.Length; j++)
                {
                    // set key with content type and training type
                    string key = ((eContent)i).ToString() + ((eTrainingType)j).ToString();
                    int value = PlayerPrefs.GetInt(key);
                    // dict key is same with playerprefs key
                    userData.contentProgress[key] = value;
                }
            }
        }
        else
        {
            //initialize data to 0
            for (int i = 0; i < (int)eContent.Length; i++)
            {
                for (int j = 0; j < (int)eTrainingType.Length; j++)
                {
                    // set key with content type and training type
                    string key = ((eContent)i).ToString() + ((eTrainingType)j).ToString();
                    // dict key is same with playerprefs key, initialize 0
                    userData.contentProgress[key] = 0;
                }
            }
            DataSave();
        }
    }

    public void DataSave() 
    {
        PlayerPrefs.SetInt("isFirstStart", Convert.ToInt16(userData.isFirstStart)); // true=1, false=0

        for (int i = 0; i < (int)eContent.Length; i++)
        {
            for (int j = 0; j < (int)eTrainingType.Length; j++)
            {
                // set key with content type and training type
                string key = ((eContent)i).ToString() + ((eTrainingType)j).ToString();
                // dict key is same with playerprefs key
                PlayerPrefs.SetInt(key, userData.contentProgress[key]);
            }
        }
    }

    public void DataSave(eContent content, eTrainingType trainingType, bool isEnded)
    {
        string key = content.ToString() + trainingType.ToString();
        userData.contentProgress[key] = Convert.ToInt16(isEnded);
        PlayerPrefs.SetInt(key, userData.contentProgress[key]);
    }

    private void OnApplicationQuit()
    {
        DataSave();
    }

    // void OnApplicationPause(bool pause)
    // {
    //     if (pause) //앱이 비활성화 일 때
    //     {
    //         DataSave();
    //     }
    //     else
    //     {
    //         DataLoad();
    //     }
    // }
}

public class UserData
{
    //최초 실행 시에만 가이드 팝업
    public bool isFirstStart;
    public Dictionary<string, int> contentProgress = new Dictionary<string, int>();
}

// scene index for load scene with num
public enum eScene
{
    Main = 0,
    Mcdonald,
    Cinema
}

public enum eContent
{
    Mcdonald = 0,
    Cinema,

    Length
}

// training type index
// 0=튜토리얼, 1=자유학습, 2=실전엽습
public enum eTrainingType
{
    Tutorial = 0,
    FreeLearn,
    Practice,

    Length
}
