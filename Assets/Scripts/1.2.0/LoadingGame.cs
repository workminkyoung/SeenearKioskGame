using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class LoadingGame : MonoBehaviour
{
    // 인트로 세팅값
    [SerializeField] private RectTransform _car;
    [SerializeField] private Vector3 _carStartPos;
    [SerializeField] private Vector3 _carPausePos;
    [SerializeField] private Vector3 _carEndPos;
    [SerializeField] private float _carMoveTime;

    [Header("Crop Setting")]
    [SerializeField] private List<Crops> _crops = new List<Crops>();
    [SerializeField] private CropType _cropType; //임시

    void Awake()
    {
        _crops.AddRange(GetComponentsInChildren<Crops>());
        for(int i = 0; i < _crops.Count; i++){
            _crops[i].Init();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Intro();
    }

    private void Intro(){
        _car.anchoredPosition = _carStartPos;
        _car.DOAnchorPos(_carPausePos, _carMoveTime).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            StartGame();
        });
    }

    private void StartGame(){
        for(int i = 0; i < _crops.Count; i++){
            _crops[i].SpawnCrop(_cropType);
        }
    }
}
