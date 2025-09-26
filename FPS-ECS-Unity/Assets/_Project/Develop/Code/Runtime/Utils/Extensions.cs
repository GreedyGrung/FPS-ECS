using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Utils
{
    public static class Extensions
    {
        public static int First(this EcsFilter filter) => filter.GetRawEntities()[0];
        public static T ToDeserizalized<T>(this string json) => JsonUtility.FromJson<T>(json);

        public static string ToJson(this object obj) => JsonUtility.ToJson(obj);
        
    }
}