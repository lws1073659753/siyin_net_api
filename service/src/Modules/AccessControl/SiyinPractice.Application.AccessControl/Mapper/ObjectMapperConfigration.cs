using SiyinPractice.Domain.AccessControl;
using SiyinPractice.Framework.Mapper;
using SiyinPractice.Shared.AccessControl.Dto;
using System.Collections.Generic;

namespace SiyinPractice.Application.AccessControl.Mapper
{
    public class ObjectMapperConfigration : IObjectMapperConfigration
    {
        public IList<ObjectMapperCreater> ObjectMapperCreaterBuilder()
        {
            var mappingData = new List<ObjectMapperCreater>();
            mappingData.Add(new ObjectMapperCreater(typeof(ZTreeNodeDto<,>), typeof(Node<>)));
            mappingData.Add(new ObjectMapperCreater<CreateMenuDto, SysMenu>());
            mappingData.Add(new ObjectMapperCreater<UpdateMenuDto, SysMenu>());
            mappingData.Add(new ObjectMapperCreater<SysMenu, MenuDto>().ReverseMap());
            mappingData.Add(new ObjectMapperCreater<MenuDto, MenuRouterDto>());
            mappingData.Add(new ObjectMapperCreater<SysMenu, MenuRouterDto>());
            mappingData.Add(new ObjectMapperCreater<SysMenu, MenuNodeDto>());
            mappingData.Add(new ObjectMapperCreater<MenuDto, MenuNodeDto>());
            mappingData.Add(new ObjectMapperCreater<SysRelation, RelationDto>());
            mappingData.Add(new ObjectMapperCreater<CreateRoleDto, SysRole>());
            mappingData.Add(new ObjectMapperCreater<UpdateRoleDto, SysRole>());
            mappingData.Add(new ObjectMapperCreater<SysRole, RoleDto>().ReverseMap());
            mappingData.Add(new ObjectMapperCreater<CreateUserDto, SysUser>());
            mappingData.Add(new ObjectMapperCreater<UpdateUserDto, SysUser>());
            mappingData.Add(new ObjectMapperCreater<SysUser, UserDto>());
            mappingData.Add(new ObjectMapperCreater<CreateDepartmentDto, SysDept>());
            mappingData.Add(new ObjectMapperCreater<UpdateDepartmentDto, SysDept>());
            mappingData.Add(new ObjectMapperCreater<SysDept, DepartmentDto>());
            mappingData.Add(new ObjectMapperCreater<SysDept, DepartmentTreeDto>());
            return mappingData;
        }
    }
}