namespace FpsEcs.Runtime.Gameplay.Enemies.Components
{
    public struct ObstacleAvoidance 
    {
        public float CheckDistance;
        public float MinTurnAngle;
        public float MaxTurnAngle;
        public int ObstacleMask;
    }
}