namespace FpsEcs.Runtime.Infrastructure.Factories.Entities
{
    public interface IEntityBuilder
    {
        IEntityBuilder With<T>() where T : struct;
    }
}