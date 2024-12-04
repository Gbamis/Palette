using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace HT
{
    public class ThiefController : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        [SerializeField] private NavMeshAgent agent;
        public static readonly int forwardAnim = Animator.StringToHash("forward");

        public void MoveToEndPoint(Vector3[] waypoints)
        {
            agent.enabled = true;
            agent.speed += 2;
            int currentIndex = 0;

            StartCoroutine(Move());
            IEnumerator Move()
            {
                anim.SetFloat(forwardAnim, 2);
                while (currentIndex != waypoints.Length)
                {
                    agent.SetDestination(waypoints[currentIndex]);
                    while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
                    {
                        yield return null;
                    }
                    currentIndex += 1;
                }
                agent.speed -= 2;
                agent.enabled = false;
                anim.SetFloat(forwardAnim, 0);
            }
        }
    }

}