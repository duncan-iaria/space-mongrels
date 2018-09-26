using UnityEngine;
using UnityEngine.EventSystems;
using SNDL;

namespace SM
{
    public class SMPawnMenu : Pawn
    {
        public EventSystem eventSystem;
        public GameObject selectedObject;

        // for closing out of menu a few levels deep(effectively resetting "selectedObject")
        protected GameObject initSelectedObject;

        [Header("Menu Objects")]
        // for comparing if we can go back or close menu when pressing back
        public GameObject topLevelMenu;
        public GameObject currentMenu;
        public GameObject previousMenu;

        protected bool buttonSelected = false;

        protected override void Awake()
        {
            // save initial selection for resetting when closed
            initSelectedObject = selectedObject;
        }

        //=======================
        // Nav Controls
        //=======================
        // catch the left axis input
        public override void onLeftAxis(Vector2 _axis)
        {
            if (_axis != Vector2.zero && buttonSelected == false)
            {
                eventSystem.SetSelectedGameObject(selectedObject);
                buttonSelected = true;
            }
        }

        public override void onAxis(InputAxis tAxis, float tValue)
        {
            if (tValue != 0 && buttonSelected == false)
            {
                //Debug.Log( "Snoot2" );
                eventSystem.SetSelectedGameObject(selectedObject);
                buttonSelected = true;
            }
        }

        public override void onCancel()
        {
            if (previousMenu != null)
            {
                selectPreviousMenu(currentMenu);
            }
            else
            {
                currentMenu.SetActive(false);

                //unpause the game
                SMGame.instance.isPaused = false;
            }
        }

        public override void onPause()
        {
            //close the main menu
            currentMenu.SetActive(false);
            Game.instance.isPaused = false;
        }

        //=======================
        // Menu Item
        //=======================
        //set a new selected object manually (when navigating to new menus)
        //commonly called through menu events (onClick)
        public virtual void setSelected(GameObject _selected)
        {
            eventSystem.SetSelectedGameObject(_selected);
        }

        //=======================
        // Menu Selection
        //=======================
        //set current menu (for turning off current menu)
        public virtual void setCurrentMenu(GameObject _newCurrent)
        {
            currentMenu = _newCurrent;
        }

        //hook for GUI
        public virtual void setPreviousMenu(GameObject _newPrevious)
        {
            previousMenu = _newPrevious;
        }

        //set previous menu as current level, and set new previous menu if there is one
        public virtual void selectPreviousMenu(GameObject _newPrevious = null)
        {
            //check if there is a previous menu
            if (previousMenu != null)
            {
                //open previous menu item
                previousMenu.SetActive(true);

                //hacky as fuck way to get a new selected obj
                //TODO fix this
                selectedObject = previousMenu.transform.GetChild(0).GetChild(0).gameObject;
                setSelected(selectedObject);
                Debug.Log("selected = " + selectedObject);

                //close current menu item
                currentMenu.SetActive(false);

                currentMenu = previousMenu;

                if (_newPrevious != null)
                {
                    //if it's the top menu item, clear it, otherwise set it to new previous
                    _newPrevious = topLevelMenu ? previousMenu = null : previousMenu = _newPrevious;
                }
            }
        }

        //=======================
        // Pawn Set/Unset Actions
        //=======================
        public override void onPawnUnset()
        {
            buttonSelected = false;
            previousMenu = null;

            //reset selection
            selectedObject = initSelectedObject;
        }

        //=======================
        // Disable (Clear)
        //=======================
        protected void OnDisable()
        {
        }
    }
}
