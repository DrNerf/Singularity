using UnityEngine;
using System.Collections;

public class StartManager : MonoBehaviour 
{

    public GUISkin Skin;
    public GameObject AdminPrefab;
    public GameObject BetaCharPrefab;
    public GameObject AlphaCharPrefab;
    public GameObject BlueBullet;
    public GameObject RedBullet;
    public GameObject BetaVisual;
    public GameObject AlphaVisual;
    public TextMesh TeamText;

    private bool Team = true;
    private string TeamButton = "";
    private bool DrawGUI = true;

    void Start()
    {
        TeamText = GameObject.Find("TeamText").GetComponent<TextMesh>();
    }

    void OnGUI()
    {
        if (DrawGUI)
        {
            GUI.skin = Skin;
            GUI.Box(new Rect(Screen.width - 600, Screen.height / 2 - 250, 400, 500), "");
            GUILayout.BeginArea(new Rect(Screen.width - 500, Screen.height / 2 - 220, 200, 500));
            GUILayout.BeginVertical();
            GUILayout.Label("");
            if (GUILayout.Button(TeamButton))
            {
                if (Team)
                {
                    Team = false;
                }
                else
                {
                    Team = true;
                }
            }
            GUILayout.Label("");
            if (GUILayout.Button("JOIN BATTLE!"))
            {
                JoinBattle();
            }
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }

    void Update()
    {
        if (Team)
        {
            TeamButton = "Join team Alpha";
            AlphaVisual.SetActive(false);
            BetaVisual.SetActive(true);
            TeamText.text = "Team Beta";
        }
        else
        {
            TeamButton = "Join team Beta";
            AlphaVisual.SetActive(true);
            BetaVisual.SetActive(false);
            TeamText.text = "Team Alpha";
        }
    }

    public void JoinBattle()
    {
        DrawGUI = false;
        if (Team)
        {
            GameObject Cam = gameObject;
            Transform SpawnPos = GameObject.Find("BlueSpawnPoint").transform;
            Transform BulletSpawnPos = GameObject.Find("BlueBulletResetPos").transform;
            GameObject Player = Network.Instantiate(BetaCharPrefab, SpawnPos.position, SpawnPos.rotation,0) as GameObject;
            GameObject Bullet = Network.Instantiate(BlueBullet, BulletSpawnPos.position, BulletSpawnPos.rotation, 0) as GameObject;
            Cam.GetComponent<SmoothFollow>().enabled = true;
            Cam.GetComponent<SmoothFollow>().target = Player.transform;
            AimController TempAimController = Cam.GetComponent<AimController>();
            TempAimController.enabled = true;
            TempAimController.Player = Player;
            TempAimController.Bullet = Bullet.transform;
            TempAimController.BlueBulletResetPos = BulletSpawnPos;
            Player.GetComponent<CharInfoController>().enabled = true;
            Player.GetComponent<CharInfoController>().SetTeamBool(Team);
            Player.GetComponent<HudController>().enabled = true;
            gameObject.GetComponent<ChatController>().RemoteChat("just joined team BETA!");
        }
        else
        {
            GameObject Cam = gameObject;
            Transform SpawnPos = GameObject.Find("RedSpawnPoint").transform;
            Transform BulletSpawnPos = GameObject.Find("RedBulletResetPos").transform;
            GameObject Player = Network.Instantiate(AlphaCharPrefab, SpawnPos.position, SpawnPos.rotation, 0) as GameObject;
            GameObject Bullet = Network.Instantiate(RedBullet, BulletSpawnPos.position, BulletSpawnPos.rotation, 0) as GameObject;
            Cam.GetComponent<SmoothFollow>().enabled = true;
            Cam.GetComponent<SmoothFollow>().target = Player.transform;
            AimController TempAimController = Cam.GetComponent<AimController>();
            TempAimController.enabled = true;
            TempAimController.Player = Player;
            TempAimController.Bullet = Bullet.transform;
            TempAimController.BlueBulletResetPos = BulletSpawnPos;
            GameObject.Find("Aim").renderer.material.color = Color.red;
            Player.GetComponent<CharInfoController>().enabled = true;
            Player.GetComponent<CharInfoController>().SetTeamBool(Team);
            Player.GetComponent<HudController>().enabled = true;
            gameObject.GetComponent<ChatController>().RemoteChat("just joined team ALPHA!");
        }
    }

    public void SpawnAdmin()
    {
        DrawGUI = false;
        GameObject Cam = gameObject;
        Transform SpawnPos = GameObject.Find("AdminsSpawnPoint").transform;
        Transform BulletSpawnPos = GameObject.Find("BlueBulletResetPos").transform;
        GameObject Player = Network.Instantiate(AdminPrefab, SpawnPos.position, SpawnPos.rotation, 0) as GameObject;
        GameObject Bullet = Network.Instantiate(BlueBullet, BulletSpawnPos.position, BulletSpawnPos.rotation, 0) as GameObject;
        Cam.GetComponent<SmoothFollow>().enabled = true;
        Cam.GetComponent<SmoothFollow>().target = Player.transform;
        AimController TempAimController = Cam.GetComponent<AimController>();
        TempAimController.enabled = true;
        TempAimController.Player = Player;
        TempAimController.Bullet = Bullet.transform;
        TempAimController.BlueBulletResetPos = BulletSpawnPos;
        Player.GetComponent<CharInfoController>().enabled = true;
        Player.GetComponent<CharInfoController>().SetTeamBool(Team);
        Player.GetComponent<HudController>().enabled = true;
    }
}
