namespace FpsEcs.Runtime.Configs.Interfaces
{
    public interface IEntityConfig : IConfig
    {
        float Health { get; }
        float Speed { get; }
    }
}