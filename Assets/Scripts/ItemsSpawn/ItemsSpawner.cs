using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemsSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();
    public List<Item> spawnedObjects = new List<Item>();
    
    [field: SerializeField] public Item itemPrefab { get; private set; }

    private void Start()
    {
        StartSpawnItems();
    }

    private IEnumerator SpawnItems()
    {
        var clone = Instantiate(itemPrefab, spawnPoints[spawnedObjects.Count].position, Quaternion.identity,
            gameObject.transform);
        spawnedObjects.Add(clone);
        yield return new WaitForSeconds(1f);
        if (spawnedObjects.Count != spawnPoints.Count)
        {
            yield return  SpawnItems();
        }
        else
        {
            Debug.Log("no space");
        }
    }

    public void StartSpawnItems()
    {
        StartCoroutine(SpawnItems());
    }

    public void RemoveItem()
    {
        if (spawnedObjects.Count != 0)
        {
            Destroy(spawnedObjects[spawnedObjects.Count - 1].gameObject);
            spawnedObjects.RemoveAt(spawnedObjects.Count - 1);
        }
        else
        {
            return;
        }
    }
}
