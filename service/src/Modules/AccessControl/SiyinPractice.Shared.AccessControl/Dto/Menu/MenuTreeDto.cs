using SiyinPractice.Shared.Core.Dto;
using System;
using System.Collections.Generic;

namespace SiyinPractice.Shared.AccessControl.Dto;

[Serializable]
public class MenuTreeDto : IDto
{
    public IEnumerable<Node<Guid>> TreeData { get; set; }
    public IEnumerable<Guid> CheckedIds { get; set; }
}