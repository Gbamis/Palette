using UnityEngine;
using System;
using System.Collections.Generic;

namespace HT
{
    public class Parasite : MonoBehaviour, IDamageable
    {
        public Action OnKilled;
        private int health = 5;
        public void TakeDamage(int val)
        {
            health -= val;
            if (health <= 0)
            {
                Destroy(gameObject);
                OnKilled();
            }
        }
    }

    public class Parasite_Processor : MonoBehaviour
    {
        private List<Reward> rewards;
        private Action OnCompleted;
        private int xp;
        private int num_of_parrasite;

        public Transform pirch_group;
        public Transform spawned;
        public GameObject parrasitePrefab;
        public LayerMask pirchMask;


        public void StartObjectiveProcessing(Objective_KillParrasite obj, Action completeCallback)
        {
            rewards = obj.reward;
            xp = obj.xp_level;

            OnCompleted = completeCallback;

            ScanAvailablePirchPoints();

        }

        private void ScanAvailablePirchPoints()
        {
            float radius = xp +10;
            Collider[] cols = new Collider[xp + 1];
            Physics.OverlapSphereNonAlloc(pirch_group.position, radius, cols,pirchMask);

            DebugCheck check = new DebugCheck(){
                center = pirch_group.position,
                debugColor = Color.black,
                radius = radius
            }; 

            Routine.Instance.AddInfo(check);

            num_of_parrasite = UnityEngine.Random.Range(1, xp + 2);
            for (int i = 0; i < num_of_parrasite; i++)
            {
                UnityEngine.Random.InitState(DateTime.Now.Millisecond * i);
                int randPoint = UnityEngine.Random.Range(0, cols.Length);
                Debug.Log("rand: " + randPoint);
                CreateParrasiteAt(cols[randPoint].transform);
            }
            Debug.Log("bugsss" + num_of_parrasite);
        }

        private void CreateParrasiteAt(Transform loc)
        {
            GameObject clone = Instantiate(parrasitePrefab, loc.position, loc.rotation, spawned);
            clone.AddComponent<Parasite>().OnKilled = External_ParrasiteKilled;
            clone.SetActive(true);
        }

        private void External_ParrasiteKilled()
        {
            num_of_parrasite--;
            if (num_of_parrasite == 0)
            {
                CompleteObjective();
                OnCompleted?.Invoke();
            }
        }

        private void CompleteObjective()
        {
            //conplete
            foreach (Reward rwd in rewards) { rwd.ApplyReward(); }
        }
    }
}
