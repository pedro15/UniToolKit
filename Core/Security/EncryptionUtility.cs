using System.Text;
using System.Security.Cryptography;
using System;

namespace UniToolkit.Security
{
    public static class EncryptionUtility
    {
        private static int IV_LENGTH = 16;

        private static string PASSWORD = "xrt2363.Q";

        public static void Init(string Password)
        {
            PASSWORD = Password;
        }

        private static byte[] GetSalt(string password)
        {
            using (var derivedBytes = new Rfc2898DeriveBytes(password, 16, 50000))
            {
                return derivedBytes.GetBytes(16);
            }
        }

        private static byte[] GetIV(byte[] data)
        {
            byte[] IV = new byte[IV_LENGTH];

            for (int i = 0; i < IV_LENGTH; i++)
            {
                IV[i] = data[i];
            }

            return IV;

        }

        private static byte[] GenerateIV()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] nonce = new byte[IV_LENGTH];
                rng.GetBytes(nonce);
                return nonce;
            }
        }

        private static byte[] HashPassword(string pw)
        {
            var md5 = MD5.Create();
            return md5.ComputeHash(Encoding.ASCII.GetBytes(pw));
        }

        public static byte[] EncryptByteArray(byte[] data, string password)
        {
            if (data != null && data.Length > 0)
            {
                byte[] originalBytes;
                byte[] BytesAndIV;
                using (var aes = Aes.Create())
                {
                    aes.KeySize = 256;
                    aes.Mode = CipherMode.CBC;

                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, HashPassword(password));

                    aes.Key = pdb.GetBytes(aes.KeySize / 8);
                    aes.IV = GenerateIV();

                    using (var encryptor = aes.CreateEncryptor())
                    {
                        originalBytes = encryptor.TransformFinalBlock(data, 0, data.Length);
                        BytesAndIV = new byte[originalBytes.Length + IV_LENGTH];
                        aes.IV.CopyTo(BytesAndIV, 0);
                        originalBytes.CopyTo(BytesAndIV, IV_LENGTH);
                        return BytesAndIV;
                    }

                }
            }
            throw new ArgumentNullException("EncryptionUtility: Data null");
        }

        public static byte[] EncryptByteArray(byte[] data)
        {
            return EncryptByteArray(data, PASSWORD);
        }

        private static byte[] DecryptByteArray(byte[] dataencrypted, string password)
        {
            if (dataencrypted != null && dataencrypted.Length > 0)
            {
                if (dataencrypted.Length > 0)
                {
                    using (var aes = Aes.Create())
                    {
                        aes.KeySize = 256;
                        aes.Mode = CipherMode.CBC;



                        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, HashPassword(password));

                        aes.Key = pdb.GetBytes(aes.KeySize / 8);
                        aes.IV = GetIV(dataencrypted);

                        byte[] dataclean = new byte[dataencrypted.Length - IV_LENGTH];

                        Array.Copy(dataencrypted, IV_LENGTH, dataclean, 0, dataencrypted.Length - IV_LENGTH);

                        using (var decryptor = aes.CreateDecryptor())
                        {
                            return decryptor.TransformFinalBlock(dataclean, 0, dataclean.Length);
                        }

                    }
                }
            }
            throw new ArgumentNullException("EncryptionUtility: DataEncrypted null");
        }

        public static byte[] DecryptByteArray(byte[] dataencrypted)
        {
            return DecryptByteArray(dataencrypted, PASSWORD);
        }

        public static byte[] StringToByeArray(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static string ByteArrayToString(byte[] data)
        {
            return Encoding.ASCII.GetString(data);
        }

        /// <summary>
        /// Encrypt a string text
        /// </summary>
        /// <param name="plainText">String to encrypt</param>
        /// <returns>Encrypted string</returns>
        public static string EncryptString(string plainText, string password)
        {
            if (!string.IsNullOrEmpty(plainText))
            {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedbytes = EncryptByteArray(plainTextBytes, password);
                return Convert.ToBase64String(encryptedbytes);
            }
            else
            {
                return plainText;
            }
        }

        public static string EncryptString(string plainText)
        {
            return EncryptString(plainText, PASSWORD);
        }

        /// <summary>
        /// Decrypt a string
        /// </summary>
        /// <param name="encryptedText">String to decrypt</param>
        /// <returns></returns>
        public static string DecryptString(string encryptedText, string password)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] Decryptedbytes = DecryptByteArray(cipherTextBytes, password);
            return Encoding.UTF8.GetString(Decryptedbytes, 0, Decryptedbytes.Length).TrimEnd("\0".ToCharArray());
        }

        public static string DecryptString(string encryptedText)
        {
            return DecryptString(encryptedText, PASSWORD);
        }

    }
}