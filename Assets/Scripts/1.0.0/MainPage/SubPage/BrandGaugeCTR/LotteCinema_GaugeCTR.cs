using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotteCinema_GaugeCTR : MonoBehaviour, IGaugeSlide
{
    //public CircleSliderGauge gauge;
    public GameObject[] startBtn;

    void IGaugeSlide.PercentUpdate(int index)
    {
        //gauge.ProgressSlider(UserDataManager.userData.LotteCinemaProgress[index]);

        for(int i = 0; i < startBtn.Length; i++)
        {
            startBtn[i].SetActive(false);   
        }
        startBtn[index].SetActive(true);
    }
}
