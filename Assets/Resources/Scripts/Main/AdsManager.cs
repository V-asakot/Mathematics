using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour
{

    private const string banner = "ca-app-pub-2516351902429576/4942342284";
    private const string inter = "ca-app-pub-2516351902429576/1221329942";
    private const string bannerIOS = "ca-app-pub-2516351902429576/4942342284";
    private const string interIOS = "ca-app-pub-2516351902429576/1221329942";
    public int curr;
    public Text Error;
    InterstitialAd InterAd;
    BannerView bannerV;
    public bool bannerLoaded;
    public bool noAds;
   // public bool startInter;
    public void Load()
    {
        LoadBanner();

    }
    public void LoadBanner()
    {
        if (noAds) return;
        Debug.Log("Banner Request");
#if UNITY_ANDROID
        bannerV = new BannerView(banner, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
      // AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("AE500C0EBD81405").Build();
        bannerV.OnAdLoaded += OnBannerLoaded;
        bannerV.OnAdFailedToLoad +=OnBannerFailed;
        bannerV.LoadAd(request);
#elif UNITY_IPHONE
         bannerV = new BannerView(bannerIOS, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
      // AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("AE500C0EBD81405").Build();
        bannerV.OnAdLoaded += OnBannerLoaded;
        bannerV.OnAdFailedToLoad +=OnBannerFailed;
        bannerV.LoadAd(request);
#endif

    }

    private void OnBannerFailed(object sender, AdFailedToLoadEventArgs e)
    {
        Error.text = e.Message;
    }

    private void OnBannerLoaded(object sender, EventArgs e)
    {
        bannerLoaded = true;
        bool b = false;
        HideBanner(b);
    }

    public void LoadInter()
    {
        if (noAds) return;
        if (curr > 0) { curr--; return; } else { curr = 2;}
        Debug.Log("Inter Request");
#if UNITY_ANDROID
        InterAd = new InterstitialAd(inter);
         AdRequest request = new AdRequest.Builder().Build();
       // AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("AE500C0EBD81405").Build();
        InterAd.OnAdLoaded += OnAdLoaded;
        InterAd.LoadAd(request);
#elif UNITY_IPHONE
        InterAd = new InterstitialAd(interIOS);
         AdRequest request = new AdRequest.Builder().Build();
       // AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("AE500C0EBD81405").Build();
        InterAd.OnAdLoaded += OnAdLoaded;
        InterAd.LoadAd(request);
#endif
    }

    private void OnAdLoaded(object sender, EventArgs e)
    {
        InterAd.Show();

    }
    public void HideBanner(bool b)
    {
        
        if (noAds)
        {
            if(bannerV!=null)bannerV.Hide();
            return;
        }
        if (b) bannerV.Hide(); else bannerV.Show();
    }

    
}
