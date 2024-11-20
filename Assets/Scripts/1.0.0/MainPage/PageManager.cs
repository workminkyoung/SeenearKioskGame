using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public static PageManager instance = null;
    public GameObject[] pageArray;
    public eTrainingType curTraingingType;

    public eTrainingType CurTrainngType
    {
        get { return curTraingingType; }
        set { curTraingingType = value; }
    }

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

    void Start()
    {
        if (UserDataManager.userData.isFirstStart)
        {
            PageMove(0);
            UserDataManager.userData.isFirstStart = false;
        }
        else
        {
            PageMove(1);
        }
    }

    public void PageMove(int id)
    {
        for (int i = 0; i < pageArray.Length; i++)
        {
            pageArray[i].SetActive(false);
        };

        pageArray[id].SetActive(true);
    }
}
