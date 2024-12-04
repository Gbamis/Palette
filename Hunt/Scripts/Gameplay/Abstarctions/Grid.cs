using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HT
{

    public class Grid
    {
        private int _row;
        private int _col;
        private float cellSize;
        private Vector3 origin;

        public Grid(int row, int col, float cell, Vector3 orig)
        {
            cellSize = cell;
            origin = orig;
            _row = row;
            _col = col;
        }

        public void Visualize(GameObject pref)
        {
            for (int col = 0; col < _col; col++)
            {
                for (int row = 0; row < _row; row++)
                {
                    Vector3 pos = GridToWorld(row, col);
                    GameObject clone = MonoBehaviour.Instantiate(pref, pos, Quaternion.identity);
                    clone.GetComponentInChildren<Text>().text = row.ToString()+" , "+col.ToString();
                    clone.SetActive(true);
                }
            }
        }

        public Vector3 GridToWorld(int x, int z)
        {
            Vector3 pos = new(x * cellSize, 0, z * cellSize);
            pos += origin;
            return pos;
        }

        public (int x, int y) WorldToGrid(Vector3 point)
        {
            Vector3 pos = point - origin;
            float xx = pos.x / cellSize;
            float zz = pos.z / cellSize;
            int x = (int)Mathf.Ceil(xx) - 1;
            int y = (int)Mathf.Ceil(zz) - 1;
            return (x, y);
        }
    }

}