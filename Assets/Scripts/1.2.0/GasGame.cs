using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GasGame : MonoBehaviour
{
    [SerializeField]
    private SwitchImage _gas;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Car _car;
    [SerializeField]
    private ProgressBar _bar;
    [SerializeField]
    private EndPage _endPage;

    private void Awake()
    {
        _car.Init();
        _car.OnGameEnd += () =>
        {
            _endPage.Active();
        };
    }

    private void Start()
    {
        _car.CarSpawn(0);
        _endPage.ResetPage();
    }

    public void GameStart()
    {
        _car.StartMoveIn();
    }
}
