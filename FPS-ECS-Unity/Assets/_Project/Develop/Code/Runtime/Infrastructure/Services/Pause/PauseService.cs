namespace FpsEcs.Runtime.Infrastructure.Services.Pause
{
    public class PauseService : IPauseService
    {
        public bool IsPaused { get; private set; }
        
        public void TogglePause()
        {
            IsPaused = !IsPaused;
        }
    }
}