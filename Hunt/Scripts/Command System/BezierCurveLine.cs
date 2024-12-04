using HT;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierCurveLine : MonoBehaviour
{
    public int segmentCount = 50; 
    public float amplitude;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segmentCount + 1;
    }

    public void DrawCurve(Vector3 startPos, Vector3 endPos)
    {
        Vector3 midPos = (startPos + endPos) / 2;
        midPos.y += amplitude;

        for (int i = 0; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            Vector3 position = CalculateBezierPoint(t, startPos, midPos, endPos);
            lineRenderer.SetPosition(i, position);
        }
        
    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // Quadratic Bezier formula: B(t) = (1-t)^2 * P0 + 2(1-t)t * P1 + t^2 * P2
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = uu * p0; // (1-t)^2 * P0
        point += 2 * u * t * p1; // 2(1-t)t * P1
        point += tt * p2; // t^2 * P2

        return point;
    }
}
