using UnityEngine;
using UnityEditor;

namespace SM
{
  [CustomEditor(typeof(SMPawnShip))]
  public class SMShipDrawer : Editor
  {
    public override void OnInspectorGUI()
    {
      DrawDefaultInspector();

      SMPawnShip shipPawn = (SMPawnShip)target;
      if (GUILayout.Button("Generate Test Drop"))
      {
        shipPawn?.ship?.dropTable?.generateDrop(Vector3.zero);
      }
    }
  }
}

