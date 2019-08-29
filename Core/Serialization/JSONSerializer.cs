using System.IO;
using UniToolkit.Security;
using UnityEngine;

namespace UniToolkit.Serialization
{
    #region Json

    /// <summary>
    /// Helper class for Json Serialization
    /// this method can serialize to many of unity data types.
    /// </summary>
    public static class JSONSerializer
    {
        public static string SerializeToJson(object obj, bool IsEncrypted = false)
        {
            if (IsEncrypted)
                return EncryptionUtility.EncryptString(JsonUtility.ToJson(obj));
            else
                return JsonUtility.ToJson(obj);
        }

        public static T DeserializeFromJson<T>(string Json, bool IsEncrypted = false)
        {
            if (IsEncrypted)
                return JsonUtility.FromJson<T>(EncryptionUtility.DecryptString(Json));
            else
                return JsonUtility.FromJson<T>(Json);
        }

        public static void SerializeToFile(object obj, string FilePath, bool IsEncrypted = false)
        {
            if (File.Exists(FilePath)) File.Delete(FilePath);
            string _text = SerializeToJson(obj, IsEncrypted);
            File.WriteAllText(FilePath, _text);
        }

        public static T DeserializeFromFile<T>(string Filepath, bool IsEncrypted = false)
        {
            if (File.Exists(Filepath))
            {
                string _json = File.ReadAllText(Filepath);
                return DeserializeFromJson<T>(_json, IsEncrypted);
            }
            else
            {
                return default(T);
            }
        }
    }
    #endregion
}