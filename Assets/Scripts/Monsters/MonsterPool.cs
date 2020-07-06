using UnityEngine;

namespace Frogi {
    public class MonsterPool : GenericObjectPool<Monster> {
        [SerializeField] private uint _monsters = 50;
        [SerializeField] private bool _canExpand;

        protected override void Awake() {
            base.Awake();
            CreateNewObjectsInPool(_monsters);
        }

        public override Monster Get() {
            if (PoolIsEmpty) {
                if (_canExpand)
                    CreateNewObjectsInPool(1);
                else
                    return default;
            }

            return _objects.Dequeue();
        }
    }
}
