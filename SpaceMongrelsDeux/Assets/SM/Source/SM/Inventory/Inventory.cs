using System.Collections.Generic;
using UnityEngine;

namespace SM
{
  [CreateAssetMenu(menuName = "SM/Inventory/Inventory", order = 0)]
  [System.Serializable]
  public class Inventory : ScriptableObject, ISerializationCallbackReceiver
  {
    public bool isClearedOnReset = false;
    public int maxItemSlots = 8;
    public List<InventoryItem> inventory;

    public void addToInventory(InventoryItem tItem)
    {
      if (tItem)
      {
        Debug.Log("Adding " + tItem.displayName + " to invetory");
        inventory.Add(tItem);
      }
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

