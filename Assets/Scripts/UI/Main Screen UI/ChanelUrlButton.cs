using UnityEngine;

public class ChanelUrlButton : ButtonInteractionHandler
{
    private const string ChanelURL = "https://www.youtube.com/@hubabu";

    public override void ButtonActivated()
    {
        Application.OpenURL(ChanelURL);
    }
}
