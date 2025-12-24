using LeoEcsLite.QoL.Modules;
using Leopotam.EcsLite;

namespace LeoEcsLite.QoL.Utils
{
    public static partial class EcsUtils
    {
        private static EcsWorld _world;
        
        public static void Initialize(EcsWorld world) => _world = world;
        
        public static IEcsSystems AddModule(this IEcsSystems systems, IEcsModule module)
        {
            var builder = new EcsModuleBuilder(systems);
            module.Register(builder);

            return systems;
        }

        public static ref T Add<T>(this int entity) where T : struct => ref _world.GetPool<T>().Add(entity);
        
        public static ref T Add<T>(this int entity, EcsWorld world) where T : struct => ref world.GetPool<T>().Add(entity);

        public static ref T Get<T>(this int entity) where T : struct => ref _world.GetPool<T>().Get(entity);
        
        public static ref T Get<T>(this int entity, EcsWorld world) where T : struct => ref world.GetPool<T>().Get(entity);

        public static ref T GetOrAdd<T>(this int entity) where T : struct
        {
            if (entity.Has<T>())
            {
                return ref entity.Get<T>();
            }

            return ref entity.Add<T>();
        }
        
        public static ref T GetOrAdd<T>(this int entity, EcsWorld world) where T : struct
        {
            if (entity.Has<T>(world))
            {
                return ref entity.Get<T>(world);
            }

            return ref entity.Add<T>(world);
        }

        public static bool Has<T>(this int entity) where T : struct => _world.GetPool<T>().Has(entity);
        
        public static bool Has<T>(this int entity, EcsWorld world) where T : struct => world.GetPool<T>().Has(entity);

        public static void Del<T>(this int entity) where T : struct => _world.GetPool<T>().Del(entity);
        
        public static void Del<T>(this int entity, EcsWorld world) where T : struct => world.GetPool<T>().Del(entity);

        public static void TryDel<T>(this int entity) where T : struct
        {
            if (entity.Has<T>())
            {
                entity.Del<T>();
            }
        }
        
        public static void TryDel<T>(this int entity, EcsWorld world) where T : struct
        {
            if (entity.Has<T>(world))
            {
                entity.Del<T>(world);
            }
        }

        public static EcsPackedEntity Pack(this int entity) => _world.PackEntity(entity);
        
        public static EcsPackedEntity Pack(this int entity, EcsWorld world) => world.PackEntity(entity);

        public static int Unpack(this EcsPackedEntity packedEntity)
        {
            packedEntity.Unpack(_world, out var entity);

            return entity;
        }
        
        public static int Unpack(this EcsPackedEntity packedEntity, EcsWorld world)
        {
            packedEntity.Unpack(world, out var entity);

            return entity;
        }

        public static void Dispose() => _world = null;
    }
}