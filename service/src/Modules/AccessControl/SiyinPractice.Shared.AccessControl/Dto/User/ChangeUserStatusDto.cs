using SiyinPractice.Shared.Core.Dto;
using System;

namespace SiyinPractice.Shared.AccessControl.Dto
{
    public class ChangeUserStatusDto : IDto
    {
        public Guid[] UserIds { get; set; }

        public int Status { get; set; }
    }
}