using UnityEngine;

namespace SM
{
  [System.Serializable]
  public class DropItem
  {
    public InventoryItem item;
    [Range(0, 100)]
    public float dropChance;
  }
}
