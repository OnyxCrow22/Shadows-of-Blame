using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONSerialiser : ISerialiser
{
    public string Seralize<T>(T obj)
    {
        return JsonUtility.ToJson(obj, true);
    }

    public T DeSerialize<T>(string jSON)
    {
        return JsonUtility.FromJson<T>(jSON);
    }
}
