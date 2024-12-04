using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
   

    public class IWeapon : ScriptableObject
    {
        protected GameObject weaponObject;
        protected Animator m_anim;
        protected float nextAllowedUse;
        protected float damagePoint;

        public GameObject weaponPrefab;
        public HAND handleWeaponGoal;
        public Vector3 localRot;
        public float scale;
        public float useRate;
        public string subMachine;

        [Range(1, 180)] public float fov;
        public float view_distance;



        public virtual void ActivateWeapon(Transform left_goal, Transform right_goal, Animator anim, float damage = 0)
        {
            weaponObject = Instantiate(weaponPrefab, handleWeaponGoal == HAND.LEFT ? left_goal : right_goal);
            weaponObject.transform.position = handleWeaponGoal == HAND.LEFT ? left_goal.position : right_goal.position;
            weaponObject.transform.localRotation = Quaternion.Euler(localRot);
            weaponObject.transform.localScale = new Vector3(scale, scale, scale);
            weaponObject.SetActive(true);
            m_anim = anim;
            nextAllowedUse = 0;
            damagePoint = damage;
        }

        public virtual void DeactivateWeapon() { }
        public virtual void Use(Transform self) { }
    }

}