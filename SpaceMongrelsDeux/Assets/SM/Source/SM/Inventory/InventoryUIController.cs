using UnityEngine;

namespace SM
{
  // Responsible for making the UI look right and tite
  public class InventoryUIController : MonoBehaviour
  {
    public Inventory inventory;
    public GameObject inventoryPanel;
    public GameObject itemGuiPrefab;

    public void buildInventoryUi(bool isInEditMode = false)
    {
      if (inventory && inventoryPanel && itemGuiPrefab)
      {
        clearInventory(isInEditMode);
        drawInventoryUi();
      }
      else
      {
        Debug.LogWarning("Inventory, Inventory Panel, or Invetory Item Prefab not assigned, could not draw inventory UI");
      }
    }

    private void clearInventory(bool isInEditMode = false)
    {
      // Clear current UI
      SMItemGUI[] allUiItems = inventoryPanel.GetComponentsInChildren<SMItemGUI>(true);

      foreach (SMItemGUI uiItem in allUiItems)
      {
        if (isInEditMode)
        {
          DestroyImmediate(uiItem.gameObject);
        }
        else
        {
          Destroy(uiItem.gameObject);
        }
      }
    }

    private void drawInventoryUi()
    {
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

    public void toggleInventory()
    {
      Debug.Log("toggling inventory...");
      if (inventoryPanel.activeSelf)
      {
        Debug.Log("it's on, turning off...");
        inventoryPanel.SetActive(false);
      }
      else
      {
        Debug.Log("it's off, turning on...");
        inventoryPanel.SetActive(true);
      }
    }
  }
}
