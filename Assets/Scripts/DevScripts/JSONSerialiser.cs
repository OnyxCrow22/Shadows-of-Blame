using UnityEngine;

public class JSONSerialiser : ISerialiser
{
    public string Seralize<T>(T obj)
    {
        return JsonUtility.ToJson(obj, true);
    }

    public T Deserialize<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }
}
