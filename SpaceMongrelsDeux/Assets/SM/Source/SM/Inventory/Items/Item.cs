using UnityEngine;

namespace SM
{
  // This bad boy is the base data object for an item, it's the shape that more
  // Things are passed around in, and how a designer would make a new item
  [CreateAssetMenu(menuName = "SM/Inventory/Item", order = 100)]
  [System.Serializable]
  public class Item : ScriptableObject
  {
    public string displayName;
    public string id;

    public int itemValue;

    public ItemType itemType;

    public float itemMass = 1f;

    public int maxStackSize = 1;

    [Header("Art")]
    public Sprite inventorySprite;
    public Sprite gameSprite;
  }
}
