using UnityEngine;

namespace SM
{
  public class SMBoundaryGenerator : MonoBehaviour
  {
    public LineRenderer lineRenderer;
    public bool isRenderInGame = true;
    public bool isRedrawEnabled = false;

    public int framesUntilUpdate = 10;
    private Vector3[] bouyArray;

    private int frameCount = 0;


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

    void FixedUpdate()
    {
      if (!isRenderInGame || !isRedrawEnabled)
      {
        enabled = false;
      }

      frameCount++;

      if (frameCount > framesUntilUpdate)
      {
        createBoundary();
        frameCount = 0;
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
