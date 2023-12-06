namespace SiyinPractice.Domain.Core
{
    public class NamedEntity : Entity, INamedEntity
    {
        public virtual string Name { get; set; }
    }
}