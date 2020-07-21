using UnityEngine;

namespace SM
{
  // Responsible for making the UI look right and tite
  public class InventoryController : MonoBehaviour
  {
    public Inventory inventory;
    public GameObject inventoryPanel;
    public GameObject itemGuiPrefab;

    public void buildInventoryUi()
    {
      if (inventory && inventoryPanel && itemGuiPrefab)
      {
        // Clear current UI
        SMItemGUI[] allUiItems = inventoryPanel.GetComponentsInChildren<SMItemGUI>(true);

        foreach (SMItemGUI uiItem in allUiItems)
        {
          DestroyImmediate(uiItem.gameObject);
        }

        // Draw the UI
        foreach (InventoryItem item in inventory.inventory)
        {
          GameObject tempItemObject = Instantiate(itemGuiPrefab);
          tempItemObject.transform.SetParent(inventoryPanel.transform, false);
          SMItemGUI itemGui = tempItemObject.GetComponent<SMItemGUI>();

          if (itemGui)
          {
            itemGui.Item = item;
          }
        }
      }
      else
      {
        Debug.LogWarning("Inventory, Inventory Panel, or Invetory Item Prefab not assigned, could not draw inventory UI");
      }
    }
  }
}
