using Firebase.Analytics;

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
     Parameter[] AdRevenueParameters = {
        new Parameter(FirebaseAnalytics.ParameterValue, Convert.ToDouble(args.AdValue.Value) / 1000000),
        new Parameter(FirebaseAnalytics.ParameterCurrency, "USD"),
        new Parameter("ad_format", adFormat), // adFormat: banner, inter, reward, app_open, native, rewarded_inter, mrec, audio
        new Parameter("ab_test_name", args.mediationABTestName),
        new Parameter("ab_test_variant", args.mediationABTestVariant),
        // not required (these are for level analytics)
        new Parameter(FirebaseAnalytics.ParameterLevel, currentLevel.ToString()),
        new Parameter("level_mode", currentMode.ToString())
    };

     FirebaseAnalytics.LogEvent("ad_revenue_sdk", AdRevenueParameters);
 }
