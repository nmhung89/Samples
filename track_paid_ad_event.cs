protected override void RequestInterstitialAds()
{
    if (mInterstitialData == null)
    {
        mInterstitialData = new InterstitialAd(InterstitialId);
        
        mInterstitialData.OnPaidEvent += (sender, args) =>
        {
            var interstitialAd = (InterstitialAd) (sender);
            if (interstitialAd != null)
            {
                var info = interstitialAd.GetResponseInfo();
                trackPaidAdEvent(args, Config.Platform.InterstitialId, info.GetMediationAdapterClassName());
            }
            else
            {
                trackPaidAdEvent(args, Config.Platform.InterstitialId, null);
            }  
        };
        
    }

    AdRequest request = new AdRequest.Builder().Build();
    mInterstitialData.LoadAd(request);
}

protected override void RequestRewardedVideoAds() {
        // ........
        // similar to interstitial
        mRewardedVideoData.OnPaidEvent += (sender, args) =>
        {
        }
        // ........
}

protected override void RequestBannerAds() {
        // ........
        // similar to interstitial
        mBannerData.OnPaidEvent += (sender, args) =>
        {
        }
        // ........
}

 public static void trackPaidAdEvent( AdValueEventArgs args, string adUnit, string adNetworkName) {
     Dictionary<string, object> Dic = new Dictionary<string, object>();
     Dic["valuemicros"] = args.AdValue.Value;
     Dic["currency"] = args.AdValue.CurrencyCode;
     Dic["precision"] = args.AdValue.Precision;
     Dic["adunitid"] = adUnit;

     if (adNetworkName != null)
     {
         Dic["network"] = adNetworkName;
     }
     Debug.Log($"trackPaidAdEvent  \n valuemicros {args.AdValue.Value} \n currency {args.AdValue.CurrencyCode} \n precision {args.AdValue.Precision} ");
     
     Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_revenue_sdk", Dic);
 }
