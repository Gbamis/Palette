using UnityEngine;

namespace HT
{
    public class Factory_LumberJack : NPC_Factory
    {
        public Transform point;
        public NPC_Lumberjack prefab;

        public override IamNPC CreaateNpc()
        {
            NPC_Lumberjack pen = Instantiate(prefab);
            pen.gameObject.SetActive(true);
            IamNPC npc = pen.GetComponent<IamNPC>();
            npc.OnSpanwed(point.position, point.rotation);
            return npc;
        }
    }
}
