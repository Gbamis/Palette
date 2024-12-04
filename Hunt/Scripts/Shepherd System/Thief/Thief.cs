using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HT
{
    public class Thief : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDamageable
    {
        private UI_ThiefInfo_View infoView;

        private List<GameObject> m_items;
        private List<Vector3> _waypoints;
        private int maxHit;
        private int hitLeft;
        private Action OnThiefkilled;
        [SerializeField] private Collider bodyCollider;
        [SerializeField] private ThiefController controller;


        public void OnSpawned(int hit, float time,List<Vector3> paths, GameObject gridItem, Action OnKilled)
        {
            hitLeft = hit;
            maxHit = hit;
            _waypoints = paths;
            OnThiefkilled = OnKilled;
            CreateVisualPath(gridItem);
            InitiateAction(time).Forget();
        }

        private async UniTaskVoid InitiateAction(float stop)
        {
           /* UI_Countdown_View view = gameEvent.OnGetCountDownView();
            view.gameObject.SetActive(true);

            float count = stop;

            while (count > 0)
            {
                Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
                pos.x = Mathf.Clamp(pos.x, 100, Screen.width - 10);
                pos.y = Mathf.Clamp(pos.y, 100, Screen.height - 10);
                view.rect.position = pos;

                view.UpdateView(count, stop);
                count -= Time.deltaTime;
                await UniTask.Yield();
            }

            Destroy(view.gameObject);
            controller.MoveToEndPoint(_waypoints.ToArray());*/
        }

        public void TakeDamage(int damage)
        {
            if (hitLeft == 0)
            {
                OnKilled();
            }
            else
            {
                hitLeft -= damage;
            }
        }

        private void OnKilled()
        {
            Destroy(infoView);
            OnThiefkilled();
        }


        public void OnPointerEnter(PointerEventData ped)
        {
            /*DisplayVisualPath(true);
            infoView = infoView == null ? gameEvent.OnGetThiefInfoView() : infoView;
            infoView.gameObject.SetActive(true);
            infoView.Set(maxHit);
            Vector3 pos = transform.position;
            pos.y += 3;
            Vector2 sPos = Camera.main.WorldToScreenPoint(pos);
            infoView.rect.position = sPos;*/
        }

        public void OnPointerExit(PointerEventData ped)
        {
            //reveal paths
            //show max hit
             DisplayVisualPath(false);
            infoView.Hide();
        }

        private void CreateVisualPath(GameObject pref)
        {
            m_items = new List<GameObject>();
            foreach (Vector3 vec in _waypoints)
            {
                GameObject clone = Instantiate(pref, vec, Quaternion.identity);
                clone.SetActive(false);
                m_items.Add(clone);
            }
        }

        private void DisplayVisualPath(bool val)
        {
            foreach (GameObject item in m_items) { item.SetActive(val); }
        }
    }
}
