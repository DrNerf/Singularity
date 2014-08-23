using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour 
{
    public Transform TeleportDestination;

    void OnTriggerEnter(Collider Item)
    {
        Item.transform.position = TeleportDestination.position;
        Item.transform.rotation = TeleportDestination.rotation;
    }
}
