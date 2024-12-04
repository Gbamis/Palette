using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HT
{
    public class Farm_Pen : MonoBehaviour, IFarmStruct
    {
        private Dictionary<GameObject, bool> fences;
        public Transform fenceRoot;

        private void Start()
        {
            fences = new Dictionary<GameObject, bool>();
            foreach (Transform child in fenceRoot)
            {
                fences.Add(child.gameObject, false);
            }
        }

        public void CreateDamageFences(int num)
        {
            num = Mathf.Clamp(num, 1, fences.Values.Count);

        }

        public Stack<Transform> GetDamageFences()
        {
            Stack<Transform> data = new Stack<Transform>();
            foreach (KeyValuePair<GameObject, bool> kv in fences)
            {
                if (kv.Value == true)
                {
                    data.Push(kv.Key.transform);
                }
            }
            return data;
        }

        public void FixFenceAt(int index) => fences.Keys.ElementAt(index).SetActive(false);
    }

}