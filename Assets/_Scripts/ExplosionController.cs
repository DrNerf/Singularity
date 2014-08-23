using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour 
{
    public int BulletForce = 3000;
    public bool IsImmobilized;
    public float ImmobilizeTime = 2;

    private bool IsApplyingForce = false;
    private Vector3 ForceDirection;
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (IsApplyingForce)
        {
            rigidbody.AddForce(ForceDirection * BulletForce);
        }
	}

    void OnTriggerEnter(Collider Item)
    {
        if (Item.tag == "Explosion")
        {

            int Rand = Random.Range(0, 3);

            switch (Rand)
            {
                case 0: { IsApplyingForce = true; ForceDirection = Vector3.forward; }; break;
                case 1: { IsApplyingForce = true; ForceDirection = Vector3.forward * -1; }; break;
                case 2: { IsApplyingForce = true; ForceDirection = Vector3.left; }; break;
                case 3: { IsApplyingForce = true; ForceDirection = Vector3.right; }; break;
            }

            Invoke("ResetForce", 0.5f);
        }
        if (Item.tag == "EMP")
        {
            IsImmobilized = true;
            Invoke("ResetImmobilize", ImmobilizeTime);
        }

    }

    void ResetForce()
    {
        IsApplyingForce = false;
    }

    void ResetImmobilize()
    {
        IsImmobilized = false;
    }
}
