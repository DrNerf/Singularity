using UnityEngine;
using System.Collections;

public class CharInfoController : MonoBehaviour 
{
    public string TeamName;
    public bool TeamBool;
    public string NickName ="";
    public int Health = 100;
    public GameObject NameText;
    public GameObject ExplosionOnDeath;
	
	void Start () 
    {
        Time.timeScale = 1;
        NickName = Camera.main.GetComponent<Networking>().NickName;
        networkView.RPC("SetNameText", RPCMode.AllBuffered, NickName, NameText.networkView.viewID);
	}

    public void SetTeamBool(bool Team)
    {
        if (Team)
        {
            TeamBool = Team;
            TeamName = "Beta";
        }
        else
        {
            TeamBool = Team;
            TeamName = "Alpha";
        }
    }
    
    [RPC]
    void SetNameText(string NickName, NetworkViewID TextObjectID)
    {
        NetworkView TextObject = NetworkView.Find(TextObjectID);
        TextObject.GetComponent<TextMesh>().text = NickName;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Respawn")
        {
            GameObject.Instantiate(ExplosionOnDeath, transform.position, transform.rotation);
            SetPlayerToSpectate();
        }
    }

    void SetPlayerToSpectate()
    {
        GameObject Cam = Camera.main.gameObject;
        Cam.GetComponent<SmoothFollow>().target = GameObject.Find("SpectatorPoint").transform;
        Cam.GetComponent<RespawnController>().IsDead = true;
        Cam.GetComponent<ChatController>().RemoteChat("just got PWND!");
        Network.Destroy(gameObject);
    }
}
