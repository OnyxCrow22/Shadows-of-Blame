using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISerialiser
{
    string Seralize<T>(T obj);
    T DeSerialize<T>(string jSON);
}
