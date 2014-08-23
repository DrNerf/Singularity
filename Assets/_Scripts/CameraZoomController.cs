using UnityEngine;
using System.Collections;

public class CameraZoomController : MonoBehaviour 
{
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            gameObject.GetComponent<SmoothFollow>().distance += 0.1f;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            gameObject.GetComponent<SmoothFollow>().distance -= 0.1f;
        }
        if (Input.GetButtonDown("CameraHeightUp"))
        {
            gameObject.GetComponent<SmoothFollow>().height += 0.5f;
        }
        if (Input.GetButtonDown("CameraHeightDown"))
        {
            gameObject.GetComponent<SmoothFollow>().height -= 0.5f;
        }
	}
}
