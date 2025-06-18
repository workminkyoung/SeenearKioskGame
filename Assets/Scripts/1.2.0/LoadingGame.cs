using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

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
        StartCoroutine(SpawnCrop());
    }

    private IEnumerator SpawnCrop()
    {
        float gameTime = 180f; // 3분
        float elapsedTime = 0f;

        while (elapsedTime < gameTime)
        {
            // 랜덤한 시간 간격 (0.5초 ~ 2초)
            float waitTime = Random.Range(0.5f, 2f);
            yield return new WaitForSeconds(waitTime);

            // 랜덤한 작물 선택
            int randomCropIndex = Random.Range(0, _crops.Count);
            _crops[randomCropIndex].CropGrowth();

            elapsedTime += waitTime;
        }
    }
}
