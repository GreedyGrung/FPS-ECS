namespace FpsEcs.Runtime.Utils
{
    public static class Constants
    {
        public static class Scenes
        {
            public const string Bootstrap = "0.Bootstrap";
            public const string Game = "1.Game";
        }

        public static class Assets
        {
            public const string PlayerPrefabPath = "Prefabs/Player";
            public const string EnemiesPrefabPath = "Prefabs/Enemies";
            
            public const string EnemiesConfigsPath = "Configs/Enemies";
            public const string WeaponsConfigsPath = "Configs/Weapons";
            public const string PlayerConfigPath = "Configs/PlayerConfig";
        }

        public static class Gameplay
        {
            public const float Gravity = 9.81f;
        }
    }
}