using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public class Factory_Merchant : NPC_Factory
    {
        public Transform point;
        public NPC_Merchant prefab;
        public override IamNPC CreaateNpc()
        {
            NPC_Merchant merchant = Instantiate(prefab);
            merchant.gameObject.SetActive(true);
            IamNPC npc = merchant.GetComponent<IamNPC>();
            npc.OnSpanwed(point.position, point.rotation);
            return npc;
        }
    }
}
