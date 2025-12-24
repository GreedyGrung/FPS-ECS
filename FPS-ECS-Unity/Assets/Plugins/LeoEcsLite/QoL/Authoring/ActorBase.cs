using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace LeoEcsLite.QoL.Authoring
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public class ActorBase : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsPackedEntity _packedEntity;
        
        private bool _isInitialized;

        public void Initialize(EcsWorld world)
        {
            if (_isInitialized) return;
            
            _world = world;
            var newEntity = _world.NewEntity();
            
            AddDefaultComponents(world, newEntity);
            ApplyAuthorings(newEntity);
            
            _packedEntity = newEntity.Pack();
            _isInitialized = true;
        }

        public int GetEntity() => _packedEntity.Unpack();

        protected virtual void AddDefaultComponents(EcsWorld world, int entity) { }
        
        private void ApplyAuthorings(int entity)
        {
            foreach (var a in GetComponents<MonoBehaviour>())
            {
                if (a is IAuthoring auth && a.isActiveAndEnabled)
                {
                    auth.Convert(_world, entity);
                }
            }
        }
    }
}