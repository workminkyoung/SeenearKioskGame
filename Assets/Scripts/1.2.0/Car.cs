using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> _sprites;
    [SerializeField]
    private Image _image;

    public void Init()
    {
        _image = GetComponent<Image>();
        _image.sprite = _sprites[0];
    }
}
