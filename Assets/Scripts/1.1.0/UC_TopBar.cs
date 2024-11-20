using KioskApp.Tutorial;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UC_TopBar : MonoBehaviour
{
    [SerializeField]
    private GameObject _bubble;
    private UC_Character _character;    
    private List<TextMeshProUGUI> _texts = new List<TextMeshProUGUI>();
    private Button _btnExit;

    public void Init()
    {
        _character = GetComponentInChildren<UC_Character>(); ;
        _texts.AddRange(GetComponentsInChildren<TextMeshProUGUI>());
        _btnExit = GetComponentInChildren<Button>();

        _btnExit.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
        _character.Init();
    }

    public void SetTopContent(string content, string name)
    {
        _texts[(int)eText.Content].text = content;
        _texts[(int)eText.Name].text = name;
    }

    public void SetBubble(string text)
    {
        _texts[(int)eText.Bubble].text = text.Replace("\\n", "\n");
    }

    public void SetContentType(CONTENT_TYPE type)
    {
        switch (type)
        {
            case CONTENT_TYPE.TUTO:
                _bubble.SetActive(true);
                break;
            case CONTENT_TYPE.FREE:
                _bubble.SetActive(false);
                break;
            case CONTENT_TYPE.REAL:
                _bubble.SetActive(false);
                break;
            default:
                break;
        }

        _character.SetCharacter(type);
    }

    enum eText
    {
        Content = 0,
        Name,
        Bubble
    }
}
