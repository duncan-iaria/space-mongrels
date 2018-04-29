using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SM
{
    public class SMBoundaryGenerator : MonoBehaviour
    {
        public LineRenderer lineRenderer;
        public List<Vector3> bouys = new List<Vector3>();
        public Vector3[] bouyArray;
        // Use this for initialization
        void Start()
        {
            int tempLength = this.transform.childCount;
            bouyArray = new Vector3[tempLength];

            for (int i = 0; i < tempLength; i++)
            {
                bouyArray[i] = this.transform.GetChild(i).transform.position;
            }

            foreach (Transform child in transform)
            {
                // bouyArray.( child.transform.position );
                bouys.Add(child.transform.position);
            }


            lineRenderer.SetPositions(bouyArray);
        }
    }
}
