using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class BannerAdd : MonoBehaviour
{
    string adUnitId;

    BannerView _bannerView;

    public void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            //초기화 완료
        });

#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/6300978111";
#else
            adUnitId = "unexpected_platform";
#endif

        LoadAd();
        ShowAdd();
    }

    public void LoadAd() //광고 로드
    {
        if (_bannerView == null)
        {
            CreateBannerView();
        }
        var adRequest = new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();

        _bannerView.LoadAd(adRequest);
    }

    public void CreateBannerView() //배너 광고 보여주기
    {
        Debug.Log("Creating banner view");

        if (_bannerView != null)
        {
            DestroyAd();
        }

        _bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        //_bannerView = new BannerView(_adUnitId, AdSize.Banner, 0, 50);
    }

    public void DestroyAd() //광고 제거
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner ad.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }

    public void ShowAdd()
    {
        _bannerView.Show();
    }

    public void HideAdd()
    {
        _bannerView.Hide();
    }
}