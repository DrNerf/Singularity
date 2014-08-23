using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatController : MonoBehaviour 
{
    public List<string> Messages;
    public Rect ChatRect;
    public GUISkin Skin;
    public AudioClip LeeroyEmote;
    public GameObject SoundEmotesObj;

    private string Message = "";
    private float LastUnfocusTime;
    private string NickName = "";
	// Use this for initialization
	void Start () 
    {
        ChatRect = new Rect(5, Screen.height - 200, 400, 300);
        NickName = gameObject.GetComponent<Networking>().NickName;
	}
	
	// Update is called once per frame
	void OnGUI () 
    {
        GUI.skin = Skin;
        GUI.SetNextControlName("");
        GUI.Box(ChatRect, "");
        GUILayout.BeginArea(new Rect(35, Screen.height - 160, 350, 300));
        //DrawChatWindow
        GUILayout.BeginVertical();
        for (int i = Messages.Count - 4; i <= Messages.Count - 1; i++)
        {
            GUILayout.Label(Messages[i]);
        }
        GUI.SetNextControlName("ChatField");
        Message = GUILayout.TextField(Message);
        if (Event.current.type == EventType.keyDown && Event.current.character == '\n')
        {
            if (GUI.GetNameOfFocusedControl() == "ChatField")
            {
                switch (Message)
                {
                    case "/dance":
                        gameObject.GetComponent<SmoothFollow>().target.GetComponent<AnimationsContoller>().Dancing = true;
                        networkView.RPC("SendChatMessage", RPCMode.AllBuffered, "is doing strange moves!", NickName);
                        break;
                    case "/admin":
                        if (PlayerPrefs.GetInt("IsAdmin") == 1)
                        {
                            gameObject.GetComponent<StartManager>().SpawnAdmin();
                            networkView.RPC("SendChatMessage", RPCMode.AllBuffered, "joined the room! Bow before him mortals!", NickName);
                        }
                        break;
                    case "/hi":
                        gameObject.GetComponent<SmoothFollow>().target.GetComponent<AnimationsContoller>().Wave = true;
                        networkView.RPC("SendChatMessage", RPCMode.AllBuffered, "is greeting everyone!", NickName);
                        break;
                    case "/hello":
                        gameObject.GetComponent<SmoothFollow>().target.GetComponent<AnimationsContoller>().Wave = true;
                        networkView.RPC("SendChatMessage", RPCMode.AllBuffered, "is greeting everyone!", NickName);
                        break;
                    case "/wave":
                        gameObject.GetComponent<SmoothFollow>().target.GetComponent<AnimationsContoller>().Wave = true;
                        networkView.RPC("SendChatMessage", RPCMode.AllBuffered, "is greeting everyone!", NickName);
                        break;
                    case "/leeroooy":
                        networkView.RPC("SendChatMessage", RPCMode.AllBuffered, "LEEROOOOOY JENKIINS!!!", NickName);
                        networkView.RPC("RunSoundEmote", RPCMode.AllBuffered, "Leeroy");
                        break;
                    default:
                        if (Message.Contains("/summ") && PlayerPrefs.GetInt("IsAdmin") == 1)
                        {
                            char[] TempCharArray = Message.ToCharArray();
                            bool GotTheSpace = false;
                            string ExtractedName = "";
                            foreach (char item in TempCharArray)
                            {
                                if (GotTheSpace)
                                {
                                    ExtractedName += item;
                                }
                                if (item == ' ')
                                {
                                    GotTheSpace = true;
                                }
                            }
                            gameObject.GetComponent<SmoothFollow>().target.GetComponent<AdminTools>().TeleportToPunish(ExtractedName);
                            networkView.RPC("SendChatMessage", RPCMode.AllBuffered, "just teleported " + ExtractedName + " to the punishment room!", NickName);
                            break;
                        }
                        if (Message.Contains("/tp") && PlayerPrefs.GetInt("IsAdmin") == 1)
                        {
                            char[] TempCharArray = Message.ToCharArray();
                            bool GotTheSpace = false;
                            string ExtractedName = "";
                            foreach (char item in TempCharArray)
                            {
                                if (GotTheSpace)
                                {
                                    ExtractedName += item;
                                }
                                if (item == ' ')
                                {
                                    GotTheSpace = true;
                                }
                            }
                            gameObject.GetComponent<SmoothFollow>().target.GetComponent<AdminTools>().TeleportToPlatform(ExtractedName);
                            networkView.RPC("SendChatMessage", RPCMode.AllBuffered, "just teleported " + ExtractedName + " to the platform!", NickName);
                            break;
                        }
                        if (Message != "")
                        {
                            if (!BadLanguageCheck(Message))
                            {
                                networkView.RPC("SendChatMessage", RPCMode.AllBuffered, Message, NickName);
                            }
                            else
                            {
                                networkView.RPC("SendChatMessage", RPCMode.AllBuffered, "tried to use rude words. ", NickName);
                            }
                        }
                        break;
                }
                Message = "";
                LastUnfocusTime = Time.time;
                GUI.FocusControl("");
                GUI.UnfocusWindow();
            }
            else
            {
                if (LastUnfocusTime < Time.time - 0.1f)
                {
                    GUI.FocusControl("ChatField");
                }
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
	}

    [RPC]
    void SendChatMessage(string ChatMessage, string Name)
    {
        Messages.Add("[" + Name + "] : " + ChatMessage);
    }

    public void RemoteChat(string message)
    {
        networkView.RPC("SendChatMessage", RPCMode.AllBuffered, message, NickName);
    }

    bool BadLanguageCheck(string StringToCheck)
    {
        string[] BadWordsArray = new string[] { "bitch", "dick", "nigga", "nigger", "fuck"
                                                , "faggot", "niga", "slut", "anus", "cunt"
                                                , "vagina", "penis", "hui", "kur", "kurva"
                                                , "pedal", "putka", "deba", "eba"};

        foreach (string item in BadWordsArray)
        {
            if (StringToCheck.ToLower().Contains(item))
            {
                return true;
            }
        }

        return false;
    }


    [RPC]
    void RunSoundEmote(string EmoteName)
    {
        if (EmoteName == "Leeroy")
        {
            SoundEmotesObj.audio.clip = LeeroyEmote;
            SoundEmotesObj.audio.Play();
        }
    }
}
