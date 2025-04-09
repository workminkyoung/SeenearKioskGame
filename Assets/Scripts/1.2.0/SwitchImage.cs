using UnityEngine;
using UnityEngine.UI;

public class SwitchImage : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _sprites;
    [SerializeField]
    private Image _image;

    public virtual void Init()
    {
        _image = GetComponent<Image>();
    }

    public virtual void Switch()
    {
        _image.sprite = _image.sprite == _sprites[(int)SpriteType.Off] ? _sprites[(int)SpriteType.On] : _sprites[(int)SpriteType.Off];
    }

    private enum SpriteType
    {
        Off,
        On
    }
}
