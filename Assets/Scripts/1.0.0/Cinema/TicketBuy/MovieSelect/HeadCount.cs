using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class HeadCount : MonoBehaviour
{
    public int adultCount;
    public int teenagerCount;
    public GameObject[] selectAdultToggleArray;
    public GameObject[] selectTeenagerToggleArray;

    private void OnEnable()
    {
        for(int i = 0; i < selectAdultToggleArray.Length; i++)
        {
            selectAdultToggleArray[i].SetActive(false);   
            selectTeenagerToggleArray[i].SetActive(false);   
        }
        
        selectTeenagerToggleArray[0].SetActive(true); 
        selectAdultToggleArray[0].SetActive(true);
        
        adultCount = 0;
        teenagerCount = 0;
    }

    private void OnDisable() 
    {
        CinemaDataManager.instance.HeadCountSave(adultCount, teenagerCount);
    }

    public void AdultCountToggle()
    {
        if(EventSystem.current.currentSelectedGameObject.name.All(char.IsDigit))
        {
            for(int i = 0; i < selectAdultToggleArray.Length; i++)
            {
                selectAdultToggleArray[i].SetActive(false);     
            }

            EventSystem.current.currentSelectedGameObject.transform.GetChild(1).gameObject.SetActive(true); 

            adultCount = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        }
        else
        {
            adultCount = 0;
        }
    }

    public void TeenagerCountToggle()
    {
        if(EventSystem.current.currentSelectedGameObject.name.All(char.IsDigit))
        {
            for(int i = 0; i < selectAdultToggleArray.Length; i++)
            {
                selectTeenagerToggleArray[i].SetActive(false);     
            }

            EventSystem.current.currentSelectedGameObject.transform.GetChild(1).gameObject.SetActive(true); 

            teenagerCount = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        }
        else
        {
            teenagerCount = 0;
        }
    }
}
