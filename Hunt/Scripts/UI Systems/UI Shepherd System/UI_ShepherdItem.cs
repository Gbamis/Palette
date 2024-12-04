using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;

namespace HT
{
    public class UI_ShepherdItem : MonoBehaviour, IPointerClickHandler
    {
        public Image activeImage;
        public Image shIcon;
        private ShepherdData m_data;
        private Shepherd waiting;
        private bool dropped;
        private bool IsSlotUsed;

        public void SetData(ShepherdData data)
        {
            shIcon.sprite = data.icon;
            m_data = data;
        }

        public void OnPointerClick(PointerEventData ped)
        {
            if (!IsSlotUsed)
            {
                waiting = waiting != null ? waiting : Instantiate(m_data.shepherdPrefab);
                waiting.gameObject.SetActive(true);
                Mouse(waiting.transform).Forget();
            }
        }

        private async UniTaskVoid Mouse(Transform child)
        {
            dropped = false;
            while (!dropped)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100, m_data.layerMask))
                {
                    if (hit.collider != null)
                    {
                        child.position = hit.point;
                        if (Input.GetMouseButtonDown(0))
                        {
                            waiting.transform.position = hit.point;
                            dropped = true;
                            IsSlotUsed = true;
                            waiting.OnSpawn(m_data);
                            activeImage.color = Color.black;
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    waiting.gameObject.SetActive(false);
                    dropped = true;
                }

                await UniTask.Yield();
            }
        }
    }
}
