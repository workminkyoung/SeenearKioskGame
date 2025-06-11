using UnityEngine;
using DG.Tweening;

public class LoadingGame : MonoBehaviour
{
    // 인트로 세팅값
    [SerializeField] private GameObject _car;
    [SerializeField] private Vector3 _carStartPos;
    [SerializeField] private Vector3 _carPausePos;
    [SerializeField] private Vector3 _carEndPos;
    [SerializeField] private float _carMoveTime;

    void Awake()
    {
        
    }

    private void Intro(){
        _car.transform.position = _carStartPos;
        _car.transform.DOMove(_carPausePos, _carMoveTime).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            StartGame();
        });
    }

    private void StartGame(){
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
