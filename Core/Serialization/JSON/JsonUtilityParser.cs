using UnityEngine;

namespace UniToolkit.Serialization.Json
{
    public class JsonUtilityParser : IJsonParser
    {
        public T FromJSON<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public string ToJSON(object obj)
        {
            return JsonUtility.ToJson(obj);
        }
    }
}
