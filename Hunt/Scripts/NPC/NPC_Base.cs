using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using Zenject;

namespace HT
{
    public class NPC_Base : MonoBehaviour
    {
        public Dialog dialog;
        protected Button talk;

        

        protected void PositionIcon()
        {
            if (talk == null)
            {
                //talk = gameEvent.OnOnCreateTalkButtonOver(transform);
                talk.gameObject.SetActive(true);
            }

            StartCoroutine(TrackButton(talk.GetComponent<RectTransform>()));
            IEnumerator TrackButton(RectTransform rect)
            {
                rect.localScale = Vector3.one * 0.2f;
                while (true)
                {
                    Vector3 body = transform.position;
                    body.y += 3;
                    Vector2 pos = Camera.main.WorldToScreenPoint(body);
                    pos.x = Mathf.Clamp(pos.x, 100, Screen.width - 10);
                    pos.y = Mathf.Clamp(pos.y, 100, Screen.height - 10);
                    rect.position = pos;
                    yield return null;
                }
            }
        }

    }



}