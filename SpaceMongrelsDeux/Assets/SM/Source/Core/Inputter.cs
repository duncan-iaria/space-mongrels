//TODO 
//Joystick is NOT forwarding 0 for controllers so the characters float - FIXIT
//Consider reworking the "Global" controls in the input section
//Make sure things are forwarding right (i havent messed up variable name)
//Consider just SENDING buttons to the controller and having the controller parse

using UnityEngine;

namespace SNDL
{
    //#######################
    // Input
    //#######################
    public class Inputter : MonoBehaviour
    {
        [Header("Components")]
        public Controller controller;

        [Header("Axes")]
        public string leftAxisHorizontal = "Horizontal";
        public string leftAxisVertical = "Vertical";
        public string rightAxisHorizontal = "RHorizontal";
        public string rightAxisVertical = "RVertical";

        [Header("Buttons")]
        public string cancelButton = "Cancel";
        public string pauseButton = "Pause";
        // public string cycleButton = "Cycle";
        public string cycleRightButton = "CycleRight";
        public string cycleLeftButton = "CycleLeft";
        public string acceptButton = "Accept";
        public string menuButton = "Menu";
        public string boostButton = "Boost";

        //left axis
        protected float leftAxisVerticalValue;
        protected float leftAxisHorizontalValue;

        //right axis
        protected float rightAxisVerticalValue;
        protected float rightAxisHorizontalValue;

        //trigger
        protected float triggerAxis;

        //=======================
        // UPDATE TICK
        //=======================
        void Update()
        {
            handleButtonInput();

            if (controller.currentPawn != null)
            {
                //handle left vertical - old way - only forwarding when value
                //if( handleAxis( leftAxisVertical, ref leftAxisVerticalValue ) != 0 )
                //{
                //	controller.onAxis( InputAxis.Vertical, leftAxisVerticalValue );
                //	//Debug.Log( "left axis vert is = " + leftAxisVerticalValue );
                //}

                //if( handleAxis( leftAxisHorizontal, ref leftAxisHorizontalValue ) != 0 )
                //{
                //	controller.onAxis( InputAxis.Horizontal, leftAxisHorizontalValue );
                //	//Debug.Log( "left axis horz is = " + leftAxisHorizontalValue );
                //}

                //always forwarding TODO see if I can get the old way to work
                controller.onAxis(InputAxis.Vertical, handleAxis(leftAxisVertical, ref leftAxisVerticalValue));
                controller.onAxis(InputAxis.Horizontal, handleAxis(leftAxisHorizontal, ref leftAxisHorizontalValue));

                //right thumbstick axis
                controller.onAxis(InputAxis.RightVertical, handleAxis(rightAxisVertical, ref rightAxisVerticalValue));
                controller.onAxis(InputAxis.RightHorizontal, handleAxis(rightAxisHorizontal, ref rightAxisHorizontalValue));


                //HANDLE IF PAUSED - STILL FORWARD INPUT RAW INPUT
                if (Game.instance.isPaused)
                {
                    //handleAxisVert
                    if (handleAxisRaw(leftAxisVertical, ref leftAxisVerticalValue) != 0)
                    {
                        controller.onAxis(InputAxis.Vertical, leftAxisVerticalValue);
                    }
                }
            }
        }

        //=======================
        // GATHER AXES
        //=======================
        //GENERIC Axis handling (should take care of all possible axis)
        //pass in the reference so that hopefully the data is persistent 
        protected virtual float handleAxis(string tAxisName, ref float tPrevious)
        {
            float tempAxis = Input.GetAxis(tAxisName);

            if (tempAxis != tPrevious)
            {
                tPrevious = tempAxis;
                return tempAxis;
            }
            else
            {
                return tPrevious;
            }
        }

        //same as above, but use the raw axis instead of the smoothed one
        protected virtual float handleAxisRaw(string tAxisName, ref float tPrevious)
        {
            float tempAxis = Input.GetAxisRaw(tAxisName);

            if (tempAxis != tPrevious)
            {
                tPrevious = tempAxis;
                return tempAxis;
            }
            else
            {
                return tPrevious;
            }
        }

        //=======================
        // GATHER BUTTON INPUTS
        //=======================
        protected virtual void handleButtonInput()
        {
            //GLOBAL
            if (Input.GetButtonDown(cancelButton))
            {
                controller.onPressCancel();
            }

            if (Input.GetButtonDown(pauseButton))
            {
                controller.onPressPause();
            }

            // if (Input.GetButtonDown(cycleButton))
            // {
            //     // controller.onPressCycle();
            //     controller.onInputButton(InputButton.Cycle);
            // }

            if (Input.GetButtonDown(cycleLeftButton))
            {
                // controller.onPressCycle();
                controller.onInputButton(InputButton.CycleLeft);
            }

            if (Input.GetButtonDown(cycleRightButton))
            {
                // controller.onPressCycle();
                controller.onInputButton(InputButton.CycleRight);
            }

            //PAWN
            if (Input.GetButtonDown(acceptButton))
            {
                controller.onInputButton(InputButton.Accept);
            }

            if (Input.GetButtonDown(menuButton))
            {
                controller.onInputButton(InputButton.Menu);
            }

            if (Input.GetButtonDown(boostButton))
            {
                controller.onInputButton(InputButton.Boost);
            }
        }
    }
}
