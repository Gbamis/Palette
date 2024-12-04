using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HT
{
    public class ShepherdSystem : MonoBehaviour
    {
        [Inject] private AppEvent appEvent;

        [Header("Shepherd classes")]
        public UI_Shepherd_View shepherd_View;
        public List<ShepherdData> shepherdData;


        private void Start() => appEvent.OnGameStarted += () => LoadData();

        private void LoadData() => shepherd_View.CreateSHepherdListView(shepherdData);


    }

}