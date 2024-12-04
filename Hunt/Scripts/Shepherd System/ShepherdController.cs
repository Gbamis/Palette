using UnityEngine;
using UnityEngine.AI;
using System;
using Cysharp.Threading.Tasks;

namespace HT
{
    public class ShepherdController : MonoBehaviour
    {
        private readonly int forwardHash;
        public Animator anim;
        public NavMeshAgent agent;
        public Collider self;
        public static readonly int forwardAnim = Animator.StringToHash("forward");


        public void Move(Vector3 point, Action callback = null)
        {
            MoveController(point).Forget();
        }

        private async UniTaskVoid MoveController(Vector3 point)
        {
            agent.enabled = true;
            float defSpeed = agent.speed;
            bool run = Vector3.Distance(transform.position, point) > 10;
            agent.speed = run ? agent.speed += 2 : agent.speed;
            
            agent.SetDestination(point);

            while (agent.enabled && (agent.pathPending || agent.remainingDistance > agent.stoppingDistance))
            {
                anim.SetFloat(forwardAnim, run ? 2 : 1);
                await UniTask.Yield();
            }
            StopMove();
            agent.speed = defSpeed;
        }

        public void StopMove()
        {
            anim.SetFloat(forwardAnim, 0);
            agent.enabled = false;
        }
    }

}