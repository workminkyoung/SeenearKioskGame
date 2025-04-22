using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ProgressBar : MonoBehaviour
{
    public Action OnEnd;

    [SerializeField] private Image _fillImage;
    [SerializeField] private float _duration = 3f;
    private bool _isPaused = false;
    private float _amount = 995f;


    public void Pause() => _isPaused = true;
    public void Resume() => _isPaused = false;

    public void StartFill()
    {
        StartCoroutine(FillOverTime());
    }

    private IEnumerator FillOverTime()
    {
        float elapsed = 0f;
        float value = -_amount;
        _fillImage.rectTransform.anchoredPosition = new Vector2(value, 0);

        while (elapsed < _duration)
        {
            if (!_isPaused)
            {
                elapsed += Time.deltaTime;
                value = UtilityExtensions.Remap(elapsed, 0, _duration, -_amount, 0);
                _fillImage.rectTransform.anchoredPosition = new Vector2(value, 0);
            }

            yield return null;
        }

        _fillImage.rectTransform.anchoredPosition = Vector2.zero;
        OnEnd?.Invoke();
    }

    public void ResetProgress()
    {
        _fillImage.rectTransform.anchoredPosition = new Vector2(-_amount, 0); 
        _isPaused = true;
    }
}
