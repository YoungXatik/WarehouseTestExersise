using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeTrigger : MonoBehaviour
{

   private ItemsSpawner _itemsSpawner;

   private void Start()
   {
      _itemsSpawner = GetComponentInParent<ItemsSpawner>();
   }

   private void OnTriggerEnter(Collider other)
   {
      PlayerInventory playerInventory;
      if (other.TryGetComponent<PlayerInventory>(out playerInventory))
      {
         playerInventory.SelectItemToPlayer(_itemsSpawner.itemPrefab,_itemsSpawner);
         playerInventory.PlayerEnterTakeTrigger();
      }
   }

   private void OnTriggerExit(Collider other)
   {
      PlayerInventory playerInventory;
      if (other.TryGetComponent<PlayerInventory>(out playerInventory))
      {
         playerInventory.PlayerExitTakeTrigger();
      }
   }
}
