using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace SM
{
    [CreateAssetMenu(menuName = "SM/Levels/Data", order = 100)]
    public class SMLevelData : ScriptableObject
    {
        public string levelName;
        public string displayName;
        public LevelType levelType;
        public bool isAdditiveLevel = false;
        public List<SMLevelData> connectedLevels = new List<SMLevelData>();
    }
}

