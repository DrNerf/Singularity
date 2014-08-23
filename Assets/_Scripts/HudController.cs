using UnityEngine;
using System.Collections;

public class HudController : MonoBehaviour 
{
    public GameObject HealthHUD;
    public GameObject HealthText;
    public GameObject CooldownHUD;
    public GameObject CooldownText;

    private CharInfoController PlayerInfo;
    private AimController AimInfo;
	// Use this for initialization
	void Start () 
    {
        HealthHUD = GameObject.Find("HealthBar");
        HealthText = GameObject.Find("HealthText");
        HealthHUD.GetComponent<MeshRenderer>().enabled = true;
        HealthText.GetComponent<MeshRenderer>().enabled = true;
        CooldownHUD = GameObject.Find("CooldownBar");
        CooldownText = GameObject.Find("CooldownText");
        CooldownHUD.GetComponent<MeshRenderer>().enabled = true;
        CooldownText.GetComponent<MeshRenderer>().enabled = true;
        PlayerInfo = gameObject.GetComponent<CharInfoController>();
        AimInfo = Camera.main.GetComponent<AimController>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        HealthText.GetComponent<TextMesh>().text = PlayerInfo.Health.ToString();
        if (AimInfo.CoolDown)
        {
            CooldownText.GetComponent<TextMesh>().text = "Cooldown";
        }
        else
        {
            CooldownText.GetComponent<TextMesh>().text = "Ready to fire";
        }
	}
}
