using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace HT
{

    [Serializable]
    public struct Response
    {
        public string text;
        public int branchDialogStatement;
        public DialogAction dialogAction;
    }


    [CreateAssetMenu(fileName = "DialogStatement", menuName = "Games/Hunt/Dialog/Statement")]
    public class DialogStatement : ScriptableObject
    {
        private List<string> lines = new();
        private bool responseTaken;
        private Dialog dialog;


        [SerializeField] private float readSpeed = 0.001f;
        [TextArea(3, 100)] public string conversation;
        [SerializeField] private List<Response> responses;


        public IEnumerator OnRead(Dialog m_dialog, Transform self = null)
        {
            dialog = m_dialog;

            lines = conversation.Split("#").ToList();
            lines.RemoveAt(0);
            int index = 0;

            while (index < lines.Count)
            {
                foreach (string line in lines)
                {
                    char who = line[0];
                    Say(who, line, self);
                    yield return new WaitForSeconds(readSpeed);
                    index++;
                }
            }

            if (responses.Count > 0)
            {
               // yield return Routine.Instance.StartCoroutine(OnReadQuestion());
            }

        }

        /*public IEnumerator OnReadQuestion()
        {
            responseTaken = false;
            UI_Dialog_Canvas canvas = gameEvent.OnGetDialogCanvas();
            foreach (Response res in responses)
            {
                canvas.AddResponseBtn(dialog, res, ActionTaken);
            }
            yield return new WaitUntil(() => responseTaken);
            canvas.ClearResponseBtn();

        }*/

        private void ActionTaken() => responseTaken = true;

        private void Say(char who, string what, Transform root)
        {
            //gameEvent.OnGetDialogCanvas().OutputMsg('.', what, dialog.npc_name, root);
        }
    }

}