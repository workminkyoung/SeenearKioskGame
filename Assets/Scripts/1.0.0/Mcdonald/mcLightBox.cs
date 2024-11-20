using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class mcLightBox : MonoBehaviour
{
    public TextMeshProUGUI _text;
    
    public void Setting()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(Animating());
    }

    public void ShowPrice(int price)
    {
        _text.text = "총 ￦" + price.ToString();
    }

    IEnumerator Animating()
    {
        float duration = 2.5f;
        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            yield return null;
        }
        
        gameObject.SetActive(false);
    }
}
