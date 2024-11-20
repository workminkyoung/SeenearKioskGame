using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mc_GaugeCTR : MonoBehaviour, IGaugeSlide
{
    //public CircleSliderGauge gauge;
    public GameObject[] startBtn;
    [SerializeField]
    private List<Transform> listStamp = new List<Transform>();

    private void Awake()
    {
        listStamp.AddRange(UtilityExtensions.GetComponentsOnlyInChildrenByTag_NonRecursive<Transform>(transform, "stamp"));
        for (int i = 0; i < listStamp.Count; i++)
        {
            listStamp[i].gameObject.SetActive(false);
        }
    }

    void IGaugeSlide.PercentUpdate(int index)
    {
        //gauge.ProgressSlider(UserDataManager.userData.McProgress[index]);


        for (int i = 0; i < startBtn.Length; i++)
        {
            startBtn[i].SetActive(false);
        }
        startBtn[index].SetActive(true);
    }

    enum eStamp
    {
        off = 0,
        on
    }
}
