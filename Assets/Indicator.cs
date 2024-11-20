using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Indicator : MonoBehaviour
{
    public Image img;
    private void OnEnable()
    {
        img.DOFade(0f, 0.01f);
        StartCoroutine(AnimCo());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        img.DOKill();
    }

    IEnumerator AnimCo()
    {
        img.DOFade(1f, 1f);
        yield return new WaitForSecondsRealtime(2f);
        img.DOFade(0f, 0.5f);
        yield return new WaitForSecondsRealtime(0.5f);

        StartCoroutine(AnimCo());
    }
}
