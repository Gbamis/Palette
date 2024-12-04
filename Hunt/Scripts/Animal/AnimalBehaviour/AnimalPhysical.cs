using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HT
{
    public class AnimalPhysical : MonoBehaviour
    {
        public List<SkinnedMeshRenderer> referenced_renderer;

        public void SetPhysicalCharcateristics(Color color)
        {
            foreach (SkinnedMeshRenderer sr in referenced_renderer)
            {
                sr.material.color = color;
            }
        }

        public void SetPhysicalCharcateristics(float r, float g, float b)
        {
            Color color = new(r, g, b, 1);
            foreach (SkinnedMeshRenderer sr in referenced_renderer)
            {
                sr.material.color = color;
            }
        }
    }

}