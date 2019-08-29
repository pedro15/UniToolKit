using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UniToolkit.Security;

namespace UniToolkit.Serialization
{
    /// <summary>
    /// Helper class for Binary Serialization
    /// </summary>
    public static class BinarySerializer
    {
        public static void Serialize(object obj, string FilePath, bool IsEncrypted = false)
        {
            BinaryFormatter bn = new BinaryFormatter();
            MemoryStream mem = new MemoryStream();
            bn.Serialize(mem, obj);
            byte[] plaindata = mem.ToArray();
            mem.Dispose();
            mem.Close();
            byte[] dataencrypted = (IsEncrypted) ? EncryptionUtility.EncryptByteArray(plaindata) :
                plaindata;
            if (File.Exists(FilePath)) File.Delete(FilePath);
            FileStream file = new FileStream(FilePath, FileMode.Create);
            bn.Serialize(file, dataencrypted);
            file.Close();
        }

        public static T Deserialize<T>(string FilePath, bool IsEncrypted = false)
        {
            if (File.Exists(FilePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = new FileStream(FilePath, FileMode.Open);
                byte[] encrypted = (byte[])formatter.Deserialize(file);
                file.Close();
                byte[] desencrypted = (IsEncrypted) ? EncryptionUtility.DecryptByteArray(encrypted) :
                    encrypted;
                MemoryStream DeserializationStream = new MemoryStream(desencrypted);
                T data = (T)formatter.Deserialize(DeserializationStream);
                DeserializationStream.Dispose();
                DeserializationStream.Close();
                return data;
            }
            else
            {
                return default(T);
            }
        }
    }
}