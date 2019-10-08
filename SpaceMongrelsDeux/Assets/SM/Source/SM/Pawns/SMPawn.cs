using SNDL;
using UnityEngine;

namespace SM
{
  public class SMPawn : Pawn
  {
    public SMInteractable currentInteractable;
    protected Rigidbody2D _rigidbody;
    protected SMGame _game;

    protected override void Awake()
    {
      base.Awake();

      //store a ref to the rigidbody
      _rigidbody = GetComponent<Rigidbody2D>();
      _game = Game.GetGame<SMGame>();
    }

    //Universal Input controls
    public override void onCancel()
    {
      SMGame.instance.togglePause();
    }

    public override void onPause() { }

    //=======================
    // Pawn Controls
    //=======================
    public override void onInputButton(InputButton tButton) { }
    public override void onAxis(InputAxis tAxis, float tValue) { }

    public Transform getTransform()
    {
      if (this != null)
      {
        return this.transform;
      }
      else return null;
    }
  }
}
