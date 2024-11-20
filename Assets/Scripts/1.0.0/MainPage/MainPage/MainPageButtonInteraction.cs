using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPageButtonInteraction : MonoBehaviour
{
    public GameObject[] pressedImgArray;
    public GameObject[] subPageArray;

    public GameObject scrollViewContent;
    public void ButtonPress(int id)
    {
        if (id == 3)
        {
            PageManager.instance.PageMove(3);
        }
        else
        {
            PageManager.instance.CurTrainngType = (eTrainingType)id;
            PageManager.instance.PageMove(2);
            //SubPageMove(id);
        }
    }

    //set subpage
    public void SubPageMove(int id)
    {
        subPageArray[id].SetActive(true);

        var childrenContentSlides = scrollViewContent.GetComponentsInChildren<IGaugeSlide>();

        for (int i = 0; i < scrollViewContent.transform.childCount; i++)
        {
            childrenContentSlides[i].PercentUpdate(id);
        }
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }
}

