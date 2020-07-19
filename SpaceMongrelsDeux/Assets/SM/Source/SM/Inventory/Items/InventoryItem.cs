using UnityEngine;

namespace SM
{
  [CreateAssetMenu(menuName = "SM/Inventory/Item", order = 100)]
  [System.Serializable]
  public class InventoryItem : ScriptableObject
  {
    public string displayName;
    public string id;

    public int itemValue;

    public ItemType itemType;

    public float itemMass = 1f;

    [Header("Art")]
    public Sprite inventorySprite;
    public Sprite gameSprite;
  }
}
