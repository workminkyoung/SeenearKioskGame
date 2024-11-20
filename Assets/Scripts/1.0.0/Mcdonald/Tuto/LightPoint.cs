using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LightPoint : MonoBehaviour
{
    private List<Image> _listImg = new List<Image>();
    private RectTransform _rect;
    private Coroutine _effect, _arrowEffect;
    
    public void Setting()
    {
        _listImg.AddRange(GetComponentsInChildren<Image>());
        _rect = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    public void SetState(bool state)
    {
        gameObject.SetActive(state);
        if (state)
        {
            // for (int i = 0; i < _listImg.Count; i++)
            // {
            //     _listImg[i].DOFade(1, 0.3f* (i + 1));
            // }
            _effect = StartCoroutine(LightEffect());
        }
        else
        {
            for (int i = 0; i < (int)eImg.LightMax; i++)
            {
                _listImg[i].color = new Color(1, 1, 1, 0);
            }
        }
    }
    
    public void SetState(bool state, Vector2 pos, string type)
    {
        gameObject.SetActive(state);
        bool isScroll = false;
        
        if (type == McdonaldProperties.ltype_scroll)
            isScroll = true;
        
        if (state)
        {
            StartEffect(isScroll);
            _rect.anchoredPosition = pos;
        }
        else
        {
            StopEffect();
            for (int i = 0; i < _listImg.Count; i++)
            {
                _listImg[i].color = new Color(1, 1, 1, 0);
            }
        }
    }

    public void StartEffect(bool isScroll = false)
    {
        if (_effect != null)
            StopCoroutine(_effect);
        _effect = StartCoroutine(LightEffect());

        if (isScroll)
        {
            // if(_arrowEffect != null)
            //     StopCoroutine(_arrowEffect);
            
            for (int i = (int)eImg.LightMax; i < (int)eImg.ArrowMax; i++)
            {
                _listImg[i].DOFade(1-_listImg[i].color.a, 0.3f* (i + 1));
            }
            //_arrowEffect = StartCoroutine(ArrowEffect());
        }
    }

    public void StopEffect()
    {
        if (_effect != null)
            StopCoroutine(_effect);
        if(_arrowEffect != null)
            StopCoroutine(_arrowEffect);
    }

    IEnumerator LightEffect()
    {
        //temp anim
        while (gameObject.activeSelf)
        {
            for (int i = 0; i < (int)eImg.LightMax; i++)
            {
                _listImg[i].DOFade(1-_listImg[i].color.a, 0.3f* (i + 1));
            }
            yield return new WaitForSecondsRealtime(0.9f);
            
            for (int i = (int)eImg.LightMax-1; i >= 0; i--)
            {
                _listImg[i].DOFade(1-_listImg[i].color.a, 0.3f* (i + 1));
            }
            yield return new WaitForSecondsRealtime(0.9f);
            
            if(gameObject.activeSelf == false)
                Debug.Log(gameObject.activeSelf);
        }
    }
    
    IEnumerator ArrowEffect()
    {
        //temp anim
        for (int i = (int)eImg.LightMax; i < (int)eImg.ArrowMax; i++)
        {
            _listImg[i].DOFade(1-_listImg[i].color.a, 0.3f* (i + 1));
            yield return null;
        }
        // while (gameObject.activeSelf)
        // {
        //     for (int i = (int)eImg.LightMax; i < (int)eImg.ArrowMax; i++)
        //     {
        //         _listImg[i].DOFade(1-_listImg[i].color.a, 0.3f* (i + 1));
        //     }
        //     yield return new WaitForSecondsRealtime(0.9f);
        //     
        //     for (int i = _listImg.Count-1; i >= (int)eImg.LightMax; i--)
        //     {
        //         _listImg[i].DOFade(1-_listImg[i].color.a, 0.3f* (i + 1));
        //     }
        //     yield return new WaitForSecondsRealtime(0.9f);
        //     
        //     if(gameObject.activeSelf == false)
        //         Debug.Log(gameObject.activeSelf);
        // }
    }

    enum eImg
    {
        LightMax = 3,
        ArrowMax = 6
    }
}
