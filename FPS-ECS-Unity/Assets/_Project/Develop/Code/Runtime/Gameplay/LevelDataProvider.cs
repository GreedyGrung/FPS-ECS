using UnityEngine;

namespace FpsEcs.Runtime.Gameplay
{
    public class LevelDataProvider : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawn;
        
        public Transform PlayerSpawn => _playerSpawn;
    }
}