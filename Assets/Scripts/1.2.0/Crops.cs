using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Crops : MonoBehaviour
{
    [SerializeField] private List<GameObject> _crops = new List<GameObject>();
    [SerializeField] private GameObject _crop;
    [SerializeField] private Image _soilFront;
    [SerializeField] private Image _soilBack;
    [SerializeField] private Button _cropButton;

    // harvest assets
    [SerializeField] private Image _loading;
    [SerializeField] private GameObject _loadingLottie;
    [SerializeField] private Image _harvestImage;
    
    private float _soilFrontSizeSmall = 220;
    private float _soilBackSizeSmall = 150;
    private float _soilFrontSizeBig = 290;
    private float _soilBackSizeBig = 220;
    
    // crop setting
    private Image _cropImage;
    private bool _isCropGrowth = false;

    public void Init()
    {
        for (int i = 0; i < _crops.Count; i++)
        {
            _crops[i].SetActive(false);
        }

        _cropButton.onClick.AddListener(() => {if(_isCropGrowth) CropHarvest();});
        gameObject.SetActive(false);
    }

    public void SpawnCrop(CropType cropType)
    {
        gameObject.SetActive(true);
        _loading.gameObject.SetActive(false);
        _harvestImage.gameObject.SetActive(false);

        // 작물 초기화
        _crop = _crops[(int)cropType];
        _cropImage = _crop.GetComponent<Image>();
        _cropImage.color = new Color(1, 1, 1, 0);
        _loading.color = new Color(1, 1, 1, 0);
        _isCropGrowth = false;

        if(cropType <= CropType.Potato){
            // 작은 흙으로 세팅
            _soilFront.rectTransform.sizeDelta = new Vector2(_soilFrontSizeSmall, _soilFront.rectTransform.sizeDelta.y);
            _soilBack.rectTransform.sizeDelta = new Vector2(_soilBackSizeSmall, _soilBack.rectTransform.sizeDelta.y);
        }
        else{
            _soilFront.gameObject.SetActive(true);
            // 큰 흙으로 세팅
            _soilFront.rectTransform.sizeDelta = new Vector2(_soilFrontSizeBig, _soilFront.rectTransform.sizeDelta.y);
            _soilBack.rectTransform.sizeDelta = new Vector2(_soilBackSizeBig, _soilBack.rectTransform.sizeDelta.y);
        }
    }

    public void ResetCrops(){
        for (int i = 0; i < _crops.Count; i++)
        {
            _crops[i].SetActive(false);
        }
    }

    public void CropGrowth(){
        _cropImage.rectTransform.anchoredPosition += new Vector2(0, -100f);
        _crop.SetActive(true);

        // 작물 커지는 애니메이션, 종료시 수확가능
        _cropImage.DOFade(1, 0.5f);
        _cropImage.rectTransform.DOAnchorPosY(_cropImage.rectTransform.anchoredPosition.y + 100f, 0.5f)
        .OnComplete(() =>
        {
            _isCropGrowth = true;
        });
    }

    public void CropHarvest(){
        _isCropGrowth = false;
        _loading.gameObject.SetActive(true);
        _loading.DOFade(1, 0.5f);

        // animation
        StartCoroutine(CropHarvestAnimation());
    }

    private IEnumerator CropHarvestAnimation()
    {
        float loadingTime = Random.Range(1f, 2f);
        yield return new WaitForSeconds(loadingTime);

        _loading.DOFade(0, 0.5f);
        _harvestImage.gameObject.SetActive(true);

        // Disable
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    public void CropGrowthEnd(){
        _isCropGrowth = false;

        _cropImage.rectTransform.DOAnchorPosY(_cropImage.rectTransform.anchoredPosition.y - 100f, 0.5f);
        _cropImage.DOFade(0, 0.5f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

}

    public enum CropType
    {
        None = -1,
        Broccoli,
        Carrot,
        SweetPotato,
        Potato,
        Onion,
        Beet,
        Radish,
        Pumpkin,
        Cabbage
    }