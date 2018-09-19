//TODO:
//Clean up how flipping works (flipping the sprite, seems dirty right now)
using SNDL;
using UnityEngine;

namespace SM
{
    public class SMPawnMongrel : SMPawn
    {
        public float moveSpeed;

        protected SpriteRenderer spriteRenderer;
        protected Rigidbody2D rb;
        protected Vector2 _moveVelocity;
        protected float _moveThreshold = .2f;

        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        //=======================
        // INPUT
        //=======================
        //AXIS
        public override void onAxis(InputAxis tAxis, float tValue)
        {
            if (Mathf.Abs(tValue) > _moveThreshold)
            {
                if (tAxis == InputAxis.Horizontal)
                {
                    _moveVelocity.x = tValue;

                    //check if we should flip the sprite
                    onFlipCheck(tValue);
                }
                else if (tAxis == InputAxis.Vertical)
                {
                    _moveVelocity.y = tValue;
                }
            }
            else
            {
                //reset move velocity to 0 (so no floating)
                if (tAxis == InputAxis.Horizontal)
                {
                    _moveVelocity.x = 0f;
                }
                else if (tAxis == InputAxis.Vertical)
                {
                    _moveVelocity.y = 0f;
                }
            }
        }

        //BUTTON
        public override void onInputButton(InputButton tButton)
        {
            switch (tButton)
            {
                case InputButton.Accept:
                    onAccept();
                    break;
                case InputButton.CycleRight:
                    onSelectNextPawn();
                    break;
                case InputButton.CycleLeft:
                    onSelectPreviousPawn();
                    break;
                default:
                    break;
            }
        }

        protected virtual void onAccept()
        {
            if (currentInteractable != null)
            {
                currentInteractable.onInteract();
            }
        }

        protected virtual void onSelectNextPawn()
        {
            _game.levelManager.getCurrentLevel().selectNextPawn();
        }

        protected virtual void onSelectPreviousPawn()
        {
            _game.levelManager.getCurrentLevel().selectPreviousPawn();
        }

        //=======================
        // UPDATE
        //=======================
        //handles actually moving
        protected virtual void FixedUpdate()
        {
            rb.velocity = _moveVelocity * moveSpeed;
        }

        //=======================
        // FEEDBACK
        //=======================
        protected virtual void onFlipCheck(float tValue)
        {
            if (tValue > 0)
            {
                if (spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = false;
                }
            }
            else
            {
                if (!spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = true;
                }
            }
        }

        //=======================
        // UTILITY
        //=======================
        //actions taken when pawn is set
        public override void onPawnSet()
        {
            //turn on update loop (to enable movement)
            enabled = true;
        }


        //actions taken when pawn is unset
        public override void onPawnUnset()
        {
            //clear the velocity so it no longer moves
            rb.velocity = Vector2.zero;
            _moveVelocity = Vector2.zero;

            //turn off update loop
            enabled = false;
        }

    }
}
