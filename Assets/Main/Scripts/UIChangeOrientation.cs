using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChangeOrientation : MonoBehaviour
{


    public void ChangeOrientation()
    {

        if (SaveManager.Instance.MyData.OrientationGame == OrientationGame.Horizontal)
        {
            SaveManager.Instance.MyData.OrientationGame = OrientationGame.Vertical;
            Screen.orientation = ScreenOrientation.Portrait;
            SaveManager.Instance.Save();
            return;
        }
        if (SaveManager.Instance.MyData.OrientationGame == OrientationGame.Vertical)
        {
            SaveManager.Instance.MyData.OrientationGame = OrientationGame.Horizontal;
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            SaveManager.Instance.Save();
        }
    

    }
}
