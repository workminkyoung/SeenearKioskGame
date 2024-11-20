using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpDownOption : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private List<Button> _listBtn = new List<Button>();
    private int _count = 1;

    public int Count
    {
        get { return _count; }
    }

    public void Setting()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _listBtn.AddRange(GetComponentsInChildren<Button>());
        
        _listBtn[(int)eBtn.Up].onClick.AddListener(Up);
        _listBtn[(int)eBtn.Down].onClick.AddListener(Down);
    }

    public void Init()
    {
        _count = 1;
        _text.text = _count.ToString();
    }

    void Up()
    {
        _count++;
        _text.text = _count.ToString();
    }

    void Down()
    {
        if(_count <= 0)
            return;
        
        _count--;
        _text.text = _count.ToString();
    }

    enum eBtn
    {
        Up = 0,
        Down
    }
}
