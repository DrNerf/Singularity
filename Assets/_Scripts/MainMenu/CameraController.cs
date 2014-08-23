using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
    public int SpeedForAnimation;
    [Range(1, 3)]
    public float SpeedDivision;
    public Transform Pos1;
    public Transform Pos2;

    private Transform LerpTransform;

	// Use this for initialization
	void Start () 
    {
        LerpTransform = Pos1;
        StartCoroutine(CameraRoutine());
	}

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, LerpTransform.position, Time.deltaTime / SpeedDivision);
        transform.rotation = Quaternion.Lerp(transform.rotation, LerpTransform.rotation, Time.deltaTime / SpeedDivision);
    }

    IEnumerator CameraRoutine()
    {
        LerpTransform = Pos1;
        yield return new WaitForSeconds(SpeedForAnimation);
        LerpTransform = Pos2;
        yield return new WaitForSeconds(SpeedForAnimation);
        StartCoroutine(CameraRoutine());
    }
}
