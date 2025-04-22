using LottiePlugin.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Car : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Action OnGameEnd;

    [SerializeField]
    private List<Sprite> _sprites;
    [SerializeField]
    private Image _image;
    [SerializeField]
    private RectTransform _carRect;
    [SerializeField]
    private GameObject _guide;
    [SerializeField]
    private RectTransform _canvas;
    [SerializeField]
    private float _moveDuration;
    [SerializeField]
    private AnimatedImage _success;
    [SerializeField]
    private AnimatedImage _warning;
    [SerializeField]
    private int _maxCar = 3;

    public ProgressBar _progressBar;

    private float _posY;
    [SerializeField]
    private bool _isRefueling = false;
    private int _curIndex = 0;

    public void Init()
    {
        _carRect = GetComponent<RectTransform>();
        _image.sprite = _sprites[0];
        _posY = _carRect.anchoredPosition.y;

        _progressBar.OnEnd += StartMoveOut;
        _warning.RawImage.enabled = false;
    }

    public void CarSpawn(int carType)
    {
        _image.sprite = _sprites[carType];
        //_image.SetNativeSize();
        _carRect.anchoredPosition = new Vector2(540 + _image.rectTransform.sizeDelta.x/2, _posY);
        _guide.SetActive(false); 
        _isRefueling = false;
    }

    public void StartMoveIn()
    {
        StartCoroutine(MoveTo(_carRect, new Vector2(0, _posY), _moveDuration, () =>
        {
            _guide.SetActive(true); 
            _isRefueling = true;
            _progressBar.Pause();
            _progressBar.StartFill();
        }));
    }

    public void StartMoveOut()
    {
        _isRefueling = false;
        _guide.SetActive(false);
        _success.Play();

        StartCoroutine(MoveTo(_carRect, new Vector2(-540 - _image.rectTransform.sizeDelta.x / 2, _posY), _moveDuration, () =>
        {
            if (_curIndex < _maxCar - 1)
            {
                _curIndex++;
                CarSpawn(_curIndex);
                StartMoveIn();
                _progressBar.ResetProgress();
            }
            else
            {
                OnGameEnd?.Invoke();
                // end Game
            }
        }));
    }

    private IEnumerator MoveTo(RectTransform rect, Vector2 targetPos, float duration, Action action = null)
    {
        Vector2 startPos = rect.anchoredPosition;
        float time = 0f;

        while (time < duration)
        {
            rect.anchoredPosition = Vector2.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        rect.anchoredPosition = targetPos;
        action?.Invoke();
    }

    // UI 요소를 누를 때 호출됨
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_isRefueling) return;
        _guide.SetActive(false);
        _progressBar.Resume();
        _warning.Stop();
        _warning.RawImage.enabled = false;

    }

    // UI 요소에서 손을 뗄 때 호출됨
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_isRefueling) return;
        _guide.SetActive(true);
        _progressBar.Pause();
        _warning.Play();
        _warning.RawImage.enabled = true;
    }
}
