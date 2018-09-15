using System;

namespace SM
{
    //##########################
    // Enum Declaration
    //##########################
    [Serializable]
    public sealed class LevelEnum
    {
        private readonly String name;
        private readonly int value;

        public static readonly LevelEnum RACEWAY = new LevelEnum(1, "SMRaceWay");
        public static readonly LevelEnum TYCHO = new LevelEnum(2, "SMTycho");
        public static readonly LevelEnum LENNOX = new LevelEnum(3, "LENNOX");

        private LevelEnum(int value, String name)
        {
            this.name = name;
            this.value = value;
        }

        public override String ToString()
        {
            return name;
        }

    }
}

