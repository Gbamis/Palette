using UnityEngine;
using Zenject;

namespace HT
{
    public class UISystem : MonoBehaviour
    {
        [Inject] private readonly AppEvent appEvent;

        public GameObject MENUBAR;
        public UI_Gameplay_Canvas gameplay_canvas;
        public UI_Breeding_Canvas breeding_Canvas;
        public UI_ScreenMarker_Canvas screenMarker_canvas;


        private void Start()
        {
            appEvent.OnGameStarted += () => MENUBAR.SetActive(true);

            InitializeCanvas();
        }

        private void InitializeCanvas()
        {
            gameplay_canvas.OnInit();
            breeding_Canvas.OnInit();
            screenMarker_canvas.OnInit();

            MENUBAR.SetActive(false);
        }


    }
}
