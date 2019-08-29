using System.Text;
using System.Security.Cryptography;
using System.IO;
using System;

namespace UniToolkit.Security
{
    public static class EncryptionUtility
    {
        // You can reemplace this keys with your own keys...

        private const string PasswordHash = "i0.1LT.h!3=tyRE@OhPRtX7";
        private const string SaltKey = "3//hN[JX{RJqM!w]8LQ0xKvY";
        private const string VIKey = "2e6c612b66td74qu";

        #region API 

        /// <summary>
        /// Encrypt a string text
        /// </summary>
        /// <param name="plainText">String to encrypt</param>
        /// <returns>Encrypted string</returns>
        public static string EncryptString(string plainText)
        {
            if (!string.IsNullOrEmpty(plainText))
            {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedbytes = EncryptByteArray(plainTextBytes);
                return Convert.ToBase64String(encryptedbytes);
            }else
            {
                return plainText;
            }
        }
        
        /// <summary>
        /// Decrypt a string
        /// </summary>
        /// <param name="encryptedText">String to decrypt</param>
        /// <returns></returns>
        public static string DecryptString(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            int count = 0;
            byte[] Decryptedbytes = DecryptByteArray(cipherTextBytes,out count);
            return Encoding.UTF8.GetString(Decryptedbytes, 0, count).TrimEnd("\0".ToCharArray());
        }

        public static byte[] EncryptByteArray(byte[] data)
        {
            byte[] cipherTextBytes = data;
            if (data.Length > 0 )
            {
                byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
                var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(data, 0, data.Length);
                        cryptoStream.FlushFinalBlock();
                        cipherTextBytes = memoryStream.ToArray();
                        cryptoStream.Close();
                    }
                    memoryStream.Close();
                }
            }
            return cipherTextBytes;
        }
        
        public static byte[] DecryptByteArray(byte[] dataencrypted)
        {
            if (dataencrypted.Length > 0)
            {
                byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };
                var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
                var memoryStream = new MemoryStream(dataencrypted);
                var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[dataencrypted.Length];
                cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return plainTextBytes;
            }
            else
            {
                return dataencrypted;
            }
        }

        public static byte[] DecryptByteArray(byte[] dataencrypted , out int descryptedbytecount)
        {
            if (dataencrypted.Length > 0)
            {
                byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };
                var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
                var memoryStream = new MemoryStream(dataencrypted);
                var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[dataencrypted.Length];
                descryptedbytecount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return plainTextBytes;
            }
            else
            {
                descryptedbytecount = 0;
                return dataencrypted;
            }
        }

        public static byte[] StringToByeArray(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static string ByteArrayToString(byte[] data)
        {
            return Encoding.ASCII.GetString(data);
        }

        #endregion
    }
}