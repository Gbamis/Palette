using UnityEngine;

namespace HT
{
    public class Factory_PenOwner : NPC_Factory
    {
        public Transform point;
        public NPC_PenOwner prefab;

        public override IamNPC CreaateNpc()
        {
            NPC_PenOwner pen = Instantiate(prefab);
            pen.gameObject.SetActive(true);
            IamNPC npc = pen.GetComponent<IamNPC>();
            npc.OnSpanwed(point.position, point.rotation);
            return npc;
        }
    }
}
