using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "Dialog", menuName = "Games/Hunt/Dialog/NpcDialog")]
    public class Dialog : ScriptableObject
    {
        public DialogStatement currentStatement;
        public List<DialogStatement> dialogStatements;
        public string npc_name;


        public void Read(Transform self = null)
        {
            currentStatement = currentStatement != null ? currentStatement : dialogStatements[0];
            Routine.Instance.StartCoroutine(Process());
            IEnumerator Process()
            {
                yield return Routine.Instance.StartCoroutine(currentStatement.OnRead(this, self));
                //gameEvent.OnBlockPlayerInput(false);
                //gameEvent.OnGetDialogCanvas().DisableDialog();
            }
        }

        public void StopReading()
        {
            Routine.Instance.StopAllCoroutines();
            //gameEvent.OnGetDialogCanvas().DisableDialog();
        }


        /*[TextArea(3, 60)] public string conversation;
        private List<string> lines = new();
        private bool nextClicked;
        [SerializeField] private float readSpeed = 0.001f;

        public void Init()
        {
            /GameEvent.Instance.OnDIalogNextClicked += (() =>
            {
                nextClicked = true;
            });
        }
        public void Read()
        {
            lines = conversation.Split("#").ToList();
            lines.RemoveAt(0);
            string statement = "";
            int index = 0;
            nextClicked = false;

            Routine.Instance.StartCoroutine(ReadLines());
            IEnumerator ReadLines()
            {
                //GameManager.Instance.BlockInput = true;
                while (index < lines.Count)
                {
                    foreach (string line in lines)
                    {
                        nextClicked = false;
                        statement = line.Substring(0).Trim();
                        StringBuilder sb = new();
                        foreach (char letter in statement)
                        {
                            sb.Append(letter);
                            string output = sb.ToString();
                            //GameEvent.Instance.OnDialogTextOutput?.Invoke(output);
                            yield return new WaitForSeconds(readSpeed);
                        }
                        //GameEvent.Instance.OnDIalogLineRead?.Invoke();
                        yield return new WaitUntil(() => nextClicked);
                        index++;
                    }
                }
                //GameEvent.Instance.OnDialogClosed?.Invoke();
                //GameManager.Instance.BlockInput = false;
            }
        }*/
    }
}
