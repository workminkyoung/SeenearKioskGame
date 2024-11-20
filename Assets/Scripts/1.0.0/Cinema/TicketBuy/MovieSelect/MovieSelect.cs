using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;
using DG.Tweening;

public class MovieSelect : MonoBehaviour
{
    public GameObject movieSelectBtn;
    public GameObject timeSelectBtn;
    public TMP_Text[] dayTimeInfo;
    private string currentDayInfo;
    public GameObject[] movieInfoArray;
    public GameObject[] timeInfoArray;
    public Text[] movieInfo;
    
    [TextArea]
    public string currentMovieInfo;
    
    public Text[] movieTimeInfo;

    [TextArea]
    public string currentMovieTimeInfo;

    public Toggle[] selectToggleArray;

    private void OnEnable()
    {
        CinemaPageManager.instance.ticketBuyCurrentPageID = (int)TicketBuyPage.MovieSelectPage;
        
        SetDayTime();
        SelectMenu(0);
        selectToggleArray[0].isOn = true;
        selectToggleArray[3].isOn = true;

    }

    public void SetDayTime()
    {
        dayTimeInfo[0].text = DateTime.Now.ToString("yyyy.MM.dd (dddd)");
        dayTimeInfo[0].text = dayTimeInfo[0].text.Replace("요일", "");
        dayTimeInfo[1].text = DateTime.Now.AddDays(1).ToString("yyyy.MM.dd (dddd)");
        dayTimeInfo[1].text = dayTimeInfo[1].text.Replace("요일", "");
        
        dayTimeInfo[0].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 880, 0);
        dayTimeInfo[0].transform.localScale = Vector3.one;
        dayTimeInfo[0].color =  Vector4.one;
        
        dayTimeInfo[1].GetComponent<RectTransform>().anchoredPosition = new Vector3(290, 880, 0);
        dayTimeInfo[1].transform.localScale = Vector3.one * 0.7f;
        Color c = new Color32(198, 198, 198, 255);
        dayTimeInfo[1].color =  c;

        currentDayInfo = dayTimeInfo[0].text;
    }

    public void NextDayBtn()
    {
        if(dayTimeInfo[0].transform.localScale == Vector3.one)
        {
            return;
        }
        StartCoroutine(DayBtnAnim(1));
    }

    public void PreviousDayBtn()
    {
        if(dayTimeInfo[1].transform.localScale == Vector3.one)
        {
            return;
        }
        StartCoroutine(DayBtnAnim(0));
    }

    IEnumerator DayBtnAnim(int id) // id = 0 이전, id = 1 다음
    {
        if(id == 0)
        {
            currentDayInfo = dayTimeInfo[1].text;
            dayTimeInfo[1].GetComponent<RectTransform>().DOAnchorPosX(dayTimeInfo[1].GetComponent<RectTransform>().anchoredPosition.x - 290f, 0.7f);
            dayTimeInfo[0].GetComponent<RectTransform>().DOAnchorPosX(dayTimeInfo[0].GetComponent<RectTransform>().anchoredPosition.x - 290f, 0.7f);
            dayTimeInfo[0].transform.DOScale(Vector3.one * 0.8f, 0.7f);
            dayTimeInfo[1].transform.DOScale(Vector3.one, 0.7f);
            yield return new WaitForSecondsRealtime(0.3f);

            dayTimeInfo[1].color =  Vector4.one;

            Color c = new Color32(198, 198, 198, 255);
            dayTimeInfo[0].color =  c;
        }
        else
        {
            currentDayInfo = dayTimeInfo[0].text;
            dayTimeInfo[0].GetComponent<RectTransform>().DOAnchorPosX(dayTimeInfo[0].GetComponent<RectTransform>().anchoredPosition.x + 290f, 0.7f);
            dayTimeInfo[1].GetComponent<RectTransform>().DOAnchorPosX(dayTimeInfo[1].GetComponent<RectTransform>().anchoredPosition.x + 290f, 0.7f);
            dayTimeInfo[1].transform.DOScale(Vector3.one * 0.8f, 0.7f);
            dayTimeInfo[0].transform.DOScale(Vector3.one, 0.7f);
            yield return new WaitForSecondsRealtime(0.3f);

            dayTimeInfo[0].color =  Vector4.one;

            Color k = new Color32(198, 198, 198, 255);
            dayTimeInfo[1].color =  k;
        }        
    }

    public void SelectMenu(int id)
    {
        if(id == 0)
        {
            movieSelectBtn.SetActive(true);
            timeSelectBtn.SetActive(false);
            currentMovieTimeInfo = null;
            for(int i = 0; i < movieInfoArray.Length; i++)
            {
                movieInfoArray[i].SetActive(true);
            }
            
            for(int i = 0; i < timeInfoArray.Length; i++)
            {
                timeInfoArray[i].SetActive(false);
            }

            SelectMovie(0);
        }
        else
        {
            movieSelectBtn.SetActive(false);
            timeSelectBtn.SetActive(true);
            currentMovieInfo = null;
            for(int i = 0; i < movieInfoArray.Length; i++)
            {
                movieInfoArray[i].SetActive(false);
            }
            
            for(int i = 0; i < timeInfoArray.Length; i++)
            {
                timeInfoArray[i].SetActive(true);
            }

            SelectTime(0);
        }
    }

    public void SelectMovie(int id)
    {
        currentMovieInfo = currentDayInfo + "\n" + movieInfo[id].text;

        CinemaDataManager.instance.MovieInfoSave(currentMovieInfo, id);
    }

    public void SelectTime(int id)
    {
        currentMovieTimeInfo = null;
        int k = 0;
        if(id == 0 || id == 1 || id == 2)
        {
            k = 0;
            currentMovieTimeInfo = "위대한 소맨";
        }
        else if(id == 3 || id == 4 || id == 5)
        {
            k = 1;
            currentMovieTimeInfo = "외로운 갈매기";
        }
        else if(id == 6 || id == 7 || id == 8)
        {
            k = 2;
            currentMovieTimeInfo = "하지만, 꽃";
        }
        
        currentMovieTimeInfo += "\n" + currentDayInfo + "\n" + movieTimeInfo[id].text;

        CinemaDataManager.instance.MovieInfoSave(currentMovieTimeInfo, k);
    }
}
