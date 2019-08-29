using UnityEngine;
using UniToolkit.Serialization;

namespace UniToolkit.Security
{
    /// <summary>
    /// Secured Unity's Playerprefs System
    /// Note: SafePlayerPrefs Uses Encryption for most of cases, so take in mind that it can be slow on Loading / Saving data.
    /// </summary>
    public static class SafePlayerprefs
    {
        public static void SetString(string key , string value)
        {
            PlayerPrefs.SetString(EncryptionUtility.EncryptString(key) , new SafeString(value));
        }

        public static string GetString(string key , string DefaultValue = "")
        {
            return PlayerPrefs.GetString(EncryptionUtility.EncryptString(key), DefaultValue);
        }

        public static void SetFloat(string key , float value)
        {
            PlayerPrefs.SetFloat(EncryptionUtility.EncryptString(key), new SafeFloat(value));
        }

        public static float GetFloat(string key , float DefaultValue = 0.0f)
        {
            return PlayerPrefs.GetFloat(EncryptionUtility.EncryptString(key), DefaultValue);
        }
        
        public static void SetInt(string key , int Value)
        {
            PlayerPrefs.SetInt(EncryptionUtility.EncryptString(key), new SafeInt(Value));
        }

        public static int GetInt(string key , int DefaultValue = 0)
        {
            return PlayerPrefs.GetInt(EncryptionUtility.EncryptString(key), DefaultValue);
        }
        
        public static void SetVector2(string key , Vector2 Value)
        {
            string json = JSONSerializer.SerializeToJson(Value, true);
            PlayerPrefs.SetString(EncryptionUtility.EncryptString(key), json);
        }

        public static Vector2 GetVector2(string key)
        {
            return GetVector3(key);
        }

        public static void SetVector3(string key , Vector3 Value)
        {
            string json = JSONSerializer.SerializeToJson(Value , false);
            PlayerPrefs.SetString(EncryptionUtility.EncryptString(key), json);
        }

        public static Vector3 GetVector3(string key)
        {
            string k = EncryptionUtility.EncryptString(key);
            
            if (PlayerPrefs.HasKey(k))
            {
                string json = PlayerPrefs.GetString(k);

                return JSONSerializer.DeserializeFromJson<Vector3>(json, false);
            }
            return Vector3.zero;
        }

        public static void SetColor(string key , Color col)
        {
            string json = JSONSerializer.SerializeToJson(col, true);
            PlayerPrefs.SetString(EncryptionUtility.EncryptString(key), json);
        }

        public static Color GetColor(string key )
        {
            string k = EncryptionUtility.EncryptString(key);
            if (PlayerPrefs.HasKey(k))
            {
                string json = PlayerPrefs.GetString(k , string.Empty);
                return JSONSerializer.DeserializeFromJson<Color>(json, true);
            }
            return Color.clear;
        }

        public static Quaternion GetQuaternion(string Key)
        {
            string k = EncryptionUtility.EncryptString(Key);
            if (PlayerPrefs.HasKey(k))
            {
                string json = PlayerPrefs.GetString(k, string.Empty);
                return Quaternion.Euler(JSONSerializer.DeserializeFromJson<Vector3>(json, true));
            }
            return Quaternion.identity;
        }

        public static void SetQuaternion(string Key , Quaternion value)
        {
            SetVector3(Key, value.eulerAngles);
        }

        //---

        public static bool HasKey (string key)
        {
            return PlayerPrefs.HasKey(EncryptionUtility.EncryptString(key));
        }

        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        public static void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(EncryptionUtility.EncryptString(key));
        }

        public static void Save()
        {
            PlayerPrefs.Save();
        }
    }
}
