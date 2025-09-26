namespace FpsEcs.Runtime.Infrastructure.Services.Pause
{
    public interface IPauseService
    {
        bool IsPaused { get; }
        void TogglePause();
    }
}