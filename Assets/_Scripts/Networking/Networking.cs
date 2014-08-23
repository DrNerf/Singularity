using UnityEngine;
using System.Collections;

public class Networking : MonoBehaviour {

    public HostData[] Servers;
    public Rect rect;
    public GUISkin GuiSkin;
    public string NickName = "Your name";
    public string RoomName = "Server name";

    private bool IsHostMenuDrawn = false;
	// Update is called once per frame
	void Update () 
    {
	    
	}

    void OnGUI()
    {

        if(!Network.isClient && !Network.isServer)
        {
            GUI.skin = GuiSkin;
            if (IsHostMenuDrawn)
            {
                rect = GUI.Window(10, rect, DrawWindow, "Host your server");
            }

            GUI.Box(new Rect(Screen.width / 2 - 125, 100, 800, 600), "");
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 50, 100, 600, 600));
            GUILayout.BeginVertical();
            GUILayout.Label("");
            GUILayout.Label("");
            NickName = GUILayout.TextField(NickName, GUILayout.Width(200));
            GUILayout.Label("");
            try
            {
                foreach (HostData item in Servers)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(item.gameName);
                    GUILayout.Label("[" + item.connectedPlayers + "]" + " Players Online");
                    if (GUILayout.Button("Join Server", GUILayout.Width(200)))
                    {
                        Network.Connect(item);
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Label("");
                }
            }
            catch
            {
                GUILayout.Label("No servers are online at the moment!");
            }
            if (GUILayout.Button("Refresh", GUILayout.Width(200)))
            {
                RefreshServersList();
            }
            GUILayout.EndVertical();
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(Screen.width / 2 + 150, 120, 600, 100));
            GUILayout.Label("");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("HOST SERVER", GUILayout.Width(200)))
            {
                if (IsHostMenuDrawn)
                {
                    IsHostMenuDrawn = false;
                }
                else
                {
                    IsHostMenuDrawn = true;
                }
            }
            if (GUILayout.Button("Take me back!", GUILayout.Width(200)))
            {
                Application.LoadLevel(1);
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }

    void StartServer(string RoomName)
    {
        if (RoomName == "" || RoomName == "Server name")
        {
            RoomName = "PvP - Gaming Room.";
        }
        try
        {
            var useNat = !Network.HavePublicAddress();
            Network.InitializeServer(32, 25000, useNat);
            MasterServer.RegisterHost("RoboRUMBLE", RoomName);
        }
        catch
        {
            Debug.Log("There was a problem with game hosting");
        }
    }

    void DrawWindow(int id)
    {
        GUILayout.BeginVertical();
        RoomName = GUILayout.TextField(RoomName);
        GUILayout.Label("");
        NickName = GUILayout.TextField(NickName); 
        GUILayout.Label("");
        if (GUILayout.Button("Start server"))
        {
            StartServer(RoomName);
        }
        GUILayout.EndVertical();
        GUI.DragWindow();
    }

    void OnServerInitialized()
    {
        Debug.Log("Server started successfully!");
        gameObject.GetComponent<StartManager>().enabled = true;
        gameObject.GetComponent<ChatController>().enabled = true;
    }

    void OnMasterServerEvent(MasterServerEvent mse)
    {
        if(mse == MasterServerEvent.RegistrationSucceeded)
        {
            Debug.Log("Registration succeeded!");
        }
        else
        {
            Debug.LogWarning(mse.ToString());
        }
    }

    void RefreshServersList()
    {
        Servers = MasterServer.PollHostList();
        MasterServer.RequestHostList("RoboRUMBLE");
    }

    void WriteDownServerList()
    {
        Servers = MasterServer.PollHostList();
        Debug.Log("Writing servers");
    }

    void OnConnectedToServer()
    {
        Debug.Log("Joined!");
        gameObject.GetComponent<StartManager>().enabled = true;
        gameObject.GetComponent<ChatController>().enabled = true;
    }

    int PingAddress(string IpAddress)
    {
        try
        {
            Ping AddressPing = new Ping(IpAddress);

            while (!AddressPing.isDone)
            {
                Debug.Log("NotReady yet pinging" + AddressPing);
            }
            return AddressPing.time;
        }
        catch
        {
            return 0;
        }
    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log("Clean up after player " + player);
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }
}
