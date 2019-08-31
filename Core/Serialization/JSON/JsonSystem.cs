namespace UniToolkit.Serialization.Json
{
    public interface IJsonParser
    {
        string ToJSON(object obj);

        T FromJSON<T>(string json);
    }
}
