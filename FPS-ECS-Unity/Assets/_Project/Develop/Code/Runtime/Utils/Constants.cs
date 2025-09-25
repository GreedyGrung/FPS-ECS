using UnityEngine;

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
            public const string EnemyPrefabPath = "Prefabs/Enemy";
            
            public const string EnemiesConfigsPath = "Configs/Enemies";
            public const string WeaponsConfigsPath = "Configs/Weapons";
            public const string PlayerConfigPath = "Configs/PlayerConfig";
            public const string GameConfigPath = "Configs/GameConfig";
        }
        
        public static class Layers
        {
            public const string Player = "Player";
            public const string Obstacle = "Obstacle";
            public const string Enemy = "Enemy";
        }

        public static class Gameplay
        {
            public const float Gravity = 9.81f;
            public const float FireDistance = 50f;
            public static readonly LayerMask ObstacleLayerMask =
                LayerMask.GetMask(Layers.Obstacle, Layers.Player, Layers.Enemy);
            public static readonly LayerMask EnemyLayerMask =
                LayerMask.GetMask(Layers.Enemy);
        }
    }
}