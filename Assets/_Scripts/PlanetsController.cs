using UnityEngine;
using System.Collections;

public class PlanetsController : MonoBehaviour
{
    public float RotatingSpeed = 0.05f;
	
	// Update is called once per frame
	void Update () 
    {
        transform.Rotate(Vector3.up * RotatingSpeed);
	}
}
