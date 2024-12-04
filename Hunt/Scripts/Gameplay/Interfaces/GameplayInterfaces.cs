using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public interface IColorInjectable
    {
        void InjectColors(List<Color> colors);
    }
}
