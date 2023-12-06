namespace SiyinPractice.Domain.Core
{
    public interface INamedEntity : IEntity
    {
        string Name { get; set; }
    }
}