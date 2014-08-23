using UnityEngine;
using System.Collections;

public class AdminTools : MonoBehaviour 
{
    public void TeleportToPunish(string Name)
    {
        networkView.RPC("TeleportPlayerOverNetwork", RPCMode.All, Name, GameObject.Find("AdminsSpawnPoint").transform.position);
    }

    public void TeleportToPlatform(string Name)
    {
        networkView.RPC("TeleportPlayerOverNetwork", RPCMode.All, Name, GameObject.Find("TeleportPoint").transform.position);
    }

    [RPC]
    void TeleportPlayerOverNetwork(string Player, Vector3 TeleportTo)
    {
        CharInfoController[] Players = FindObjectsOfType(typeof(CharInfoController)) as CharInfoController[];
        foreach (CharInfoController item in Players)
        {
            if (item.NickName == Player)
            {
                item.transform.position = TeleportTo;
            }
        }
    }

    void Start()
    {
        
    }
}
