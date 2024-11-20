using KioskApp.Tutorial;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UP_EndPage : UP_PageBase
{
    private Button _exitBtn;
    private UC_Bills _bills;
    private UC_Check _missionChecker;
    private TextMeshProUGUI _textComplete;
    [SerializeField]
    private List<GameObject> _characters = new List<GameObject>();
    private CINEMA_TYPE _cinemaType;

    private const string _completeGood = "학습완료!";
    private const string _completeBad = "미션실패";

    public void SetCinemaType(CINEMA_TYPE type)
    {
        _cinemaType = type;
    }

    public override void Init()
    {
        _exitBtn = UtilityExtensions.GetComponentOnlyInChildren_NonRecursive<Button>(transform);
        _bills = GetComponentInChildren<UC_Bills>();
        _missionChecker = GetComponentInChildren<UC_Check>();
        _textComplete = UtilityExtensions.GetComponentOnlyInChildren_NonRecursive<TextMeshProUGUI>(transform);
        base.Init();

        _bills.Show(false);
        _missionChecker.Init();
        _missionChecker.Show(false);
    }

    protected override void BindDelegate()
    {
        base.BindDelegate();
        _exitBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }

    protected override void TutoOnPageEnable()
    {
        base.TutoOnPageEnable();
        _textComplete.text = _completeGood;

        for (int i = 0; i < _characters.Count; i++)
        {
            _characters[i].SetActive(false);
        }
        _characters[0].SetActive(true);
    }

    protected override void FreeOnPageEnable()
    {
        base.FreeOnPageEnable();
        _bills.Init();
        _bills.Show(true);
        _textComplete.text = _completeGood;
        
        for (int i = 0; i < _characters.Count; i++)
        {
            _characters[i].SetActive(false);
        }
        _characters[0].SetActive(true);
    }

    protected override void RealOnPageEnable()
    {
        base.RealOnPageEnable();
        _global.Quest.Show(false);
        _missionChecker.Show(true);
        bool isRight = false;
        if (_cinemaType == CINEMA_TYPE.TICKET)
        {
            isRight = _missionChecker.CheckAnswer();
        }
        else
        {
            isRight = _missionChecker.CheckAnswer_reservation();
        }


        if (isRight)
        {
            _textComplete.text = _completeGood;

            for (int i = 0; i < _characters.Count; i++)
            {
                _characters[i].SetActive(false);
            }
            _characters[0].SetActive(true);
        }
        else
        {
            _textComplete.text = _completeBad;

            for (int i = 0; i < _characters.Count; i++)
            {
                _characters[i].SetActive(false);
            }
            _characters[1].SetActive(true);
        }
    }

    //TODO : scene change 
}
