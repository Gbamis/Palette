using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "MouseMode", menuName = "Games/Hunt/MouseModeData")]
    public class MouseMode : ScriptableObject
    {
        public enum Mode
        {
            CAMERA = 0,
            UNIT = 1,
            NUTRITION = 2,
            DELETION = 3

        }

        public Mode mode;
    }

}