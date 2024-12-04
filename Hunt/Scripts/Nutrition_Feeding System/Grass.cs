using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public class Grass : MonoBehaviour, IFood
    {
        public float quantity { set; get; }
        public float editorValue;

        private void Awake() => quantity = editorValue;

        public float Consume(float rate)
        {
            try
            {
                if (gameObject != null)
                {
                    if (quantity < 0) { Destroy(gameObject); }
                    quantity -= rate;
                }
            }
            catch (System.Exception e) { }

            return rate;
        }
    }

}