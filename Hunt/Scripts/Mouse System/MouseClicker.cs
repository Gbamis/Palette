using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public class MouseClicker : MonoBehaviour
    {
        private Coroutine run;
        public Transform dirIcon;

        private void OnEnable() => run = StartCoroutine(Active());

        public void Dropped()
        {
            gameObject.SetActive(false);
        }

        IEnumerator Active()
        {
            while (gameObject.activeSelf)
            {
                dirIcon.LookAt(Camera.main.transform);
                yield return null;
            }
        }
    }

}