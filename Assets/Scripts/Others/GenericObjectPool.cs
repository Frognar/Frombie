using System.Collections.Generic;
using UnityEngine;

namespace Frogi {
    public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component {
        private static GenericObjectPool<T> _instance;

        public static GenericObjectPool<T> Instance {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<GenericObjectPool<T>>();

                    if (_instance == null)
                        _instance = new GameObject(typeof(GenericObjectPool<T>).Name).AddComponent<GenericObjectPool<T>>();
                }

                return _instance;
            }
        }
        [SerializeField] protected T _prefab;
        
        protected readonly Queue<T> _objects = new Queue<T>();
        public bool PoolIsEmpty => _objects.Count == 0;

        protected virtual void Awake() {
            if(_instance != null) Destroy(gameObject);
        }

        public virtual T Get() {
            if (PoolIsEmpty) CreateNewObjectsInPool(50);

            return _objects.Dequeue();
        }

        public void ReturnToPool(T objectToReturn) {
            objectToReturn.gameObject.SetActive(false);
            _objects.Enqueue(objectToReturn);
        }

        protected void CreateNewObjectsInPool(uint count) {
            for (var i = 0; i < count; i++) CreateAndSetUpNewObject();
        }

        protected virtual void CreateAndSetUpNewObject() {
            var newObject = Instantiate(_prefab, transform, true);
            newObject.gameObject.SetActive(false);
            _objects.Enqueue(newObject);
        }
    }
}