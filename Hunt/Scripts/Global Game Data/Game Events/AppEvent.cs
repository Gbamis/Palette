using UnityEngine;
using System;

namespace HT
{
    public enum GAMESTATE { RUNNING, STOPPED }

    [CreateAssetMenu(fileName = " AppEvents", menuName = "Games/Hunt/EventChannel/AppEvents")]
    public class AppEvent : ScriptableObject
    {
        public GAMESTATE gameState;
        public Action OnGameStarted;

        public void Raise_OnApp_GameStarted()
        {
            OnGameStarted?.Invoke();
            gameState = GAMESTATE.RUNNING;
        }
    }
}
