using System.Collections.Generic;
using UnityEngine;
using SNDL;

namespace SM
{
    [CreateAssetMenu(menuName = "SM/Levels/Data", order = 100)]
    public class SMLevelData : ScriptableObject
    {
        public string levelName;
        public string displayName;
        public bool isAdditiveLevel = false;
        public LevelType levelType;
        public Vector2Variable interiorOffset;
        public List<SMLevelData> connectedLevels = new List<SMLevelData>();
    }
}

