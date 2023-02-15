using System.Collections.Generic;
using System.Linq;
using CaseSDK.PoolScriptableObjects;
using UnityEngine;

namespace CaseSDK
{
    public class PoolManager : Singleton<PoolManager>
    {
        [SerializeField] private PoolListScriptableObject pools;
        private readonly List<Queue<GameObject>> _queues = new List<Queue<GameObject>>();
        private PoolScriptableObject GetPool<T>() => pools.list.Find(o => o.poolPrefab.GetComponent<T>() != null);
        private Queue<GameObject> GetQueue<T>() => _queues.Find(queue => queue.Any(o => o.GetComponent<T>() != null));
        public GameObject Dequeue<T>() => GetPool<T>().Dequeue(GetQueue<T>());
        public void Enqueue<T>(GameObject obj) => GetPool<T>().Enqueue(obj, GetQueue<T>(), gameObject.transform);

        private void Awake()
        {
            for (var i = 0; i < pools.list.Count; i++)
            {
                _queues.Add(new Queue<GameObject>());
                pools.list[i].CreatePool(_queues[i], gameObject.transform);
            }
        }
    }
}