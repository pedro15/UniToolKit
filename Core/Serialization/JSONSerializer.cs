using System.IO;
using UniToolkit.Security;
using UniToolkit.Serialization.Json;

namespace UniToolkit.Serialization
{
    #region Json

    /// <summary>
    /// Helper class for Json Serialization
    /// this method can serialize to many of unity data types.
    /// </summary>
    public static class JSONSerializer
    {

        private static IJsonParser parser = new JsonUtilityParser();
        
        public static void OverrideParser(IJsonParser _parser)
        {
            if (_parser == null) throw new System.ArgumentNullException("JSONSerializer: Parser null");

            parser = _parser;
        }

        public static string SerializeToJson(object obj, bool IsEncrypted = false)
        {
            if (IsEncrypted)
                return EncryptionUtility.EncryptString(parser.ToJSON(obj));
            else
                return parser.ToJSON(obj);
        }

        public static T DeserializeFromJson<T>(string Json, bool IsEncrypted = false)
        {
            if (IsEncrypted)
                return parser.FromJSON<T>(EncryptionUtility.DecryptString(Json));
            else
                return parser.FromJSON<T>(Json);
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