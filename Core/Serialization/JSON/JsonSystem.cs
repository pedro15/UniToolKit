using UnityEngine;

namespace UniToolkit.Serialization.Json
{
    public static class JsonSystem
    {
        public static string ToJSON(object obj)
        {
            return JsonUtility.ToJson(obj);
        }

        public static T FromJSON<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }
    }
}