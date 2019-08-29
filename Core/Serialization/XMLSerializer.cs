using System.IO;
using UniToolkit.Security;
using System.Xml.Serialization;

/// <summary>
/// Helper class for Xml Serialization
/// </summary>
public static class XMLSerializer
{

    public static void SerializeXML(object obj, string FilePath, bool IsEncrypted = false)
    {
        XmlSerializer ser = new XmlSerializer(obj.GetType());
        if (File.Exists(FilePath)) File.Delete(FilePath);

        MemoryStream mem = new MemoryStream();

        ser.Serialize(mem, obj);

        string txt = EncryptionUtility.ByteArrayToString(mem.ToArray());

        mem.Dispose();
        mem.Close();

        string finaltext = (IsEncrypted) ? EncryptionUtility.EncryptString(txt) : txt;

        byte[] data = EncryptionUtility.StringToByeArray(finaltext);

        mem = new MemoryStream(data);

        FileStream file = new FileStream(FilePath, FileMode.Create);

        mem.WriteTo(file);

        file.Close();
        mem.Dispose();
        mem.Close();
    }

    public static T DeserializeXML<T>(string FilePath, bool IsEncrypted = false)
    {
        if (File.Exists(FilePath))
        {

            byte[] Loadedbytes = File.ReadAllBytes(FilePath);

            string txt = (IsEncrypted) ? EncryptionUtility.DecryptString(EncryptionUtility.ByteArrayToString(Loadedbytes)) :
                EncryptionUtility.ByteArrayToString(Loadedbytes);

            byte[] FinalBytes = EncryptionUtility.StringToByeArray(txt);

            XmlSerializer ser = new XmlSerializer(typeof(T));

            MemoryStream mem = new MemoryStream(FinalBytes);

            T data = (T)ser.Deserialize(mem);
            mem.Dispose();
            mem.Close();
            return data;
        }
        else
        {
            return default(T);
        }
    }

}