using UnityEngine;
using UnityEditor;

namespace SM
{
  [CustomEditor(typeof(InventoryUIController))]
  public class InventoryUIControllerDrawer : Editor
  {
    public override void OnInspectorGUI()
    {
      DrawDefaultInspector();

      InventoryUIController inventoryController = (InventoryUIController)target;
      if (GUILayout.Button("Build Inventory UI"))
      {
        inventoryController?.buildInventoryUi(true);
      }
    }
  }
}

