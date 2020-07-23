using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SM
{
  public class SMItemGUI : MonoBehaviour
  {
    public Item Item
    {
      get { return item; }
      set { item = value; init(); }
    }

    public Image uiSpriteImage;
    public TextMeshProUGUI uiQuantityText;

    private Item item;

    public void init()
    {
      if (item)
      {
        uiSpriteImage.sprite = item.inventorySprite;
        uiQuantityText.text = "1";
      }
    }
  }
}
