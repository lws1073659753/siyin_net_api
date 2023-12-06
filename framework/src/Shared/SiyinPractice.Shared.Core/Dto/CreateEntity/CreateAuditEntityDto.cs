namespace SiyinPractice.Shared.Core.Dto
{
    public class CreateAuditEntityDto : CreateNamedEntityDto, ICreateAuditEntityDto
    {
        public string Description { get; set; }
    }
}