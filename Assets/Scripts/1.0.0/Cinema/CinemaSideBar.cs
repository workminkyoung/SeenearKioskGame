using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class CinemaSideBar : MonoBehaviour
{
    bool isOpenSideBar = false;

    public GameObject sidebarIcon;
    public GameObject[] boradArray;
    public GameObject[] boardTextArray;

    void Start()
    {
        isOpenSideBar = false;
    }

    public void OnTouchSideBar()
    {
        switch(CinemaDataManager.instance.sceneID)
        {
            case 0 :
                boradArray[0].SetActive(true);
                boradArray[1].SetActive(false);            
                boardTextArray[0].SetActive(true);
                boardTextArray[1].SetActive(false);
                break;
            case 1 : 
                boradArray[0].SetActive(true);
                boradArray[1].SetActive(false);                
                boardTextArray[0].SetActive(false);
                boardTextArray[1].SetActive(true);
                break;
            case 2 :
                boradArray[0].SetActive(false);
                boradArray[1].SetActive(true);   

                if(CinemaDataManager.instance.cinemaPracticeCase == 0)
                {
                    boardTextArray[3].SetActive(true);
                    boardTextArray[4].SetActive(false);
                } 
                else
                {
                    boardTextArray[3].SetActive(false);
                    boardTextArray[4].SetActive(true);
                }
                break;
        }


        isOpenSideBar = !isOpenSideBar;

        if(isOpenSideBar)
        {
            sidebarIcon.transform.rotation = Quaternion.Euler(0, 180, 0);
            gameObject.GetComponent<RectTransform>().DOAnchorPosX(600f, 0.5f).SetEase(Ease.Linear);
        }
        else
        {
            sidebarIcon.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.GetComponent<RectTransform>().DOAnchorPosX(50f, 0.5f).SetEase(Ease.Linear);
        }
    }

    public void Continue()
    {
        isOpenSideBar = !isOpenSideBar;
        sidebarIcon.transform.rotation = Quaternion.Euler(0, 0, 0);
        gameObject.GetComponent<RectTransform>().DOAnchorPosX(50f, 0.5f).SetEase(Ease.Linear);
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainPage");
    }

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
