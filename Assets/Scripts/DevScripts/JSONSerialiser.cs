using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONSerialiser : ISerialiser
{
    public string Seralize<T>(T obj)
    {
        return JsonUtility.ToJson(obj, true);
    }

    public T DeSerialize<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }
}
