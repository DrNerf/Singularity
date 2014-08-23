using UnityEngine;
using System.Collections;

public class NamesLookAtInit : MonoBehaviour 
{

	// Use this for initialization
    void Start()
    {
        gameObject.GetComponent<SmoothLookAt>().target = Camera.main.transform;
    }
}
