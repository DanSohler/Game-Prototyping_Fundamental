using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineTrajectory : MonoBehaviour
{
    public LineRenderer wantedLineRenderer;

    private void Awake()
    {
        wantedLineRenderer = GetComponent<LineRenderer>();
    }

    public void RenderLine(Vector3 mouseStartPoint, Vector3 mouseEndPoint)
    {
        wantedLineRenderer.positionCount = 2;
        Vector3[] points = new Vector3[2];
        points[0] = mouseStartPoint;
        points[1] = mouseEndPoint;

        wantedLineRenderer.SetPositions(points);
    }

    public void EndLine()
    {
        wantedLineRenderer.positionCount = 0;
    }

}
