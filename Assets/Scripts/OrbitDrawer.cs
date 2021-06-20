using UnityEngine;

public class OrbitDrawer : MonoBehaviour
{
    [SerializeField] private GameObject dot;
    [SerializeField] private LineRenderer lineRenderer;

    public void SetupOrbit(float r, bool isHorizontal = false, float orbitMultiplier = 2f, bool positiveOrbit = false)
    {
        int segments = 360;
        lineRenderer.positionCount = segments;

        int pointCount = segments;
        Vector3[] points = new Vector3[pointCount];


        if(isHorizontal)
        {
            for(int i = 0; i < pointCount; i++)
            {
                float rad = Mathf.Deg2Rad * (i * 360f / segments);
                points[i] = new Vector3(Mathf.Sin(rad) * r, Mathf.Cos(rad) * r, 0);
            }

            lineRenderer.SetPositions(points);
        }
        else
        {
            for(int i = 0; i < pointCount; i++)
            {
                float rad = Mathf.Deg2Rad * (i * 360f / segments);
                float x = r * Mathf.Sin(rad);
                float y = r * Mathf.Cos(rad);

                if(positiveOrbit)
                {
                    points[i] = new Vector3(x, x / orbitMultiplier, y);
                }
                else
                {
                    points[i] = new Vector3(x, y / orbitMultiplier, y);
                }
            }

            lineRenderer.SetPositions(points);
        }
    }
}
