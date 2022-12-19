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
        StartCoroutine(SpawnItems());
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

    public void RemoveItem()
    {
        spawnedObjects.RemoveAt(0);
        Destroy(spawnedObjects[0].gameObject);
    }
}
