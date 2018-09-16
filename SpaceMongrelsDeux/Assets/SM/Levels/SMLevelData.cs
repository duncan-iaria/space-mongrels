using System.Collections.Generic;
using UnityEngine;

namespace SM
{
    [CreateAssetMenu(menuName = "SM/Levels/Data", order = 100)]
    public class SMLevelData : ScriptableObject
    {
        public string levelName;
        public string displayName;
        public bool isAdditiveLevel = false;
        public LevelType levelType;
        public List<SMLevelData> connectedLevels = new List<SMLevelData>();
    }
}

