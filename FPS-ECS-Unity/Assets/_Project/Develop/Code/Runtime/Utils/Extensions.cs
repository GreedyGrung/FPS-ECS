using UnityEngine;

namespace FpsEcs.Runtime.Utils
{
    public static class Extensions
    {
        public static T ToDeserizalized<T>(this string json) => JsonUtility.FromJson<T>(json);

        public static string ToJson(this object obj) => JsonUtility.ToJson(obj);
    }
}