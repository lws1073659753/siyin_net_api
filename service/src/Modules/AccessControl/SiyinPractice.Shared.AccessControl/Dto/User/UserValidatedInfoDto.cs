using SiyinPractice.Shared.Core.Dto;
using System;

namespace SiyinPractice.Shared.AccessControl.Dto
{
    [Serializable]
    public record UserValidatedInfoDto : IDto
    {
        public UserValidatedInfoDto(Guid id, string account, string name, string roleids, int status)
        {
            Id = id;
            Account = account;
            Name = name;
            RoleIds = roleids;
            Status = status;
            ValidationVersion = Guid.NewGuid().ToString("N");
        }

        public Guid Id { get; init; }

        public string Account { get; init; }

        public string Name { get; init; }

        //public string Email { get; set; }

        public string RoleIds { get; init; }

        public int Status { get; init; }

        public string ValidationVersion { get; init; }
    }
}