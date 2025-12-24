namespace LeoEcsLite.QoL.Factory
{
    public interface IEntityBuilder
    {
        IEntityBuilder With<T>() where T : struct;
        int Build();
    }
}