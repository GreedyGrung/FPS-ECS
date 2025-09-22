using System;

namespace FpsEcs.Runtime.Infrastructure.Services.Input
{
    public interface IInputService : IDisposable
    {
        int InputX { get; }
        
        int InputY { get; }
        
        bool AttackInput { get; }
        
        event Action OnPauseInput;
    }
}