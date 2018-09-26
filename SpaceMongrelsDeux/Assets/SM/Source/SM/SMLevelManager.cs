﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using SNDL;

namespace SM
{
    [CreateAssetMenu(menuName = "SM/Levels/Manager", order = 100)]
    public class SMLevelManager : LevelManager
    {
        public SMLevelData currentLevel;
        public SMLevelSet currentLoadedLevels;
        public SMLevelData defaultExteriorLevel;
        protected SMLevelData currentExteriorLevel;
        protected SMLevelData currentInteriorLevel;
        protected SMGame game;
        protected string levelToLoadName;

        public virtual void init(SMGame tGame)
        {
            game = tGame;
        }

        public virtual void loadLevelByData(SMLevelData tLevelData)
        {
            if (tLevelData != null)
            {
                if (tLevelData.levelType == LevelType.Interior)
                {
                    loadInteriorLevel(tLevelData);
                }
                else
                {
                    loadExteriorLevel(tLevelData);
                }

                setCurrentLevel(tLevelData);
            }
        }

        protected void loadInteriorLevel(SMLevelData tLevelData)
        {
            // load interior
            Debug.Log("interior");

            // if the scene is the same as what's already loaded
            if (currentInteriorLevel != null && currentInteriorLevel.levelName == tLevelData.levelName)
            {
                Scene tempScene = SceneManager.GetSceneByName(tLevelData.levelName);
                if (tempScene.isLoaded)
                {
                    SMLevel tempLoadedLevel = getLoadedLevelByData(tLevelData);
                    tempLoadedLevel.reinitializeLevel();
                }
                else
                {
                    loadLevelByName(tLevelData.levelName);
                }
            }
            else
            {
                loadLevelByName(tLevelData.levelName);
            }
        }

        protected void loadExteriorLevel(SMLevelData tLevelData)
        {
            // load exterior
            Debug.Log("exterior");

            if (currentExteriorLevel != null && currentExteriorLevel.levelName == tLevelData.levelName)
            {
                Scene tempScene = SceneManager.GetSceneByName(tLevelData.levelName);
                if (tempScene.isLoaded)
                {
                    Debug.Log("EXTERIOR ALREADY LOADED");
                    SMLevel tempLoadedLevel = getLoadedLevelByData(tLevelData);
                    tempLoadedLevel.reinitializeLevel();
                }
                else
                {
                    swapExteriorLoadedLevel(tLevelData);
                }
            }
            else
            {
                swapExteriorLoadedLevel(tLevelData);
            }
        }

        protected void swapExteriorLoadedLevel(SMLevelData tNextLevel)
        {
            if (currentExteriorLevel != null && getLoadedLevelByData(currentExteriorLevel) != null)
            {
                unloadLevelByName(currentExteriorLevel.levelName);
            }

            loadLevelByName(tNextLevel.levelName);
        }

        public void setCurrentLevel(SMLevelData tLevelData)
        {
            if (tLevelData.levelType == LevelType.Interior)
            {
                currentInteriorLevel = tLevelData;
            }
            else
            {
                currentExteriorLevel = tLevelData;
            }

            currentLevel = tLevelData;
        }

        public virtual SMLevel getCurrentLevel()
        {
            return getLoadedLevelByData(currentLevel);
        }

        protected virtual SMLevel getLoadedLevelByData(SMLevelData tData)
        {
            for (int i = currentLoadedLevels.items.Count - 1; i >= 0; --i)
            {
                if (tData.levelName == currentLoadedLevels.items[i].levelName)
                {
                    return currentLoadedLevels.items[i];
                }
            }

            Debug.Log("No loaded level found.");
            return null;
        }

        public void loadCurrentExteriorLevel()
        {
            if (currentExteriorLevel != null)
            {
                game.loadLevel(currentExteriorLevel);
            }
            else
            {
                game.loadLevel(defaultExteriorLevel);
            }
        }

        protected virtual void loadLevelByName(string tLevelName, LoadSceneMode tSceneMode = LoadSceneMode.Additive)
        {
            SceneManager.LoadScene(tLevelName, tSceneMode);
        }

        protected virtual void unloadLevelByName(string tLevelName)
        {
            SceneManager.UnloadSceneAsync(tLevelName);
        }
    }
}
