using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crops : MonoBehaviour
{
    [SerializeField] private List<GameObject> _crops = new List<GameObject>();
    [SerializeField] private GameObject _crop;
    [SerializeField] private Image _soilFront;
    [SerializeField] private Image _soilBack;
    
    private float _soilFrontSizeSmall = 220;
    private float _soilBackSizeSmall = 150;
    private float _soilFrontSizeBig = 290;
    private float _soilBackSizeBig = 220;

    public void Init()
    {
        for (int i = 0; i < _crops.Count; i++)
        {
            _crops[i].SetActive(false);
        }
    }

    public void SpawnCrop(CropType cropType)
    {
        _crop = _crops[(int)cropType];
        _crop.SetActive(true);

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