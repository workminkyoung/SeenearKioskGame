using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class InterstitialAdd : MonoBehaviour
{
    string adUnitId;

    InterstitialAd interstitalad;

    public void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            //초기화 완료
        });

#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5224354917";
#else
            adUnitId = "unexpected_platform";
#endif

        LoadAd();
    }

    public void LoadAd() //광고 로드
    {
        if (interstitalad == null)
        {
            CreateInterstitalView();
        }

        var adRequest = new AdRequest.Builder()
            .AddKeyword("unity-admob-sample2")
            .Build();
        
        interstitalad.LoadAd(adRequest);
        interstitalad.Show();
    }

    public void CreateInterstitalView() //전면 광고 보여주기
    {
        Debug.Log("Creating Interstital view");
        
        if (interstitalad != null)
        {
            DestroyAd();
        }

        interstitalad = new InterstitialAd(adUnitId);
    }

    public void DestroyAd() //광고 제거
    {
        if (interstitalad != null)
        {
            Debug.Log("Destroying ad.");
            interstitalad.Destroy();
            interstitalad = null;
        }
    }
}
