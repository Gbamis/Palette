using UnityEngine;
namespace HT
{
    public class UI_ScreenMarker_Canvas : UICanvas
    {
        [Header("Map Marker")]
        public Transform markerHolder;
        public UI_ScreenMarker screenMarker;

        public override void OnInit()
        {
            base.OnInit();
            uiEvent.OnCreateMarker += OnCreateMarker;
        }

        public void OnDisable() => uiEvent.OnCreateMarker -= OnCreateMarker;

        private UI_ScreenMarker OnCreateMarker(Transform obj, Sprite icon = null)
        {
            UI_ScreenMarker clone = Instantiate(screenMarker, markerHolder);
            clone.Init(obj, icon);
            clone.gameObject.SetActive(true);
            return clone;
        }
    }
}
