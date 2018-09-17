using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using SNDL;

namespace SM
{
    [CreateAssetMenu(menuName = "SM/Levels/Manager", order = 100)]
    public class SMLevelManager : LevelManager
    {
        SMLevelData currentExteriorLevel;
        SMLevelData currentInteriorLevel;

        protected SMGame game;
        protected string levelToLoadName;

        public virtual void init(SMGame tGame)
        {
            game = tGame;
        }

        public virtual void loadLevelByData(SMLevelData tData)
        {
            if (tData != null)
            {
                if (tData.levelType == LevelType.Interior)
                {
                    // load interior
                    Debug.Log("interior");

                    // if the scene is the same as what's already loaded
                    if (currentInteriorLevel != null && currentInteriorLevel.levelName == tData.levelName)
                    {
                        Scene tempScene = SceneManager.GetSceneByName(tData.levelName);
                        if (tempScene.isLoaded)
                        {
                            SceneManager.SetActiveScene(tempScene);
                        }
                        else
                        {
                            loadLevelByName(tData.levelName, LoadSceneMode.Additive);
                        }
                    }
                    else
                    {
                        loadLevelByName(tData.levelName, LoadSceneMode.Additive);
                    }
                }
                else
                {
                    // load exterior
                    Debug.Log("exterior");
                    if (currentExteriorLevel != null && currentExteriorLevel.levelName == tData.levelName)
                    {
                        Scene tempScene = SceneManager.GetSceneByName(tData.levelName);
                        if (tempScene.isLoaded)
                        {
                            Debug.Log("EXTERIOR ALREADY LOADED");
                            SceneManager.SetActiveScene(tempScene);
                        }
                        else
                        {
                            loadLevelByName(tData.levelName, LoadSceneMode.Single);
                        }
                    }
                    else
                    {
                        loadLevelByName(tData.levelName);
                    }
                }

                setCurrentLevel(tData);
            }
        }

        protected void setCurrentLevel(SMLevelData tLevelData)
        {
            if (tLevelData.levelType == LevelType.Interior)
            {
                currentInteriorLevel = tLevelData;
            }
            else
            {
                currentExteriorLevel = tLevelData;
            }
            Debug.Log("current level set");
        }

        protected virtual void loadLevelByName(string tLevelName, LoadSceneMode tSceneMode = LoadSceneMode.Single)
        {
            Scene tempScene = SceneManager.GetSceneByName(tLevelName);
            SceneManager.LoadScene(tLevelName, tSceneMode);
        }
    }
}
