using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using Zenject;

namespace HT
{
    public class Shepherd : MonoBehaviour, IPointerClickHandler, ICommandable, IHoverable
    {
        private ShepherdController controller;
        private ShepheredFOV shepheredFov;
        private ShepherdScanner shepherdScanner;
        private ShepherdData shepherdData;

        private BaseWeapon baseWeapon;

        private bool selected;
        private bool isExecutingCommand;
        private bool isDirty;

        public MouseMode mouseMode;
        public GameObject anchor;
        public Transform left_goal;
        public Transform right_goal;



        public void OnSpawn(ShepherdData data)
        {
            controller = GetComponent<ShepherdController>();
            shepheredFov = GetComponentInChildren<ShepheredFOV>();
            shepherdScanner = GetComponent<ShepherdScanner>();

            shepherdData = data;
            Transform handle = data.weaponFactory.Goal() == HAND.LEFT ? left_goal : right_goal;
            baseWeapon = data.weaponFactory.ActivateWeaponFromUpgrade(handle, controller.anim);

            //ConnectUpgrade(bankAccount.GetWeaponUpgradeAfterLevel(shepherdData.attainedLevel));

            isDirty = true;
            Unit_ShowFOV_Effect();
        }

        /*private void SetUnlockedWeapons(int dataHash){
            unlockedWeaponHashes = new List<int>();
        }


        private void ConnectUpgrade(WeaponFactory upgrade)
        {

            baseWeapon?.DeactivateWeapon();
            Transform handle = upgrade.Goal() == HAND.LEFT ? left_goal : right_goal;
            baseWeapon = upgrade.ActivateWeaponFromUpgrade(handle, controller.anim);
        }

        private void OnUpgrade()
        {
            if (bankAccount.CanUpgradeFrom(shepherdData.attainedLevel))
            {
                shepherdData.attainedLevel++;
                WeaponFactory wf = bankAccount.GetWeaponUpgradeAfterLevel(shepherdData.attainedLevel);
                ConnectUpgrade(wf);
                //unlockedWeaponHashes.Add(wf.factoryHash);
            }

            OnDeSelect();
        }
        private void OnDownGrade()
        {
            shepherdData.attainedLevel--;
            ConnectUpgrade(bankAccount.GetWeaponUpgradeAfterLevel(shepherdData.attainedLevel));
            OnDeSelect();
        }

        private void Update()
        {
            if (view != null && view.gameObject.activeSelf)
            {
                view.FollowTransform(transform);
            }
        }*/

        private void FixedUpdate()
        {
            if (!isDirty) { return; }
            (bool, Transform) found = shepherdScanner.IsDangerSeen(baseWeapon.field_of_view,
             baseWeapon.view_distance);

            if (found.Item1)
            {
                controller.StopMove();
                baseWeapon.UseWeaponOn(found.Item2);
            }
        }

        public void OnPointerClick(PointerEventData ped)
        {
            if (isDirty)
            {
                if (selected)
                {
                    OnDeSelect();
                    //gameEvent.OnGet_CommandSystem().RemoveSelected(this);
                    return;
                }
                else
                {
                    OnSelect();
                }
            }
        }
        public void OnPointerEnter(PointerEventData ped) => OnHighlight();
        public void OnPointerExit(PointerEventData ped) => OnDeHightlight();

        public void OnSelect()
        {
            selected = true;
            anchor.SetActive(true);
            mouseMode.mode = MouseMode.Mode.UNIT;
            //gameEvent.OnGet_CommandSystem().AddToSelection(this, transform.position);

            /*view = view != null ? view : gameEvent.OnGetShepherdUpgradeView();
            view.gameObject.SetActive(true);
            bool canUpgrade = bankAccount.CanUpgradeShepherd();

            view.SetData(OnUpgrade, canUpgrade, shepherdData);*/


            StartCoroutine(Mouse());
            IEnumerator Mouse()
            {
                while (selected)
                {
                    Unit_Rotate();
                    yield return null;
                }
            }
        }
        public void OnDeSelect()
        {
            selected = false;
            isExecutingCommand = false;
            anchor.SetActive(false);
            mouseMode.mode = MouseMode.Mode.CAMERA;
            //gameEvent.OnGet_CommandSystem().RemoveSelected(this);
            shepheredFov.gameObject.SetActive(false);
        }

        public void OnHighlight() => Unit_ShowFOV_Effect();

        public void OnDeHightlight()
        {
            if (!isDirty) { return; }
            shepheredFov.gameObject.SetActive(false);
        }



        public void Unit__Move_Command(Vector3 point)
        {
            if (!isExecutingCommand)
            {
                controller.Move(point, OnDeSelect);
            }
        }

        private void Unit_Rotate()
        {
            float h = Input.GetAxis("Horizontal");
            if (h != 0)
            {
                Vector3 rot = 160 * h * Time.deltaTime * Vector3.up;
                transform.Rotate(rot);
            }
        }

        private void Unit_ShowFOV_Effect()
        {
            if (!isDirty) { return; }
            shepheredFov.gameObject.SetActive(true);
            // shepheredFov.ShowFOV(m_weapon.fov, m_weapon.view_distance);
            shepheredFov.ShowFOV(baseWeapon.field_of_view, baseWeapon.view_distance);
        }
    }
}
