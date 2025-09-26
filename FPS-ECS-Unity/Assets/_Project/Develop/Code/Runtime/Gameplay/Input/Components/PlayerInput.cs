using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Input.Components
{
    public struct PlayerInput
    {
        public Vector2 Move;
        public Vector2 Look;
        public bool AttackPressed;
    }
}