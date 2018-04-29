using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SM
{
    public class SMBoundaryGenerator : MonoBehaviour
    {
        public LineRenderer lineRenderer;
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

            lineRenderer.positionCount = tempLength;
            lineRenderer.SetPositions(bouyArray);
        }
    }
}
