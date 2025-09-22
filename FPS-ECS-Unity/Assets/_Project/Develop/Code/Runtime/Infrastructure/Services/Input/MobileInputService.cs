using System;

namespace FpsEcs.Runtime.Infrastructure.Services.Input
{
    public class MobileInputService : IInputService
    {
        public int InputX { get; }
        public int InputY { get; }
        
        public bool AttackInput { get; }

        public event Action OnPauseInput;

        public void Dispose()
        {
            
        }
    }
}