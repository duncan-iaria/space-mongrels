using UnityEngine;

namespace SM
{
  public class SMBoundaryGenerator : MonoBehaviour
  {
    public LineRenderer lineRenderer;
    public bool isRenderInGame = true;
    private Vector3[] bouyArray;

    void Start()
    {
      if (lineRenderer != null)
      {
        createBoundary();
      }
      else
      {
        if (isRenderInGame)
        {
          Debug.LogWarning("No LineRendered Assigned, Boundary will not render in game.");
        }
      }
    }

    void createBoundary()
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
