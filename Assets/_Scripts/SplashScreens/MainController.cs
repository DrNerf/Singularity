using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainController : MonoBehaviour 
{

    public List<Texture> Screens;
    public float Clock = 0;
    public float SplashScreenTime = 3;

    private int ScreenIndentifier = 0;

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Screens[ScreenIndentifier]);
        Clock += Time.deltaTime;
        if (Clock > SplashScreenTime)
        {
            NextSplashOrLevel();
        }
    }

    void NextSplashOrLevel()
    {
        Debug.Log("Invoke called");
        if (ScreenIndentifier == Screens.Count - 1)
        {
            Application.LoadLevel(1);
        }
        else
        {
            ScreenIndentifier += 1;
            Clock = 0;
        }
    }
}
