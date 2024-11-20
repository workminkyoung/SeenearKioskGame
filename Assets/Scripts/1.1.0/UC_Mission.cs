using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UC_Mission : MonoBehaviour
{
    private List<TextMeshProUGUI> _texts = new List<TextMeshProUGUI>();
    [SerializeField]
    private GameObject _correct;
    [SerializeField]
    private GameObject _wrong;

    public void Init()
    {
        _texts.AddRange(GetComponentsInChildren<TextMeshProUGUI>());
    }

    public void SetMission(string category, string name)
    {
        _texts[(int)TEXT.CATEGORY].text = category + " :";
        _texts[(int)TEXT.NAME].text = name;

        _wrong.SetActive(false);
        _correct.SetActive(false);
    }

    public void InitAnswer()
    {
        _wrong.SetActive(false);
        _correct.SetActive(false);
    }

    public void Wrong()
    {
        _correct.SetActive(false);
        _wrong.SetActive(true);
    }

    public void Correct()
    {
        _correct.SetActive(true);
        _wrong.SetActive(false);
    }

    enum TEXT
    {
        CATEGORY = 0,
        NAME
    }
}
