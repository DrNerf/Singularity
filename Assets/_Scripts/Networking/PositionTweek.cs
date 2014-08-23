using UnityEngine;
using System.Collections;

public class PositionTweek : MonoBehaviour 
{

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        if (stream.isWriting)
        {
            Vector3 Pos = transform.position;
            stream.Serialize(ref Pos);
        }
        else
        {
            Vector3 ReceivedPosition = Vector3.zero;
            stream.Serialize(ref ReceivedPosition);
            transform.position = ReceivedPosition;
            //Vector3.Lerp(transform.position, ReceivedPosition, Time.deltaTime * 5);
        }
    }
}
