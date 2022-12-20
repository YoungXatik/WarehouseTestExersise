using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private bool _canTakeItems;
    private float _pickUpTimerCount;
    private Item _currentPickedItem;
    private ItemsSpawner _currentItemSpawner;

    private bool _canPlaceItems;
    private float _placeTimerCount;

    [SerializeField] private float timeToPlaceItem;

    [SerializeField] private float timeToPickUpItem;
    [SerializeField] private Transform pickUpPosition;
    [SerializeField] private float pickUpItemYOffset;

    public List<Item> playerInventory = new List<Item>();
    [SerializeField] private int inventorySize;

    [SerializeField] private GameObject palette;

    [SerializeField] private Vector3 startPalettePosition, inHandsPalettePosition;
    [SerializeField] private Vector3 startPaletteRotation, inHandsPaletteRotation;

    private void Update()
    {
        if (_canTakeItems)
        {
            if (_currentItemSpawner.spawnedObjects.Count != 0)
            {
                _pickUpTimerCount += Time.deltaTime;
                if (_pickUpTimerCount >= timeToPickUpItem)
                {
                    _pickUpTimerCount = 0;
                    PickUpItem();
                }
            }
            else
            {
                _pickUpTimerCount = 0;
            }
        }
        else
        {
            _pickUpTimerCount = 0;
        }

        if (_canPlaceItems)
        {
            RemoveItem();
        }
    }

    public void PlayerEnterTakeTrigger()
    {
        _canTakeItems = true;
    }

    public void PlayerExitTakeTrigger()
    {
        _canTakeItems = false;
        _currentItemSpawner = null;
    }

    public void SelectItemToPlayer(Item item, ItemsSpawner spawner)
    {
        _currentItemSpawner = spawner;
        _currentPickedItem = item;
    }

    private void PickUpItem()
    {
        if (inventorySize > playerInventory.Count)
        {
            var clone = Instantiate(_currentPickedItem,
                new Vector3(pickUpPosition.position.x,
                    pickUpPosition.position.y + (pickUpItemYOffset * playerInventory.Count), pickUpPosition.position.z),
                Quaternion.identity, pickUpPosition);
            playerInventory.Add(clone);
            _currentItemSpawner.RemoveItem();
            ChangePalettePositionAndRotation();
        }
        else
        {
            Debug.Log("inventory filled");
        }
    }

    public void AllowPlayerPlaceItems()
    {
        _canPlaceItems = true;
    }

    public void ForbidPlayerPlaceItems()
    {
        _canPlaceItems = false;
    }

    private void RemoveItem()
    {
        for (int i = 0; i < playerInventory.Count; i++)
        {
            if (playerInventory.Count != 0)
            {
                Destroy(playerInventory[i].gameObject);
                playerInventory.RemoveAt(i);
                ChangePalettePositionAndRotation();
            }
            else
            {
                ForbidPlayerPlaceItems();
            }
        }
    }

    private float _timeToChangePalettePosition = 0.0f;

    private void ChangePalettePositionAndRotation()
    {
        if (playerInventory.Count >= 1)
        {
            palette.transform.DOLocalMove(inHandsPalettePosition, _timeToChangePalettePosition).SetEase(Ease.Linear);
            palette.transform.DOLocalRotate(inHandsPaletteRotation, _timeToChangePalettePosition)
                .SetEase(Ease.Linear);
        }
        else if (playerInventory.Count == 0)
        {
            palette.transform.DOLocalMove(startPalettePosition, _timeToChangePalettePosition).SetEase(Ease.Linear);
            palette.transform.DOLocalRotate(startPaletteRotation, _timeToChangePalettePosition)
                .SetEase(Ease.Linear);
        }
    }
}