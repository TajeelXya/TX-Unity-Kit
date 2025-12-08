namespace UniTx.Runtime.Services
{
    /// <summary>
    /// Describes data required to create or identify an entity.
    /// </summary>
    public interface IEntityData : IData
    {
        string Name { get; }

        IEntity CreateEntity();
    }
}