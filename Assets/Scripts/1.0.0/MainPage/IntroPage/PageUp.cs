using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PageUp : MonoBehaviour
{
    public GameObject[] textDummy = new GameObject[9];
    public RectTransform[] indicatorRectTr = new RectTransform[9]; 
    public Image[] indicatorimg = new Image[9];
    public Button btn;
    public int pageID = 0;
    
    Color32 btnActiveColor = new Color32(207,206,255,255);
    Color32 indicatorActiveColor = new Color32(126,129,239,255);
    Vector2 rectOriginSize = new Vector2(32, 32);
    Vector2 rectIndicatorActiveSize = new Vector2(80, 32);


    void Start()
    {
        pageID = 0;
    }

    public void BtnInteract()
    {
        AnimationReset();

        if(pageID == 8)
        {
            pageID = 0;
            PageManager.instance.PageMove(1);
        }
        else
        {
            pageID++;
        }

        TextAnimate(); //텍스트 표출
        IndicatorAnimate(); //인디케이터 애니메이션

        if(pageID > 4 && pageID < 8) //5초 페이지 강제 점유
        {
            PageOccupy();
        }
    }

    void AnimationReset()
    {
        StopAllCoroutines();
        btn.image.DOKill();
        for(int i = 0; i < textDummy.Length; i++)
        {
            textDummy[i].SetActive(false);
        };

        for(int i = 0; i < indicatorimg.Length; i++)
        {
            indicatorRectTr[i].DOSizeDelta(rectOriginSize, 0.3f);
            indicatorimg[i].DOColor(Color.white, 0.3f);
        };

        btn.image.DOColor(Color.white, 0);
    }

    void TextAnimate()
    {
        textDummy[pageID].SetActive(true);
    }
    
    void IndicatorAnimate()
    {
        indicatorRectTr[pageID].DOSizeDelta(rectIndicatorActiveSize, 0.3f);
        indicatorimg[pageID].DOColor(indicatorActiveColor, 0.3f);
    }

    void PageOccupy()
    {
        StartCoroutine(BtnInactiveCo());
    }

    public int delayTime;
    IEnumerator BtnInactiveCo()
    {
        btn.interactable = false;
        yield return new WaitForSecondsRealtime(delayTime);
        btn.interactable = true;
        
        // btn.image.DOColor(btnActiveColor, 0.5f);
        // yield return new WaitForSecondsRealtime(0.5f);
        // btn.image.DOColor(Color.white, 0.5f);
        // yield return new WaitForSecondsRealtime(0.5f);        
        // btn.image.DOColor(btnActiveColor, 0.5f);
        // yield return new WaitForSecondsRealtime(0.5f);
        // btn.image.DOColor(Color.white, 0.5f);

        btn.image.DOFade(0f, 0.5f);
        yield return new WaitForSecondsRealtime(0.5f);        
        btn.image.DOFade(1f, 0.5f);        
        yield return new WaitForSecondsRealtime(0.5f);   
        btn.image.DOFade(0f, 0.5f);
        yield return new WaitForSecondsRealtime(0.5f);        
        btn.image.DOFade(1f, 0.5f);       
    }
}
