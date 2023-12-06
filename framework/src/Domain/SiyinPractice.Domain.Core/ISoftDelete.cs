namespace SiyinPractice.Domain.Core
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}