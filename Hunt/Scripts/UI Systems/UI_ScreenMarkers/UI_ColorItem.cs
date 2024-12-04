using UnityEngine;
using UnityEngine.UI;

namespace HT
{
    public class UI_ColorItem : MonoBehaviour
    {
        [SerializeField] private Image bg;

        public void SetColor(Color color) => bg.color = color;
    }

}