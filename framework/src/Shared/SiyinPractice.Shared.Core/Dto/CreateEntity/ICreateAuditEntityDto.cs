namespace SiyinPractice.Shared.Core.Dto
{
    public interface ICreateAuditEntityDto : ICreateNamedEntityDto
    {
        string Description { get; set; }
    }
}