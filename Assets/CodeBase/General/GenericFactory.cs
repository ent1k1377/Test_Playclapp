using UnityEngine;

namespace CodeBase.General
{
    public class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T _prefab;
        [SerializeField] private Transform _pointSpawn;
        [SerializeField] private int _poolSize = 10;
        
        private PoolMono<T> _pool;

        private void Start() => 
            _pool = new PoolMono<T>(_prefab, _poolSize, transform);

        public T GetNewInstance()
        {
            var go = _pool.GetFreeElement();
            go.transform.position = _pointSpawn.position;
            return go;
        }
    }
}