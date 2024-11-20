using KioskApp.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KioskApp.Cinema;
using UnityEngine.SceneManagement;

public class UP_MainManager : MonoBehaviour
{
    public GameObject home;
    public GameObject menu;

    public Button _btnOnboarding;
    public List<Button> homeBtns = new List<Button>();
    public List<Button> menuBtns = new List<Button>();

    public Button _back;


    private void Awake()
    {
        menu.SetActive(false);

        if(Cinema_UserData.inst == null)
        {
            DontDestroyOnLoad(Cinema_UserData.Instance.gameObject);
        }

        _btnOnboarding.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(2);
        });

        for(int i = 0; i < homeBtns.Count; i++)
        {
            int index = i;
            homeBtns[i].onClick.AddListener(() =>
            {
                Cinema_UserData.inst._contentType = (CONTENT_TYPE)index;
                home.SetActive(false);
                menu.SetActive(true);
            });
        }


        for (int i = 0; i < menuBtns.Count; i++)
        {
            int index = i;
            menuBtns[i].onClick.AddListener(() =>
            {
                Cinema_UserData.inst._cinemaType = (CINEMA_TYPE)index;
                SceneManager.LoadScene(1);
            });
        }

        _back.onClick.AddListener(() =>
        {
            home.SetActive(true);
            menu.SetActive(false);
        });
    }
}
