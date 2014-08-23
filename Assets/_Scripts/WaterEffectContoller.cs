using UnityEngine;
using System.Collections;

public class WaterEffectContoller : MonoBehaviour 
{
    public Texture WaterEffect;

    private bool IsInWater = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Water")
        {
            IsInWater = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Water")
        {
            IsInWater = false;
        }
    }

    void OnGUI()
    {
        if (IsInWater)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), WaterEffect);
        }
    }
}
