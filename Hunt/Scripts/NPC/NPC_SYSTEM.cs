using UnityEngine;

namespace HT
{
    public class NPC_SYSTEM : MonoBehaviour
    {
        public NPC_Factory merchant_Factory;
        public NPC_Factory pen_owner_Factory;
        public NPC_Factory lumberJack_factory;

        private void Start()
        {
            merchant_Factory.CreaateNpc();
            pen_owner_Factory.CreaateNpc();
            lumberJack_factory.CreaateNpc();
        }
    }
}
