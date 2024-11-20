using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class McSidebar : MonoBehaviour
{
    private List<Transform> _listObj = new List<Transform>();
    private List<Transform> _listPracticeObj = new List<Transform>();
    private List<Button> _tutoBtns = new List<Button>();
    private List<Button> _learnBtns = new List<Button>();
    private List<Button> _practiceBtns = new List<Button>();
    private Button _sidebarBtn;
    private bool isOpenSideBar = false;
    private eScene _scene;
    private eTrainingType _trainingType;
    private RectTransform _rect;
    
    public void Setting(eScene scene, eTrainingType trainingType)
    {
        _scene = scene;
        _trainingType = trainingType;
        _rect = GetComponent<RectTransform>();
        _sidebarBtn = GetComponentInChildren<Button>();
        _listObj.AddRange(UtilityExtensions.GetComponentsOnlyInChildren_NonRecursive<Transform>(transform));
        _listPracticeObj.AddRange(UtilityExtensions.GetComponentsOnlyInChildren_NonRecursive<Transform>(_listObj[(int)eSidebarObj.Practice].transform));

        _tutoBtns.AddRange(_listObj[(int)eSidebarObj.Tutorial].GetComponentsInChildren<Button>());
        _learnBtns.AddRange(_listObj[(int)eSidebarObj.Learn].GetComponentsInChildren<Button>());
        _practiceBtns.AddRange(_listObj[(int)eSidebarObj.Practice].GetComponentsInChildren<Button>());

        _learnBtns[(int)eBtn.Continue].onClick.AddListener(Continue);
        _learnBtns[(int)eBtn.Restart].onClick.AddListener(Restart);
        _learnBtns[(int)eBtn.Exit].onClick.AddListener(Exit);
        _practiceBtns[(int)eBtn.Continue].onClick.AddListener(Continue);
        _practiceBtns[(int)eBtn.Restart].onClick.AddListener(Restart);
        _practiceBtns[(int)eBtn.Exit].onClick.AddListener(Exit);

        _sidebarBtn.onClick.AddListener(OnTouchSideBar);
    }

    public void ActivateTutorial()
    {
        _listObj[(int)eSidebarObj.Tutorial].gameObject.SetActive(true);
        _listObj[(int)eSidebarObj.Learn].gameObject.SetActive(false);
        _listObj[(int)eSidebarObj.Practice].gameObject.SetActive(false);
    }

    public void ActivateLearn()
    {
        _listObj[(int)eSidebarObj.Tutorial].gameObject.SetActive(false);
        _listObj[(int)eSidebarObj.Learn].gameObject.SetActive(true);
        _listObj[(int)eSidebarObj.Practice].gameObject.SetActive(false);
    }

    public void ActivatePractice()
    {
        _listObj[(int)eSidebarObj.Tutorial].gameObject.SetActive(false);
        _listObj[(int)eSidebarObj.Learn].gameObject.SetActive(false);
        _listObj[(int)eSidebarObj.Practice].gameObject.SetActive(true);
    }

    void OnTouchSideBar()
    {
        switch (_trainingType)
        {
            case eTrainingType.Tutorial:
                ActivateTutorial();
                break;
            case eTrainingType.FreeLearn:
                ActivateLearn();
                break;
            case eTrainingType.Practice:
                ActivatePractice();
                break;
        }

        isOpenSideBar = !isOpenSideBar;

        if (isOpenSideBar)
        {
            _listObj[(int)eSidebarObj.Arrow].transform.rotation = Quaternion.Euler(0, 180, 0);
            _rect.DOAnchorPosX(600f, 0.5f).SetEase(Ease.Linear);
        }
        else
        {
            _listObj[(int)eSidebarObj.Arrow].transform.rotation = Quaternion.Euler(0, 0, 0);
            _rect.DOAnchorPosX(50f, 0.5f).SetEase(Ease.Linear);
        }
    }

    void Continue()
    {
        OnTouchSideBar();
    }

    void Restart()
    {
        SceneManager.LoadScene((int)_scene);
    }
    
    void Exit()
    {
        SceneManager.LoadScene((int)eScene.Main);   
    }

    enum eSidebarObj
    {
        Arrow = 0,
        Tutorial,
        Learn,
        Practice
    }

    enum ePracticeObj
    {
        Board1 = 0,
        Board2,
        text
    }

    enum eBtn
    {
        Continue = 0,
        Restart,
        Exit
    }
}
