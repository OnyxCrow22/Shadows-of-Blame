using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool pooling;

    private List<GameObject> pooledObjs = new List<GameObject>();
    private int maximumAmount = 75;

    [SerializeField] private GameObject[] NPCS;

    private void Awake()
    {
        if (pooling == null)
        {
            pooling = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maximumAmount; i++)
        {
            int RandomNPC = Random.Range(0, NPCS.Length);
            GameObject newObj = Instantiate(NPCS[RandomNPC]);
            newObj.SetActive(false);
            pooledObjs.Add(newObj);
        }
    }

    public GameObject GetPooledObjs()
    {
        for (int i = 0; i < pooledObjs.Count; i++)
        {
            if (!pooledObjs[i].activeInHierarchy)
            {
                return pooledObjs[i];
            }
        }

        return null;
    }
}
