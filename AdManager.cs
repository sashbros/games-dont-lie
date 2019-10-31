using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;

    private string storeId = "3299772";

    private string videoAd = "video";
    private string rewardedVideo = "rewardedVideo";

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Monetization.Initialize(storeId, false);
    }
    public void ShowAd()
    {
        if(GameObject.Find("SaveManager"))
        {
           
                if(Monetization.IsReady(videoAd))
                {
                    ShowAdPlacementContent ad = null;
                    ad = Monetization.GetPlacementContent(videoAd) as ShowAdPlacementContent;

                    if (ad!=null)
                    {
                        ad.Show();
                    }
                }
          
        }
    }
    public void ShowRewardedAd()
    {
        if (GameObject.Find("SaveManager"))
        {

            if (Monetization.IsReady(rewardedVideo))
            {
                ShowAdPlacementContent ad = null;
                ad = Monetization.GetPlacementContent(videoAd) as ShowAdPlacementContent;

                if (ad != null)
                {
                    ad.Show();
                }
            }

        }
    }
}
