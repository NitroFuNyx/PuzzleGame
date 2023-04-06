using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public bool NeedToShowAdBeforeLevelStart()
    {
        bool showAd = false;

        int adShowIndex;

        adShowIndex = Random.Range(0, 2);

        if(adShowIndex == 1)
        {
            showAd = true;
        }

        return showAd;
    }
}
