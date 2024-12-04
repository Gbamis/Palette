using System.Collections.Generic;
using System;
using UnityEngine;

namespace HT
{
    public class Goat_Thief_Processor : MonoBehaviour
    {
        private int spawnedNunmber;
        private Grid grid;
        public int rows;
        public int cols;
        public float cSize;
        public GameObject gridItem;
        public GameObject tile;
        public bool showGrid;

        [SerializeField] private Thief thiefPrefab;
        private List<Reward> reward;

        private List<(int, int)> path_a;
        private List<(int, int)> path_a_a;
        private List<(int, int)> path_a_a_a;

        private void Start()
        {
            grid = new(rows, cols, cSize, transform.position);
            if (showGrid)
            {
                grid.Visualize(gridItem);
            }
            //grid.Visualize(gridItem);

            path_a = new List<(int, int)>();
            path_a_a = new List<(int, int)>();
            path_a_a_a = new List<(int, int)>();

            path_a.Add((1, 9)); path_a.Add((2, 5)); path_a.Add((1, 0));
        }


        public void StartObjectiveProcessing(Objective_Theives  obj)
        {
            reward = obj.reward;

            List<Vector3> waypoints = TransformCordToList(path_a);

            Thief thief = Instantiate(thiefPrefab, transform);
            thief.gameObject.SetActive(true);
            thief.transform.position = waypoints[0];

            UnityEngine.Random.InitState(DateTime.Now.Millisecond);
            float stop = UnityEngine.Random.Range(30, 40);

            thief.OnSpawned(obj.maxHit, stop, waypoints, tile, TheifKilled);
        }

        private void CompleteObjective()
        {
            //conplete
            foreach (Reward rwd in reward) { rwd.ApplyReward(); }
        }

        private void TheifKilled()
        {
            if (spawnedNunmber == 0)
            {
                //complete objective
                CompleteObjective();
            }
            else
            {
                spawnedNunmber -= 1;
            }
        }

        private List<Vector3> TransformCordToList(List<(int, int)> cordinates)
        {
            List<Vector3> points = new();
            foreach ((int, int) cord in cordinates)
            {
                Vector3 pos = grid.GridToWorld(cord.Item1, cord.Item2);
                points.Add(pos);
            }
            return points;
        }
    }
}
