using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace HT
{
    public class NPC_Lumberjack : NPC_Base, IamNPC, IPointerClickHandler
    {
        private bool looping;

        public UI_Lumberjack_View ui_lumberjack;
        public int amount;
        public BankAccount bankAccount;
        public LumberjackController controller;
        [SerializeField] private AudioClip chainSaw;
        public LayerMask treeMask;


        public void OnSpanwed(Vector3 pos, Quaternion rot)
        {
            transform.SetPositionAndRotation(pos, rot);
            PositionIcon();
        }

        public void OnPointerClick(PointerEventData ped)
        {
            ui_lumberjack.gameObject.SetActive(true);
            ui_lumberjack.AddAction(Action_ChopTree, transform);

        }

        private void Hide() { ui_lumberjack.gameObject.SetActive(false); ui_lumberjack.RemoveAction(); }

        private void Action_ChopTree()
        {
            looping = true;

            StartCoroutine(MouseHoveringLoop());
            IEnumerator MouseHoveringLoop()
            {
                while (looping)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit, 200))
                    {
                        if (hit.collider != null)
                        {
                            if (hit.collider.CompareTag("Tree"))
                            {
                                Routine.Instance.mousePointer.Set(ui_lumberjack.activeCursor);
                                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Escape))
                                {
                                    controller.MoveToTree(hit.point, StartCutting);
                                    looping = false;
                                    Routine.Instance.mousePointer.Default();
                                    ui_lumberjack.RemoveAction();
                                    Hide();
                                }
                            }
                            else
                            {
                                Routine.Instance.mousePointer.Set(ui_lumberjack.inactiveCursor);
                            }

                        }
                    }
                    yield return null;
                }
                Hide();
            }
        }

        private void StartCutting()
        {
            GameObject tree = Tree();
            Routine.Instance.audioSourcePrefab.gameObject.SetActive(true);
            Routine.Instance.audioSourcePrefab.clip = chainSaw;
            Routine.Instance.audioSourcePrefab.transform.position = transform.position;
            Routine.Instance.audioSourcePrefab.Play();

            StartCoroutine(Saw());

            IEnumerator Saw()
            {
                yield return new WaitForSeconds(chainSaw.length);
                Routine.Instance.audioSourcePrefab.gameObject.SetActive(false);
                yield return new WaitForSeconds(1);
                SFX.Core.TreeFall(transform.position);
                yield return new WaitForSeconds(9);
                tree.SetActive(false);
            }
        }

        private GameObject Tree(){
            Collider[] tree = new Collider[1];
            int c = Physics.OverlapSphereNonAlloc(transform.position,2,tree,treeMask);
            return tree[0].gameObject;
        }
    }
}
