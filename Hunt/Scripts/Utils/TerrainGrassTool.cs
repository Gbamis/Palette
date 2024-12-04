using UnityEngine;

public class TerrainGrassTool : MonoBehaviour
{
    public float startOffset;
    public float rayLength = 10f;
    [SerializeField] private LayerMask terrain;

    public void Adjust()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 sp = Point(transform.GetChild(i).position);
            transform.GetChild(i).transform.position = sp;
        }
    }

    public Vector3 Point(Vector3 origin)
    {
        Vector3 pos = Vector3.zero;
        origin.y += startOffset;

        if (Physics.Raycast(origin, Vector3.down, out RaycastHit info, 200, terrain))
        {
            if (info.collider != null)
            {
                pos = info.point;
            }
        }
        return pos;
    }
}
