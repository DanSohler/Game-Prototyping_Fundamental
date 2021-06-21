using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineTrajectory : MonoBehaviour
{
    public LineRenderer wantedLineRenderer;

    private void Awake()
    {
        // declares wanted linerenderer
        wantedLineRenderer = GetComponent<LineRenderer>();
    }

    // function that sets up where the initial draw points are.
    public void RenderLine(Vector3 mouseStartPoint, Vector3 mouseEndPoint)
    {
        wantedLineRenderer.positionCount = 2;
        Vector3[] points = new Vector3[2];
        points[0] = mouseStartPoint;
        points[1] = mouseEndPoint;

        wantedLineRenderer.SetPositions(points);
    }

    // culls rendered line
    public void EndLine()
    {
        wantedLineRenderer.positionCount = 0;
    }

}
