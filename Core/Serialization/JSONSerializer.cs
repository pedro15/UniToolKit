using System.IO;
using JsonUtility = UnityEngine.JsonUtility;
using UniToolkit.Security;

namespace UniToolkit.Serialization
{
    #region Json

    /// <summary>
    /// Helper class for Json Serialization
    /// this method can serialize to many of unity data types.
    /// </summary>
    public static class JSONSerializer
    {
        public static IJSONProvider Customprovider = null;

        public static string SerializeToJson(object obj, bool IsEncrypted = false)
        {
            if (IsEncrypted)
                return EncryptionUtility.EncryptString(Customprovider == null? JsonUtility.ToJson(obj) : Customprovider.ToJSON(obj));
            else
                return Customprovider == null? JsonUtility.ToJson(obj) : Customprovider.ToJSON(obj);
        }

        public static T DeserializeFromJson<T>(string Json, bool IsEncrypted = false)
        {
            if (IsEncrypted)
                return  Customprovider == null? JsonUtility.FromJson<T>(EncryptionUtility.DecryptString(Json)) : 
                    Customprovider.ToObject<T>(EncryptionUtility.DecryptString(Json));
            else
                return Customprovider == null? JsonUtility.FromJson<T>(Json) : Customprovider.ToObject<T>(Json);
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