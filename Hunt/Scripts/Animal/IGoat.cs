using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public interface IGoat
    {
        public Gene gene { set; get; }
        public Gender gender { set; get; }

        public bool isOnHeat();

    }
}
