using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class SellPoint : MonoBehaviour
{
    public List<Transform> placePoints = new List<Transform>();
    public List<Item> placedItems = new List<Item>();
    
    [SerializeField] private int maxItemsCount;
    [SerializeField] private TextMeshProUGUI countText;

    private Vector3 _spawnPoint;

    [SerializeField] private float timeToPlaceItem;
    
    public List<Item> playerItems = new List<Item>();


    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory _playerInventory;
        if (other.TryGetComponent<PlayerInventory>(out _playerInventory))
        {
            _spawnPoint = _playerInventory.transform.position;
            playerItems = _playerInventory.playerInventory;
            PlaceItems();
            _playerInventory.AllowPlayerPlaceItems();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerInventory _playerInventory;
        if (other.TryGetComponent<PlayerInventory>(out _playerInventory))
        {
            playerItems.Clear();
            _playerInventory.ForbidPlayerPlaceItems();
            SellPlacedItems();
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        countText.text = $"{placedItems.Count}/{maxItemsCount}";
    }

    private void SellPlacedItems()
    {
        int sum = 0;
        for (int i = 0; i < placedItems.Count; i++)
        {
            sum += placedItems[i].ItemCost;
            placedItems[i].gameObject.transform.DOScale(0, 0.25f).From(1).SetEase(Ease.Linear).OnComplete(delegate
            {
                Destroy(placedItems[i].gameObject);
            });
        }
        placedItems.Clear();
        PlayerMoney.Instance.AddMoney(sum);
    }

    private void PlaceItems()
    {
        for (int i = 0; i < playerItems.Count; i++)
        {
            if (placedItems.Count != placePoints.Count)
            {
                var clone = Instantiate(playerItems[placedItems.Count], _spawnPoint, Quaternion.identity,
                    placePoints[placedItems.Count]);
                clone.transform.DOMove(placePoints[placedItems.Count].position, timeToPlaceItem).SetEase(Ease.Linear);
                placedItems.Add(clone);
                UpdateUI();
            }
            else
            {
                Debug.Log("no more items to place");
                playerItems.Clear();
            }
        }
    }
}