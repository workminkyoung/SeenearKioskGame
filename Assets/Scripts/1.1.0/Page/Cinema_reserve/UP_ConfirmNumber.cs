using KioskApp.Cinema;
using KioskApp.Tutorial;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UP_ConfirmNumber : UP_PageBase
{
    private TMP_InputField _inputField;
    [SerializeField]
    private List<Button> _numBtns = new List<Button>();
    [SerializeField]
    private Button _removeBtn;
    [SerializeField]
    private Button _removeAllBtn;

    //popup check
    [SerializeField]
    private GameObject _popupCheck;
    [SerializeField]
    private TextMeshProUGUI _textCheckNum;
    [SerializeField]
    private Button _btnConfirm;
    [SerializeField]
    private Button _btnCancel;

    //popup load
    [SerializeField]
    private GameObject _popupLoad;
    [SerializeField]
    private Button _btnCancelLoad;
    private Coroutine _loadNextPage;

    //tuto
    [SerializeField]
    private TutorialDataTable[] _tutoDataPopup;
    [SerializeField]
    private GameObject[] _highlightPopup;
    private TutorialDataTable _curData;
    private RectTransform _curRect;
    private POINT_SIZE _curAlertSize = POINT_SIZE.M;

    public override void Init()
    {
        _inputField = GetComponentInChildren<TMP_InputField>();
        base.Init();
    }

    protected override void BindDelegate()
    {
        base.BindDelegate();

        for (int i = 0; i < _numBtns.Count; i++)
        {
            int index = i;
            _numBtns[i].onClick.AddListener(() =>
            {
                if(_inputField.text.Length < 8)
                    _inputField.text += index.ToString();
            });
        }
        
        _removeBtn.onClick.AddListener(() =>
        {
            if(_inputField.text.Length > 0)
                _inputField.text = _inputField.text.Substring(0, _inputField.text.Length - 1);
        });
        
        _removeAllBtn.onClick.AddListener((() =>
        {
            _inputField.text = "";
        }));

        _btnCancel.onClick.AddListener(() =>
        {
            _highlightPopup[1].SetActive(false);
            _popupCheck.SetActive(false);
            _navigation.ActivateNavigation(true);
            _global.CloseTutoAlert();
        });

        _btnCancelLoad.onClick.AddListener(() =>
        {
            _popupLoad.SetActive(false);
            if (_loadNextPage != null)
                StopCoroutine(_loadNextPage);
            _navigation.ActivateNavigation(true);
            _global.CloseTutoAlert();
        });
    }

    protected override void TutoBindDelegate()
    {
        base.TutoBindDelegate();


        _btnConfirm.onClick.AddListener(() =>
        {
            OnPopupLoad(true);
        });

        _inputField.onValueChanged.AddListener((text) =>
        {
            if (text == TextData._tutoReserveNum)
            {
                _curAlertSize = POINT_SIZE.S;
                _highlight.SetActive(false);
                _highlightPopup[0].SetActive(true);
                _curData = _tutoDataPopup[0];
                _curRect = _highlightObjects[1];
                StartCheckInteraction();
            }
            else
            {
                StopCheckIntercation();
                _tutoInteractionChecked = true;
                _global.CloseTutoAlert();
            }
        });
        BindDelegateConfirmPopup(true);
    }

    protected override void FreeBindDelegate()
    {
        base.FreeBindDelegate();

        _btnConfirm.onClick.AddListener(() =>
        {
            OnPopupLoad(false);
        });

        BindDelegateConfirmPopup();
    }

    protected override void RealBindDelegate()
    {
        base.RealBindDelegate();

        _btnConfirm.onClick.AddListener(() =>
        {
            OnPopupLoad(false);
        });

        BindDelegateConfirmPopup();
    }

    private void BindDelegateConfirmPopup(bool isTuto = false)
    {
        _inputField.onValueChanged.AddListener((text) =>
        {
            if (text.Length == 8)
            {
                _navigation.ActivateNextButton(true);
                _navigation.SetNextAction(() => OnPopupCheck(isTuto));
            }
            else
            {
                _navigation.ActivateNextButton(false);
            }
        });
    }

    protected override void OnPageEnable()
    {
        base.OnPageEnable();
        _inputField.text = "";
        _popupCheck.SetActive(false);
        _popupLoad.SetActive(false);
        _navigation.ActivateNavigation(true);
        _navigation.SetPosition(0, -630);

        for (int i = 0; i < _highlightPopup.Length; i++)
        {
            _highlightPopup[i].SetActive(false);
        }
    }

    protected override void TutoOnPageEnable()
    {
        base.TutoOnPageEnable();

        _curData = _tutoData;
        _curRect = _highlightObjects[0];
    }

    protected override void FreeOnPageEnable()
    {
        base.FreeOnPageEnable();
    }

    protected override void RealOnPageEnable()
    {
        base.RealOnPageEnable();
    }

    protected override void OnPageDisable()
    {
        Cinema_UserData.Instance._reservation._number = _inputField.text;
        //dummy
        Cinema_UserData.Instance._movie._name = TextData._names[0];
        Cinema_UserData.Instance._movie._time = TextData._times[1];
        Cinema_UserData.Instance._movie._adultNum = 1;
        Cinema_UserData.Instance._movie._teenNum = 2;
        Cinema_UserData.Instance._movie._selecSeats = new List<string> { "D3", "D4" };
        base.OnPageDisable();
        _navigation.ResetNextAction();
    }

    public void OnPopupCheck(bool isTuto)
    {
        _popupCheck.SetActive(true);
        _textCheckNum.text = _inputField.text;
        _navigation.ActivateNavigation(false);

        if (isTuto)
        {
            _global.CloseTutoAlert();
            _highlightPopup[0].SetActive(false);
            _highlightPopup[1].SetActive(true);
            _curData = _tutoDataPopup[1];
            _curRect = _highlightObjects[2];
            StartCheckInteraction();
        }
    }

    public void OnPopupLoad(bool isTuto)
    {
        _popupCheck.SetActive(false);
        _popupLoad.SetActive(true);
        _loadNextPage = StartCoroutine(LoadNextPage());

        if (isTuto)
        {
            _highlightPopup[1].SetActive(false);
            _global.CloseTutoAlert();
        }
    }

    protected override void TutoAlert()
    {
        _global.OpenTutoAlert(_curData._pointSize, _curRect, _curData._textAlert, _curAlertSize);
    }

    private IEnumerator LoadNextPage()
    {
        yield return new WaitForSeconds(3f);
        NextPage();
    }
}
