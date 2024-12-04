using UnityEngine;
using Zenject;

namespace HT
{
    [System.Serializable]
    public struct Virus
    {
        public float spread;
        public float value;
        public float outBreakInterval;
        public LayerMask layerMask;
    }

    public class Environment_VirusOutbreak : MonoBehaviour
    {
        private Vector3 outbreakPoint;
        private float step = 0;
        public Virus virus;

        private void Update()
        {
            if (step > virus.outBreakInterval)
            {
                Vector2 rand = Random.insideUnitCircle;
                outbreakPoint =  Routine.Instance.player.position;
                outbreakPoint.x += rand.x;
                outbreakPoint.z += rand.y;

                DiseasesOutbreakAt(outbreakPoint);
                step = 0;
            }
            else
            {
                step += Time.deltaTime;
            }
        }

        public void DiseasesOutbreakAt(Vector3 pos)
        {
            Collider[] agents = Physics.OverlapSphere(pos, virus.spread, virus.layerMask);
            if (agents.Length > 0)
            {
                foreach (Collider col in agents)
                {
                    col.TryGetComponent<IHealth>(out IHealth health);
                    health.TriggerSickness(virus.value);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Color color = Color.blue;
            color.a = 0.5f;
            Gizmos.color = color;
            Gizmos.DrawSphere(outbreakPoint, virus.spread);
        }
    }

}