using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace HT
{
    public class PoolItem : MonoBehaviour
    {
        public IObjectPool<GameObject> pool;
        private void OnDisable() => pool.Release(gameObject);

    }

    public class PoolSystem : MonoBehaviour
    {
        [Inject] GameplayEvent gameplayEvent;
        
        public GameObject prefab;
        public int poolSize;
        private IObjectPool<GameObject> m_pool;
        public IObjectPool<GameObject> Pool
        {
            get
            {
                m_pool ??= new ObjectPool<GameObject>(CreateItem, OnTake, OnReturn, OnDestory, true, 10, poolSize);
                return m_pool;
            }
        }

        private GameObject CreateItem()
        {
            GameObject clone = Instantiate(prefab,transform);
            clone.AddComponent<PoolItem>().pool = Pool;
            return clone;
        }
        private void OnTake(GameObject obj) => obj.SetActive(true);
        private void OnReturn(GameObject obj) => obj.SetActive(false);
        private void OnDestory(GameObject obj) => Destroy(obj);

        private void Start()
        {
            gameplayEvent.OnGetObjectFromPool += () => Pool.Get();
        }

    }

}