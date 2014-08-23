using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AimController : MonoBehaviour 
{
    public GameObject Player;
    public Transform AimObject;
    public List<GameObject> Explosions;
    public bool CoolDown = false;
    public Transform Bullet;
    public Transform BlueBulletResetPos;
    public GameObject EMP;
    public bool EMPCoolDown = false;

    private bool IsBulletFlying = false;
    private Vector3 BulletTarget;
    private bool IsPauseMenuOpened = false;
    private RespawnController PlayerRespawn;

	// Use this for initialization
	void Start () 
    {
        PlayerRespawn = gameObject.GetComponent<RespawnController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (Input.GetKeyDown("o"))
        {
            if (IsPauseMenuOpened)
            {
                IsPauseMenuOpened = false;
            }
            else
            {
                IsPauseMenuOpened = true;
            }
        }

        if (!PlayerRespawn.IsDead)
        {
            if (!IsPauseMenuOpened)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.cyan);
                RaycastHit Hit;
                if (Physics.Raycast(ray, out Hit) == true)
                {
                    AimObject.position = Hit.point;

                    if (Input.GetMouseButtonDown(0))
                    {

                        if (!CoolDown)
                        {
                            CoolDown = true;
                            Invoke("ResetCoolDown", 2);
                            BulletTarget = Hit.point;
                            IsBulletFlying = true;
                            Invoke("BulletExplode", 0.5f);
                        }
                    }

                    if (Input.GetMouseButtonDown(1))
                    {
                        if (!EMPCoolDown)
                        {
                            EMPCoolDown = true;
                            Invoke("ResetEMPCoolDown", 2);
                            networkView.RPC("EMPExplodeRPC", RPCMode.AllBuffered, Hit.point);
                        }
                    }

                    if (IsBulletFlying)
                    {
                        Bullet.position = Vector3.Lerp(Bullet.position, Hit.point, Time.deltaTime * 10);
                        Invoke("ResetBullet", 0.5f);
                    }
                }

            }
        }
	}

    void OnGUI()
    {
        
    }

    void ResetCoolDown()
    {
        CoolDown = false;
    }

    void ResetEMPCoolDown()
    {
        EMPCoolDown = false;
    }

    void ResetBullet()
    {
        IsBulletFlying = false;
    }

    void BulletExplode()
    {
        networkView.RPC("BulletExplodeRPC", RPCMode.All, BulletTarget);
    }

    [RPC]
    void BulletExplodeRPC(Vector3 ExplosionPosition)
    {
        Bullet.position = BlueBulletResetPos.position;
        int RandNum = Random.Range(0, Explosions.Count);
        GameObject.Instantiate(Explosions[RandNum], ExplosionPosition, Quaternion.identity);
        Debug.Log("RPC Explosion");
    }

    [RPC]
    void EMPExplodeRPC(Vector3 EMPPosition)
    {
        GameObject.Instantiate(EMP, EMPPosition, Quaternion.identity);
        Debug.Log("RPC EMP");
    }
}
