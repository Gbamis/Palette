using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace HT
{
    public class LumberjackController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator anim;

        public void MoveToTree(Vector3 point, Action atPoint)
        {
            agent.enabled = true;
            agent.SetDestination(point);

            StartCoroutine(Move());

            IEnumerator Move()
            {
                while (Vector3.Distance(transform.position, point) > 2.6f)
                {
                    float dis = Vector3.Distance(transform.position, point);
                    Debug.Log(dis);
                    MotionValue(1);
                    yield return null;
                }
                MotionValue(0);
                atPoint?.Invoke();
                agent.enabled = false;
                Debug.Log("Reached");
            }
        }

        private void MotionValue(float val) => anim.SetFloat("forward", val);
    }
}
