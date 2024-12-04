using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public class UI_Shepherd_View : UI_Views
    {
        public Transform listView;
        public UI_ShepherdItem uiPrefab;

        public void CreateSHepherdListView(List<ShepherdData> data)
        {
            foreach (ShepherdData sp in data)
            {
                UI_ShepherdItem item = Instantiate(uiPrefab, listView);
                item.gameObject.SetActive(true);
                item.GetComponent<RectTransform>().localScale = Vector2.one * 0.3f;
                item.SetData(sp);
            }
        }
        // [SerializeField] private UI_ShepherdUpgrade_View upgradeView;


        /* public UI_ShepherdUpgrade_View CreateView()
         {
             UI_ShepherdUpgrade_View view = Instantiate(upgradeView);
             view.transform.SetParent(transform);
             return view;
         }*/


    }
}
