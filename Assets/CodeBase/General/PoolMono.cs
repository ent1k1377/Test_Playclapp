using System.Collections.Generic;
using System.Linq;
using CodeBase.Extensions;
using UnityEngine;

namespace CodeBase.General
{
    public class PoolMono<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly T[] _prefabs;
        private readonly Transform _container;
        private List<T> _pools;

        public bool AutoExpand { get; set; } = true;

        public PoolMono(T prefab, int count, Transform container)
        {
            _prefab = prefab;
            _container = container;

            CreatePoolIdenticalObject(count);
        }

        public PoolMono(T[] prefabs, int count, Transform container)
        {
            _prefabs = prefabs;
            _container = container;

            CreatePoolDifferentObject(count);
        }

        private void CreatePoolDifferentObject(int count)
        {
            _pools = new List<T>();

            foreach (var p in _prefabs)
                for (var j = 0; j < count; j++)
                    CreateObject(p);
        }

        private void CreatePoolIdenticalObject(int count)
        {
            _pools = new List<T>();

            for (var i = 0; i < count; i++)
                CreateObject();
        }

        private T CreateObject(T prefab, bool isActiveByDefault = false)
        {
            var createdObject = Object.Instantiate(prefab, _container);
            createdObject.gameObject.SetActive(isActiveByDefault);
            _pools.Add(createdObject);
            return createdObject;
        }

        private T CreateObject(bool isActiveByDefault = false)
        {
            var createdObject = Object.Instantiate(_prefab, _container);
            createdObject.gameObject.SetActive(isActiveByDefault);
            _pools.Add(createdObject);
            return createdObject;
        }

        private bool HasFreeElement(out T element)
        {
            foreach (var mono in _pools.Where(mono => !mono.gameObject.activeInHierarchy))
            {
                element = mono;
                mono.gameObject.Activate();
                return true;
            }
            element = null;
            return false;
        }

        public T GetFreeElement()
        {
            if (HasFreeElement(out var element))
                return element;

            if (AutoExpand && _prefab != null)
                return CreateObject(true);
            
            return CreateObject(_prefabs[Random.Range(0, _prefabs.Length)]);
        }
    }
}