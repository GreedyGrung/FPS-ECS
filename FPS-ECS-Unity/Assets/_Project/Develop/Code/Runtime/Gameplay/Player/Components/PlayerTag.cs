namespace FpsEcs.Runtime.Gameplay.Player.Components
{
    public struct PlayerTag
    {
        
    }
    
    public struct MovementInfo
    {
        public float Speed;
    }
    
    public struct MovementRuntime
    {
        public float YVelocity;
    }

    public struct CameraState
    {
        public float Yaw;        // вокруг Y (тело)
        public float Pitch;      // вокруг X (камера)
        public float Sensitivity; // множитель для дельты мыши
        public float MinPitch;   // -89..-60
        public float MaxPitch;   //  60.. 89
    }
}