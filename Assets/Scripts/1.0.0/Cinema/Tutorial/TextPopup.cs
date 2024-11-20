using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextPopup : MonoBehaviour
{
    [TextArea]
    public List<string> popuptextList;
    
    public Text currentPopupText;

    private void Start()
    {
        bool isOn = false;
        bool isOnTwo = false;
        currentPopupText.text = null;
        NextTypig();
    }

    public void NextTypig()
    {
        currentPopupText.DOKill();
        currentPopupText.text = null;

        TutorialManager.instance.PageSet(TutorialManager.instance.currentPopupId);
        if(TutorialManager.instance.currentPopupId != 29)
        {
            TutorialManager.instance.currentPopupId++;
            currentPopupText.DOText(popuptextList[TutorialManager.instance.currentPopupId - 1], 2f).SetEase(Ease.Linear);
        }
    }

    public void TutoOpen()
    {
        TutorialManager.instance.TutorialOpen();

        currentPopupText.DOKill();
        currentPopupText.text = null;
        
        currentPopupText.DOText(popuptextList[TutorialManager.instance.currentPopupId - 1], 2f).SetEase(Ease.Linear);
    }

    public bool isOn = false;
    public bool isOnTwo = false;
    public bool isOnThree = false;
    public bool isOnFour = false;
    public bool isFirst = false;
    public bool isFinish = false;
    public void SeatCountCheck(int k)
    {
        if(k == 1)
        {
            isOn = !isOn;
        }
        else if(k == 2)
        {
            isOnTwo = !isOnTwo;
        }
        else if(k == 3)
        {
            if(isFirst)
            {
                isOnThree = true;
            }
            else
            {
                isOnThree = true;
                isFirst = true;
                
                TutoOpen();
            }
        }
        else if(k == 4)
        {
            if (isFirst)
            {
                isOnFour = true;
            }
            else
            {
                isOnFour = true;
                isFirst = true;
                
                TutoOpen();
            }
        }


        if (isOn == true && isOnTwo == true)
        {
            TutoOpen();
        }
        else if(!isOn && !isOnTwo && isFirst)
        {
            if(!isOnFour || !isOnThree)
            {
                isFinish = true;
                TutorialManager.instance.SeatCancelPageBtn[98].interactable = true;
                TutorialManager.instance.SeatCancelPageBtn[99].interactable = true;
                TutorialManager.instance.AnotherSeatSelectPageBtn[94].interactable = false;
                TutorialManager.instance.AnotherSeatSelectPageBtn[95].interactable = false;
                TutorialManager.instance.HighlightPosSet("10, -140"); //F6,7
            }
        }

        if(isOnFour && isOnThree && isFirst && isFinish)
        {
            TutorialManager.instance.SeatCancelPageBtn[100].interactable = true;
            TutorialManager.instance.HighlightPosSet("340, -820"); //������ư
        }
    }
}
