namespace FpsEcs.Runtime.Gameplay.Player.Components
{
    public struct CameraState
    {
        public float Yaw;
        public float Pitch;
    }

    public struct CameraSettings
    {
        public float Sensitivity;
        public float MinPitch;
        public float MaxPitch;
    }
}