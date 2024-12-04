using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HT
{
    [Serializable]
    public class DebugCheck
    {
        [HideInInspector] public Vector3 center;
        public Color debugColor;
        public float radius;

    }

    [Serializable]
    public struct MousePointer
    {
        public Texture2D defaultCursor;
        public readonly void Set(Texture2D text) => Cursor.SetCursor(text, Vector2.zero, CursorMode.Auto);
        public readonly void Default() => Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    public class Routine : MonoBehaviour
    {
        [Inject] private GameplayEvent gameplayEvent;

        [HideInInspector] public List<DebugCheck> gizmos = new List<DebugCheck>();
        public Transform player;
        public MouseClicker moveIcon;
        public MousePointer mousePointer;
        public AudioSource audioSourcePrefab;


        public static Routine Instance { set; get; }

        private void Awake()
        {
            Instance = this;
            gameplayEvent.gameState = GAMESTATE.STOPPED;
            
            mousePointer.Default();  
        }


        public void AddInfo(DebugCheck info)
        {
            if (gizmos.Contains(info)) { return; }
            gizmos.Add(info);
            StartCoroutine(Remove());
            IEnumerator Remove()
            {
                yield return new WaitForSeconds(1);
                if (gizmos.Contains(info)) { gizmos.Remove(info); }
            }
        }

        private void OnDrawGizmos()
        {
            if (gizmos.Count == 0) { return; }
            foreach (DebugCheck info in gizmos)
            {
                Gizmos.color = info.debugColor;
                Gizmos.DrawSphere(info.center, info.radius);
            }
        }
    }
}
