using KioskApp.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UC_Character : MonoBehaviour
{
    private List<Image> _characters = new List<Image>();
    
    public void Init()
    {
        _characters.AddRange(GetComponentsInChildren<Image>());
    }

    public void SetCharacter(CONTENT_TYPE type)
    {
        for (int i = 0; i < _characters.Count; i++)
        {
            _characters[i].gameObject.SetActive(false);
        }

        switch (type)
        {
            case CONTENT_TYPE.TUTO:
                _characters[(int)CHARACTER.TUTO].gameObject.SetActive(true);
                break;
            case CONTENT_TYPE.FREE:
                _characters[(int)CHARACTER.ELSE].gameObject.SetActive(true);
                break;
            case CONTENT_TYPE.REAL:
                _characters[(int)CHARACTER.ELSE].gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    enum CHARACTER
    {
        TUTO = 0,
        ELSE
    }
}
