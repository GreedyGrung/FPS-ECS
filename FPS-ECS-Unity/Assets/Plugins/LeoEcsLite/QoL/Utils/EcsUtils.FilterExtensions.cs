using Leopotam.EcsLite;

namespace LeoEcsLite.QoL.Utils
{
    public static partial class EcsUtils
    {
        public static EcsWorld.Mask Inc<T1>(this EcsWorld world) 
            where T1 : struct => 
            world.Filter<T1>();

        public static EcsWorld.Mask Inc<T1, T2>(this EcsWorld world) 
            where T1 : struct 
            where T2 : struct =>
            world.Filter<T1>().Inc<T2>();

        public static EcsWorld.Mask Inc<T1, T2, T3>(this EcsWorld world) 
            where T1 : struct
            where T2 : struct
            where T3 : struct =>
            world.Filter<T1>().Inc<T2>().Inc<T3>();

        public static EcsWorld.Mask Inc<T1, T2, T3, T4>(this EcsWorld world) 
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct =>
            world.Filter<T1>().Inc<T2>().Inc<T3>().Inc<T4>();

        public static EcsWorld.Mask Inc<T1, T2, T3, T4, T5>(this EcsWorld world) 
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct =>
            world.Filter<T1>().Inc<T2>().Inc<T3>().Inc<T4>().Inc<T5>();
        
        public static EcsWorld.Mask Inc<T1, T2, T3, T4, T5, T6>(this EcsWorld world) 
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct =>
            world.Filter<T1>().Inc<T2>().Inc<T3>().Inc<T4>().Inc<T5>().Inc<T6>();
        
        public static EcsWorld.Mask Inc<T1, T2, T3, T4, T5, T6, T7>(this EcsWorld world) 
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
            where T7 : struct =>
            world.Filter<T1>().Inc<T2>().Inc<T3>().Inc<T4>().Inc<T5>().Inc<T6>().Inc<T7>();

        public static EcsWorld.Mask Exc<T1, T2>(this EcsWorld.Mask mask)
            where T1 : struct
            where T2 : struct =>
            mask.Exc<T1>().Exc<T2>();

        public static EcsWorld.Mask Exc<T1, T2, T3>(this EcsWorld.Mask mask)
            where T1 : struct
            where T2 : struct
            where T3 : struct =>
            mask.Exc<T1>().Exc<T2>().Exc<T3>();

        public static EcsWorld.Mask Exc<T1, T2, T3, T4>(this EcsWorld.Mask mask)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct =>
            mask.Exc<T1>().Exc<T2>().Exc<T3>().Exc<T4>();

        public static EcsWorld.Mask Exc<T1, T2, T3, T4, T5>(this EcsWorld.Mask mask)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct =>
            mask.Exc<T1>().Exc<T2>().Exc<T3>().Exc<T4>().Exc<T5>();

        public static EcsWorld.Mask Exc<T1, T2, T3, T4, T5, T6>(this EcsWorld.Mask mask)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct =>
            mask.Exc<T1>().Exc<T2>().Exc<T3>().Exc<T4>().Exc<T5>().Exc<T6>();

        public static EcsWorld.Mask Exc<T1, T2, T3, T4, T5, T6, T7>(this EcsWorld.Mask mask)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
            where T7 : struct =>
            mask.Exc<T1>().Exc<T2>().Exc<T3>().Exc<T4>().Exc<T5>().Exc<T6>().Exc<T7>();
        
        public static int First(this EcsFilter filter) => filter.GetRawEntities()[0];
    }
}