using System.Collections.Generic;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay
{
    public class LevelDataProvider : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawn;
        [SerializeField] private List<Transform> _enemySpawns;
        
        public Transform PlayerSpawn => _playerSpawn;
        public IReadOnlyList<Transform> EnemySpawns => _enemySpawns;
    }
}