using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HT
{
    [Serializable]
    public struct Callout
    {
        public Text id;
        public Text msg;
        public RectTransform rectTransform;
        public void Enable(bool value) => rectTransform.gameObject.SetActive(value);
        public void Print(string m_id, string m_msg, Transform root = null)
        {
            id.text = m_id; msg.text = m_msg;
            Vector2 pos = Camera.main.WorldToScreenPoint(root.position);
            rectTransform.position = pos;
        }
    }

    public class UI_Dialog_Canvas : UICanvas
    {
        private List<DialogButton> dialogButtons = new List<DialogButton>();
        public UI_Dialog_View dialog_View;
        private DialogModel model;

        [SerializeField] private Transform buttonList;
        [SerializeField] private DialogButton dialogButton;


        public override void OnInit()
        {
            base.OnInit();

            DisableDialog();
        }

        public override void OnDisplay() => base.OnDisplay();

        public override void OnHide()
        {
            base.OnHide();

        }

        public void AddResponseBtn(Dialog m_dialog, Response m_response, Action completed)
        {
            DialogButton button = Instantiate(dialogButton, buttonList);
            button.Create(m_dialog, m_response, completed);
            button.gameObject.SetActive(true);
            dialogButtons.Add(button);
        }

        public void ClearResponseBtn()
        {
            foreach (DialogButton bt in dialogButtons) { Destroy(bt.gameObject); }
            dialogButtons.Clear();
        }


        public void DisableDialog() => dialog_View.gameObject.SetActive(false);

        public void OutputMsg(char id, string msg, string npc, Transform root)
        {
            //callout.Print(npc, msg,root);
            //callout.Enable(true);
            dialog_View.gameObject.SetActive(true);
            model.PresentView(dialog_View, npc, msg, root.position);

        }
    }
}
