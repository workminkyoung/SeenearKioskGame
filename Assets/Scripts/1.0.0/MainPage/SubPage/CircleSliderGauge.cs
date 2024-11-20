using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleSliderGauge : MonoBehaviour
{
    public Image guage;
    public Text percentTxt;
    public int percent;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProgressSlider(int p)
    {
        StartCoroutine(progressAnimCo(p));
    }

    IEnumerator progressAnimCo(int s)
    {
        print(s);
        yield return new WaitForSecondsRealtime(0.4f);
        int p = 0;
        while(p < s)
        {
            p ++;
            percentTxt.text = p.ToString() + "%";
            guage.fillAmount = p * 0.01f;
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    private void OnDisable()
    {
        guage.fillAmount = 0;
        percentTxt.text = "0%";
    }
}