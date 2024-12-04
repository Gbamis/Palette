using UnityEngine;

namespace HT
{
    public class NPC_Merchant : NPC_Base, IamNPC
    {
        public void OnSpanwed(Vector3 pos, Quaternion rot)
        {
            transform.SetPositionAndRotation(pos, rot);
            PositionIcon();
        }

    }
}
