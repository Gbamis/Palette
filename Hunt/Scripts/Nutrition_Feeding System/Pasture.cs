using System;
using UnityEngine;

namespace HT
{
    public class Pasture : MonoBehaviour
    {
        [SerializeField] private GameObject grassClump_prefab;
        [SerializeField] private Transform spanedRoot;
        [SerializeField] private Transform ray_casts;
        [SerializeField] private LayerMask mask;
        [SerializeField] private float hieghtCheck;

        public bool showDebug;
        public Color gizmoColor;
        public float radius;


        private void Start() => CreatePasture();

        public void CreatePasture()
        {
            for (int i = 0; i < ray_casts.childCount; i++)
            {
                Vector3 sp = Point(ray_casts.GetChild(i).position);
                Quaternion rot = ray_casts.GetChild(i).rotation;
                GameObject grass = Instantiate(grassClump_prefab, sp, rot, spanedRoot);
                grass.SetActive(true);
            }
            ray_casts.gameObject.SetActive(false);
        }

        public Vector3 Point(Vector3 origin)
        {
            Vector3 pos = Vector3.zero;
            origin.y += hieghtCheck;

            if (Physics.Raycast(origin, Vector3.down, out RaycastHit info, 200, mask))
            {
                if (info.collider != null)
                {
                    pos = info.point;
                }
            }
            return pos;
        }

        private void OnDrawGizmos()
        {
            if (ray_casts.childCount > 0 && showDebug)
            {
                Gizmos.color = gizmoColor;
                foreach (Transform child in ray_casts)
                {
                    Gizmos.DrawSphere(child.position, radius);
                }
            }
        }
    }

}