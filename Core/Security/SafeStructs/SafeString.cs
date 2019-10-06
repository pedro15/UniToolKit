using System;
using UniToolkit.Serialization.LitJSON;

namespace UniToolkit.Security
{
    [Serializable]
    public struct SafeString
    {
        [UnityEngine.SerializeField, UnityEngine.HideInInspector, JsonInclude]
        private string str;
        [UnityEngine.SerializeField, UnityEngine.HideInInspector, JsonInclude]
        private int offset;

        Random rand;

        public SafeString(string value)
        {
            str = string.Empty;
            rand = new Random();
            offset = rand.Next(-1000, 1000);
            str = SecureText(value, offset);
        }

        private void Dispose()
        {
            str = string.Empty;
        }

        private string GetValue()
        {
            return UnSecureText(str, offset);
        }

        public static string SecureText(string str, int offset)
        {
            string secstring = "";
            for (int i = 0; i < str.Length; i++)
            {
                secstring += (char)(str[i] + offset);
            }
            return secstring;
        }

        public static string UnSecureText(string str, int offset)
        {
            string unsec = "";
            for (int i = 0; i < str.Length; i++)
            {
                unsec += (char)(str[i] - offset);
            }
            return unsec;
        }

        public override string ToString()
        {
            return GetValue();
        }


        public static implicit operator SafeString(string s)
        {
            return new SafeString(s);
        }

        public static implicit operator string(SafeString ss)
        {
            return ss.GetValue();
        }
    }
}