using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake2 : MonoBehaviour
{
    public int lenght;
    public LineRenderer lineRend;
    public Vector3[] segmentPoses;
    private Vector3[] segmentV;

    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;
    public float wiggleSpeed;
    public float wiggleMagnitude;
    public Transform wiggleDir;

    private void Start()
    {
        lineRend.positionCount = lenght;
        segmentPoses = new Vector3[lenght];
        segmentV = new Vector3[lenght];
    }

    private void Update()
    {
        wiggleDir.localRotation = Quaternion.Euler(0,0,Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);

        segmentPoses[0] = targetDir.position;

        for (int i = 1; i < segmentPoses.Length; i++)
        {
            Vector3 targPos = segmentPoses[i-1] + (segmentPoses[i] - segmentPoses[i-1]).normalized * targetDist;
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], targPos, ref segmentV[i], smoothSpeed);
        }
        lineRend.SetPositions(segmentPoses);
    }
}
