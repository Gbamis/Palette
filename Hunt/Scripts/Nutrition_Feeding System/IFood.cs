using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public interface IFood
    {
        public float quantity { set; get; }
        float Consume(float rate);
    }

}