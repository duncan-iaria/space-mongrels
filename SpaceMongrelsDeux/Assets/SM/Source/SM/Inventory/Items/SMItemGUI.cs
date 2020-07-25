using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SM
{
  public class SMItemGUI : MonoBehaviour
  {
    public InventoryItem Item
    {
      get { return item; }
      set { item = value; init(); }
    }

    public Image uiSpriteImage;
    public TextMeshProUGUI uiQuantityText;

    private InventoryItem item;

    public void init()
    {
      Debug.Log("Creating new GUI item");
      if (item != null)
      {
        Debug.Log("GUI Item successfully created");
        uiSpriteImage.sprite = item.item.inventorySprite;
        uiQuantityText.text = item.quantity.ToString();
      }
    }
  }
}
