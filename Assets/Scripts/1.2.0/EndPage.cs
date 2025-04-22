using DG.Tweening;
using LottiePlugin.UI;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class EndPage : MonoBehaviour
{
    [SerializeField]
    private Image _btn1, _btn2;
    [SerializeField]
    private AnimatedImage _success;

    public void ResetPage()
    {
        _btn1.color = new Color(1, 1, 1, 0);
        _btn2.color = new Color(1, 1, 1, 0);
        gameObject.SetActive(false);
    }

    public void Active()
    {
        gameObject.SetActive(true);
        _success.Play();

        _btn1.DOFade(1, 1).SetDelay(0.5f);
        _btn2.DOFade(1, 1).SetDelay(0.5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _success.Play();
        }
    }
}
