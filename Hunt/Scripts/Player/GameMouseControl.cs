using UnityEngine;
using System.Collections.Generic;
using Zenject;

namespace HT
{
    [System.Serializable]
    public class Config
    {
        public Transform cam;
        public Transform self;
    }
    public class GameMouseControl : MonoBehaviour
    {
        [Inject] private GameplayEvent gameplayEvent;
        public GameObject rangeIcon;
        public Config config;

        [SerializeField] private List<Control> abilities;

        private void Awake()
        {
            gameplayEvent.OnShowSelectionRange += ShowSelectionRange;
        }

        private void Update()
        {
            { foreach (Control ab in abilities) { ab.OnUpdate(config); } }
        }

        private void ShowSelectionRange()
        {
            rangeIcon.SetActive(true);
            Invoke(nameof(HideRnage), 2);
        }
        private void HideRnage() => rangeIcon.SetActive(false);


    }

}