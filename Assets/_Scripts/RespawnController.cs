using UnityEngine;
using System.Collections;

public class RespawnController : MonoBehaviour 
{
    public bool IsDead = false;
    public GUISkin Skin;

    private float RespawnSecs = 25;
    void OnGUI()
    {
        GUI.skin = Skin;
        if (IsDead)
        {
            GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height/2 - 200, 350, 200), "You are dead! Respawn in " + RespawnSecs.ToString("0") + "seconds");
        }
    }

    void Update()
    {
        if (IsDead)
        {
            RespawnSecs -= Time.deltaTime;
        }

        if (RespawnSecs < 0)
        {
            IsDead = false;
            RespawnSecs = 25;
            GetComponent<StartManager>().JoinBattle();
        }
    }

}
