using UnityEngine;
using UnityEditor;

namespace SM
{
  [CustomEditor(typeof(InventoryController))]
  public class InventoryControllerDrawer : Editor
  {
    public override void OnInspectorGUI()
    {
      DrawDefaultInspector();

      InventoryController inventoryController = (InventoryController)target;
      if (GUILayout.Button("Build Inventory UI"))
      {
        inventoryController?.buildInventoryUi();
      }
    }
  }
}

