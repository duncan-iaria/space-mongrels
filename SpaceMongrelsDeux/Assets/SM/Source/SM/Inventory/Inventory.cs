using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SM
{
  [CreateAssetMenu(menuName = "SM/Inventory/Inventory", order = 0)]
  [System.Serializable]
  public class Inventory : ScriptableObject, ISerializationCallbackReceiver
  {
    public bool isClearedOnReset = false;
    public int maxItemSlots = 8;
    public List<InventoryItem> inventory;

    public UnityEvent onInventoryFull;
    public UnityEvent onInventoryItemAdded;

    public bool addToInventory(Item tItem)
    {
      if (tItem)
      {
        Debug.Log("Adding " + tItem.displayName + " to invetory");
        InventoryItem tempItem = new InventoryItem(tItem, 1);

        foreach (InventoryItem item in inventory)
        {
          if (item.item == tItem)
          {
            if (item.quantity < item.item.maxStackSize)
            {
              item.quantity++;
              onInventoryItemAdded?.Invoke();
              return true;
            }
          }
        }

        if (inventory.Count >= maxItemSlots)
        {
          // Inventory is full - Don't add item
          onInventoryFull?.Invoke();
          return false;
        }

        // Couldn't increment an existing item, we we're adding it
        inventory.Add(tempItem);
        onInventoryItemAdded?.Invoke();
        return true;
      }

      // No item was passed in
      return false;
    }
    public void OnAfterDeserialize()
    {
      if (isClearedOnReset)
      {
        inventory.Clear();
      }
    }



    public void OnBeforeSerialize() { }

  }

}

