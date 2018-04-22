using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNDL;

namespace SM
{
    public class SMPawnShip : SMPawn
    {
        public SMShip ship;
        public SMReactor reactor;
        // These are set by the ship object
        [HideInInspector]
        public float moveSpeed, horizontalDampening = .5f, rotationSpeed;
        [HideInInspector]
        public int maxHealth;

        protected int currentHealth;


        //=======================
        // Initialization
        //=======================
        protected override void Awake()
        {
            base.Awake();
            rebuildShipData();
        }

        protected virtual void rebuildShipData()
        {
            if (ship != null && reactor != null)
            {
                ship.initialize(gameObject);
                reactor.initialize(gameObject);
            }
        }

        //=======================
        // Pawn Ship Controls
        //=======================
        public override void onInputButton(InputButton tButton)
        {
            switch (tButton)
            {
                case InputButton.Menu:
                    loadShipInterior();
                    break;
                default:
                    break;
            }
        }

        public override void onAxis(InputAxis tAxis, float tValue)
        {
            switch (tAxis)
            {
                case InputAxis.Vertical:
                    if( tValue > 0 ) 
                    {
                        accelerate(new Vector2( 0f, tValue));
                    }
                    else
                    {
                        accelerate(new Vector2( 0f, tValue * horizontalDampening));    
                    }
                    break;
                case InputAxis.Horizontal:
                    accelerate(new Vector2(tValue, 0f), horizontalDampening);
                    break;
                case InputAxis.RightHorizontal:
                    rotate(tValue);
                    break;
                case InputAxis.RightVertical:
                    break;
                default:
                    break;
            }
        }

        //=======================
        // Movement
        //=======================
        protected virtual void accelerate(Vector2 tVector, float tDampening = 1f)
        {
            //_rigidbody.AddForce((tVector * moveSpeed) * tDampening * Time.deltaTime);
            _rigidbody.AddForce((transform.right * tVector.x * tDampening) * moveSpeed * Time.deltaTime);
            _rigidbody.AddForce((transform.up * tVector.y) * moveSpeed * Time.deltaTime);
        }

        protected virtual void rotate(float tRotationValue)
        {
            _rigidbody.AddTorque(tRotationValue * rotationSpeed * Time.deltaTime);
        }

        //=======================
        // Return to Interior
        //=======================
        protected virtual void loadShipInterior()
        {
            SMGame tempGame = SMGame.GetGame<SMGame>();
            tempGame.onLoadLevel(1, .5f, true);
        }
    }
}