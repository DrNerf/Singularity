using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;

public class CallAdmin : MonoBehaviour 
{
    public GUISkin Skin;

    private string Subject = "Admin request";
    private string MessageText = "";
    private bool IsDrawn = false;

    void SendRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("Subject", WWW.EscapeURL(Subject));
        form.AddField("Text", WWW.EscapeURL(MessageText));
        WWW www = new WWW("http://pvp-gaming.net/Home/SubmitTicket", form);
        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            if (IsDrawn)
            {
                IsDrawn = false;
            }
            else
            {
                IsDrawn = true;
            }
        }
    }

    void OnGUI()
    {
        if (IsDrawn)
        {
            GUI.skin = Skin;
            GUI.Box(new Rect(50, 50, Screen.width / 3, 600), "Send ticket to moderators");
            GUILayout.BeginArea(new Rect(120, 120, Screen.width / 5, 600), "");
            GUILayout.BeginVertical();
            GUILayout.Label("Subject:");
            Subject = GUILayout.TextField(Subject);
            GUILayout.Label("Message:");
            MessageText = GUILayout.TextArea(MessageText);
            GUILayout.Label("");
            GUILayout.Label("");
            if (GUILayout.Button("Submit", GUILayout.Width(200)))
            {
                SendRequest();
            }
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
