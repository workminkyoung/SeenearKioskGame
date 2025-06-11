using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crops : MonoBehaviour
{
    [SerializeField] private List<GameObject> _crops = new List<GameObject>();
    [SerializeField] private GameObject _crop;
    [SerializeField] private Image _soilFront;
    [SerializeField] private Image _soilBack;
    

    private void Init()
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
        }
        else{
            _soilFront.gameObject.SetActive(true);
            // 큰 흙으로 세팅
        }
    }

    public void ResetCrops(){
        for (int i = 0; i < _crops.Count; i++)
        {
            _crops[i].SetActive(false);
        }
    }

    public enum CropType
    {
        None = -1,
        Beet,
        Broccoli,
        Radish,
        Carrot,
        Onion,
        SweetPotato,
        Potato,
        Pumpkin,
        Cabbage
    }
}
