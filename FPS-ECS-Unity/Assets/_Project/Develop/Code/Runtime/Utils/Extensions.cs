using FpsEcs.Runtime.Gameplay.Common;
using FpsEcs.Runtime.Gameplay.Common.Modules;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Utils
{
    public static class Extensions
    {
        public static int First(this EcsFilter filter) => filter.GetRawEntities()[0];
        
        public static T ToDeserizalized<T>(this string json) => JsonUtility.FromJson<T>(json);

        public static string ToJson(this object obj) => JsonUtility.ToJson(obj);
        
        public static IEcsSystems AddModule(this IEcsSystems systems, IEcsModule module)
        {
            var builder = new EcsModuleBuilder(systems);
            module.Register(builder);

            return systems;
        }
        
    }
}