using UnityEngine;

namespace HT
{
    public abstract class NPC_Factory : MonoBehaviour
    {
        public abstract IamNPC CreaateNpc();
    }
}
