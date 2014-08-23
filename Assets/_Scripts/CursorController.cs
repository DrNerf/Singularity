using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour 
{
    public Texture Cursor;
    public int CursorSize;
	
    void Start()
    {
        Screen.showCursor = false;
    }
	// Update is called once per frame
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, CursorSize, CursorSize), Cursor);
    }

    void OnApplicationFocus(bool FocusStatus)
    {
        if (FocusStatus)
        {
            Screen.showCursor = false;
        }
        else
        {
            Screen.showCursor = true;
        }
    }
}
