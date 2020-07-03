using UnityEngine;
using SNDL;

namespace SM
{
  public class SMUIGateNavigationGuide : MonoBehaviour
  {
    public SMLevelGate gate;
    private Transform player;
    private SMGame game;
    private Camera gameCamera;
    // Start is called before the first frame update
    void Start()
    {
      game = Game.GetGame<Game>() as SMGame;
      gameCamera = game.view.cam;
      player = game.controller.currentPawn.transform;
    }

    // Update is called once per frame
    void Update()
    {
      if (player != null && gameCamera != null)
      {
        Vector3 tempGatePoint = gameCamera.WorldToViewportPoint(gate.transform.position);

        float tempX = Mathf.Clamp(tempGatePoint.x, -1, 1);
        float tempY = Mathf.Clamp(tempGatePoint.y, -1, 1);

        Vector3 tempPos = new Vector3(tempX, tempY, 15);
        this.transform.position = gameCamera.ViewportToWorldPoint(tempPos);
        // Debug.Log("screen point " + tempX + " " + tempY);
      }
      else
      {
        Debug.Log("no player found");
      }
    }
  }
}
