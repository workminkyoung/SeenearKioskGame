using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HighlightEffect : MonoBehaviour
{
    public Image[] highlightArray;

    private void OnEnable()
    {
        HighlightAnimation();
    }

    private void OnDisable()
    {
        StopAllCoroutines();

        highlightArray[0].DOKill();
        highlightArray[1].DOKill();
        highlightArray[2].DOKill();
        highlightArray[0].color = new Vector4(1, 1, 1, 0);
        highlightArray[1].color = new Vector4(1, 1, 1, 0);
        highlightArray[2].color = new Vector4(1, 1, 1, 0);
    }

    public void HighlightAnimation()
    {
        StartCoroutine(LightAnimCo());
    }

    public IEnumerator LightAnimCo()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        highlightArray[0].DOFade(1f, 0.8f);
        highlightArray[1].DOFade(1f, 0.8f).SetDelay(0.2f);
        highlightArray[2].DOFade(1f, 0.8f).SetDelay(0.4f);

        yield return new WaitForSecondsRealtime(1.2f);

        highlightArray[0].DOFade(0f, 0.5f);
        highlightArray[1].DOFade(0f, 0.5f);
        highlightArray[2].DOFade(0f, 0.5f);

        yield return new WaitForSecondsRealtime(1.2f);


        StartCoroutine(LightAnimCo());
    }
}
