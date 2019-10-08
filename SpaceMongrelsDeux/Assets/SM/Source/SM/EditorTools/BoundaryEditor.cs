using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SM
{
    [CustomEditor(typeof(SMBoundaryGenerator))]
    public class BoundaryEditor : Editor
    {
        void OnSceneGUI()
        {
            SMBoundaryGenerator tempBoundaryParent = target as SMBoundaryGenerator;

            if(tempBoundaryParent == null || tempBoundaryParent.transform.childCount <= 0)
                return;

            for (int i = 0; i < tempBoundaryParent.transform.childCount; i++)
            {
                Transform tempParentTransform = tempBoundaryParent.transform;
                Transform currentChild = tempParentTransform.GetChild(i);
                Handles.color = Color.cyan;

                if(i == tempParentTransform.childCount - 1)
                {
                    // Draw a line to the first child from the last one (to make it loop)
                    Handles.DrawLine(currentChild.position, tempParentTransform.GetChild(0).position);
                }
                else
                {
                    // Draw a line to the next child from the current one
                    Handles.DrawLine(currentChild.position, tempParentTransform.GetChild(i + 1).position);
                }
            }

        }
    }
}