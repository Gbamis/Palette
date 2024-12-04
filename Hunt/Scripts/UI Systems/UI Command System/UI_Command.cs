using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HT
{
    public class UI_Command : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public MouseMode mouseMode;
        private AnimalAction command;
        private CommandSystem cs;

        public Image iconpart;

        public void SetCommand(AnimalAction cmd, CommandSystem cmds, Sprite icon)
        {
            command = cmd;
            cs = cmds;
            iconpart.sprite = icon;
        }

        public void OnPointerClick(PointerEventData ped) {}

        public void OnPointerEnter(PointerEventData ped) => mouseMode.mode = MouseMode.Mode.CAMERA;
        public void OnPointerExit(PointerEventData ped) => mouseMode.mode = MouseMode.Mode.UNIT;


    }
}
