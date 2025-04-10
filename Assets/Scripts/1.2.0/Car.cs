using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Car : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
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

    private float _posY;
    private bool _isRefueling = false;

    public void Init()
    {
        _carRect = GetComponent<RectTransform>();
        _image.sprite = _sprites[0];
        _posY = _carRect.anchoredPosition.y;
    }

    public void CarSpawn(int carType)
    {
        _image.sprite = _sprites[carType];
        _image.SetNativeSize();
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
        }));
    }

    public void StartMoveOut()
    {
        StartCoroutine(MoveTo(_carRect, new Vector2(-540 - _image.rectTransform.sizeDelta.x / 2, _posY), _moveDuration, () =>
        {
            _isRefueling = false;
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
    }

    // UI 요소에서 손을 뗄 때 호출됨
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_isRefueling) return;
        _guide.SetActive(true);
    }
}
