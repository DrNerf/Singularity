using UnityEngine;
using System.Collections;

public class ObjectDestructor : MonoBehaviour 
{
    public float Time;

    void Start()
    {
        Destroy(gameObject, Time);
    }
}
