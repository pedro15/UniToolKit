using System.IO;
using JsonUtility = UnityEngine.JsonUtility;
using UniToolkit.Security;
using UniToolkit.Serialization.LitJSON;

namespace UniToolkit.Serialization
{
    #region Json

    /// <summary>
    /// Helper class for Json Serialization
    /// this method can serialize to many of unity data types.
    /// </summary>
    public static class JSONSerializer
    {
        public static string SerializeToJson(object obj, bool IsEncrypted = false, bool UseUnitySerializer = false)
        {
            if (IsEncrypted)
                return EncryptionUtility.EncryptString(UseUnitySerializer? JsonUtility.ToJson(obj) : JsonMapper.ToJson(obj));
            else
                return UseUnitySerializer? JsonUtility.ToJson(obj) : JsonMapper.ToJson(obj);
        }

        public static T DeserializeFromJson<T>(string Json, bool IsEncrypted = false, bool UseUnitySerializer = false)
        {
            if (IsEncrypted)
                return  UseUnitySerializer? JsonUtility.FromJson<T>(EncryptionUtility.DecryptString(Json)) : 
                    JsonMapper.ToObject<T>(EncryptionUtility.DecryptString(Json));
            else
                return UseUnitySerializer? JsonUtility.FromJson<T>(Json) : JsonMapper.ToObject<T>(Json);
        }

        public static void SerializeToFile(object obj, string FilePath, bool IsEncrypted = false)
        {
            if (File.Exists(FilePath)) File.Delete(FilePath);
            string _text = SerializeToJson(obj, IsEncrypted);
            UnityEngine.Debug.Log(_text);
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