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

    private Item _currentItem;
    [SerializeField] private int itemsCount;
    [SerializeField] private int maxItemsCount;
    [SerializeField] private TextMeshProUGUI countText;

    private Vector3 _spawnPoint;
    

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory _playerInventory;
        if (other.TryGetComponent<PlayerInventory>(out _playerInventory))
        {
            _spawnPoint = _playerInventory.transform.position;
            for (int i = 0; i < _playerInventory.playerInventory.Count; i++)
            {
                _currentItem = _playerInventory.playerInventory[itemsCount];
                StartCoroutine(CreateItem(_spawnPoint));
                _playerInventory.AllowPlayerPlaceItems();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerInventory _playerInventory;
        if (other.TryGetComponent<PlayerInventory>(out _playerInventory))
        {
            _playerInventory.ForbidPlayerPlaceItems();
            itemsCount = 0;
        }
    }

    private void UpdateUI()
    {
        countText.text = $"{itemsCount}/{maxItemsCount}";
    }

    private IEnumerator CreateItem(Vector3 startPos)
    {
        var clone = Instantiate(_currentItem,startPos,Quaternion.identity,placePoints[itemsCount]);
        placedItems.Add(clone);
        clone.transform.DOMove(placePoints[itemsCount].position, 0.35f).SetEase(Ease.Linear);
        itemsCount++;
        UpdateUI();
        yield return new WaitForSeconds(0.35f);
        if (placedItems.Count != placePoints.Count)
        {
            yield return CreateItem(_spawnPoint);
        }
        else
        {
            Debug.Log("no space");
        }
    }
}
