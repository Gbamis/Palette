using Unity.VisualScripting;
using UnityEngine;

namespace HT
{
    public class ShepherdScanner : MonoBehaviour
    {
        public LayerMask dangerMask;
        private float _fov;
        private float _view_distance;
        public bool inside;
        public bool seen;

        public float _angle;

        public (bool, Transform) IsDangerSeen(float fov, float view_distance)
        {
            _fov = fov;
            _view_distance = view_distance * 2;

            Collider[] col = new Collider[1];
            int val = Physics.OverlapSphereNonAlloc(transform.position, _view_distance, col, dangerMask);
            inside = col[0] != null ? true : false;
            if (col[0] != null)
            {
                if (WithinFOV(col[0].transform.position))
                {
                    seen = CanSeeDanger(col[0].transform);
                }
                else
                {
                    seen = false;
                }

                return (seen, col[0].transform);
            }
            return (false, null);
        }

        private bool CanSeeDanger(Transform obj)
        {
            Vector3 origin = transform.position;
            origin.y += 2;
            Vector3 dir = (obj.position - origin).normalized;

            Debug.DrawRay(origin, dir * _view_distance, Color.black);

            if (Physics.Raycast(origin, dir, out RaycastHit hit, _view_distance, dangerMask))
            {
                if (hit.collider != null)
                {
                    return true;
                }
            }
            return false;
        }

        private bool WithinFOV(Vector3 other)
        {
            Vector3 dir = (other - transform.position).normalized;
            _angle = Vector3.Angle(transform.forward, dir);
            return _angle <= _fov / 2;
        }

        private void OnDrawGizmos()
        {
            Color red = Color.red;
            red.a = 0.2f;
            Gizmos.color = red;
            Gizmos.DrawSphere(transform.position, _view_distance);
        }
    }
}
