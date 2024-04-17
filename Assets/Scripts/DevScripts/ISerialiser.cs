public interface ISerialiser
{
    string Seralize<T>(T obj);
    T Deserialize<T>(string json);
}
