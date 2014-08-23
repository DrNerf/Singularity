using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour 
{
    public GUISkin Skin;

    void OnGUI()
    {
        GUI.skin = Skin;

        GUILayout.BeginArea(new Rect(Screen.width/2 - 200, 100, 400, 600));
        GUILayout.BeginVertical();
        GUILayout.Label("");
        GUILayout.Button("Singleplayer");
        GUILayout.Label("");
        GUILayout.Button("Multiplayer");
        GUILayout.Label("");
        GUILayout.Button("Options");
        GUILayout.Label("");
        GUILayout.Button("Quit");
        GUILayout.EndVertical();
        GUILayout.EndArea();

        GUI.skin = null;
    }
	
}
