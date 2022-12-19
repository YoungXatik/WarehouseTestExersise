using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private bool _canTakeItems;
    private float _pickUpTimerCount;
    private Item _currentPickedItem;
    private ItemsSpawner _currentItemSpawner;
    
    [SerializeField] private float timeToPickUpItem;
    [SerializeField] private Transform pickUpPosition;
    [SerializeField] private float pickUpItemYOffset;
    
    public List<Item> playerInventory = new List<Item>();

        private void Update()
    {
        if (_canTakeItems)
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

    public void PlayerEnterTakeTrigger()
    {
        _canTakeItems = true;
    }
    
    public void PlayerExitTakeTrigger()
    {
        _canTakeItems = false;
    }
    
    public void SelectItemToPlayer(Item item,ItemsSpawner spawner)
    {
        _currentItemSpawner = spawner;
        _currentPickedItem = item;
    }
    
    private void PickUpItem()
    {
        Instantiate(_currentPickedItem,
            new Vector3(pickUpPosition.position.x,
                pickUpPosition.position.y + (pickUpItemYOffset * playerInventory.Count), pickUpPosition.position.z),
            Quaternion.identity,pickUpPosition);
        playerInventory.Add(_currentPickedItem);
        _currentItemSpawner.RemoveItem();
    }
}
