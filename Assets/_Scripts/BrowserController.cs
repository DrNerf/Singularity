using UnityEngine;
using System.Collections;

public class BrowserController : MonoBehaviour 
{
    void SetAdmin(string DefStr)
    {
        PlayerPrefs.SetInt("IsAdmin", 1);
    }
	
}
