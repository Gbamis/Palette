using UnityEngine;
using System.Collections.Generic;

namespace HT
{
    public class UI_BreedList_View : UI_Views
    {
        public MouseMode mouseMode;
        [SerializeField] private Transform list;
        [SerializeField] private UI_ColorItem colorItem;
        [SerializeField] private List<Color> m_colors;

        public void MouseIn() => mouseMode.mode = MouseMode.Mode.UNIT;
        public void MouseOut() => mouseMode.mode = MouseMode.Mode.CAMERA;

        public void CreateColorItems(List<Color> colors)
        {
            m_colors = colors;
            RedrawUI();
        }

        public void RemoveAllColors()
        {
            m_colors.Clear();
        }

        public void RemoveColor(Color color)
        {
            if (m_colors.Contains(color))
            {
                m_colors.Remove(color);
            }
            RedrawUI();
        }

        private void RedrawUI()
        {
            if (list.childCount > 0)
            {
                foreach (Transform child in list)
                {
                    Destroy(child.gameObject);
                }
            }

            foreach (Color col in m_colors)
            {
                UI_ColorItem ui = Instantiate(colorItem, list);
                ui.SetColor(col);
                ui.gameObject.SetActive(true);
            }

        }
    }
}
