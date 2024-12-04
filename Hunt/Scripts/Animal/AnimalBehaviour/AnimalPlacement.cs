
using UnityEngine;

namespace HT
{
    public class AnimalPlacement : MonoBehaviour
    {
        public LayerMask placementLayers;
        public float offset;

        public void AdjustPlacement()
        {
            Vector3 pos = transform.position;
            pos.y +=offset;
            Vector3 origin = pos;
            Vector3 dir = Vector3.down * 10;

            if (Physics.Raycast(origin, dir, out RaycastHit hit, 100, placementLayers))
            {
                if (hit.collider != null)
                {
                    Vector3 endPos = transform.position;
                    endPos.y = hit.point.y;
                    transform.position = endPos;
                }
            }
            //Debug.DrawRay(origin, dir, Color.yellow);
        }
    }

}