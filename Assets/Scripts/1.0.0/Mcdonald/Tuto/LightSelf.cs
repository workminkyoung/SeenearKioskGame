using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LightSelf : MonoBehaviour
{
    private List<Image> _listImg = new List<Image>();
    private Coroutine _effect;

    private void Awake()
    {
        _listImg.AddRange(GetComponentsInChildren<Image>());
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        gameObject.SetActive(true);
        StartEffect();
    }
    
    public void StartEffect()
    {
        if (_effect != null)
            StopCoroutine(_effect);

        _effect = StartCoroutine(LightEffect());
    }

    public void StopEffect(bool isShow = true)
    {
        if (_effect != null)
            StopCoroutine(_effect);
        
        gameObject.SetActive(isShow);
    }

    IEnumerator LightEffect()
    {
        //temp anim
        while (gameObject.activeSelf)
        {
            for (int i = 0; i < _listImg.Count; i++)
            {
                _listImg[i].DOFade(1-_listImg[i].color.a, 0.3f* (i + 1));
            }
            yield return new WaitForSecondsRealtime(0.9f);
            for (int i = _listImg.Count-1; i >= 0; i--)
            {
                _listImg[i].DOFade(1-_listImg[i].color.a, 0.3f* (i + 1));
            }
            yield return new WaitForSecondsRealtime(0.9f);
        }
    }
}
