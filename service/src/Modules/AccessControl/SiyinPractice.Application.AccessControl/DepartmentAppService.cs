using SiyinPractice.Application.Core;
using SiyinPractice.Domain.AccessControl;
using SiyinPractice.Domain.Core;
using SiyinPractice.Interface.AccessControl;
using SiyinPractice.Shared.AccessControl.Dto;
using SiyinPractice.Shared.AccessControl.Dto.Dept;
using SiyinPractice.Shared.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiyinPractice.Application.AccessControl;

public class DepartmentAppService : AppService, IDepartmentAppService
{
    private readonly IEfRepository<SysDept> _deptRepository;

    public DepartmentAppService(IEfRepository<SysDept> deptRepository)
    {
        _deptRepository = deptRepository;
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        return await _deptRepository.DeleteRangeAsync(d => d.Id == id);
    }

    public async Task<Guid> CreateAsync(CreateDepartmentDto input)
    {
        var isExists = await _deptRepository.AnyAsync(x => x.FullName == input.FullName);
        Validate.Assert(isExists, "该部门全称已经存在");

        var dept = Mapper.Map<SysDept>(input);
        await this.SetDeptPids(dept);
        await _deptRepository.InsertAsync(dept);

        return dept.Id;
    }

    public async Task<int> UpdateAsync(Guid id, UpdateDepartmentDto input)
    {
        var oldDeptDto = await _deptRepository.FindAsync(id);
        Validate.Assert(oldDeptDto.Pid == Guid.Empty && input.Pid != Guid.Empty, "一级单位不能修改等级");
        var isExists = await _deptRepository.AnyAsync(x => x.FullName == input.FullName && x.Id != id);
        Validate.Assert(isExists, "该部门全称已经存在");
        var deptEnity = Mapper.Map<SysDept>(input);
        deptEnity.Id = id;

        if (oldDeptDto.Pid == input.Pid)
        {
            await _deptRepository.UpdateAsync(deptEnity, UpdatingProps<SysDept>(x => x.FullName, x => x.SimpleName, x => x.Tips, x => x.Ordinal));
        }
        else
        {
            await this.SetDeptPids(deptEnity);
            await _deptRepository.UpdateAsync(deptEnity, UpdatingProps<SysDept>(x => x.FullName, x => x.SimpleName, x => x.Tips, x => x.Ordinal, x => x.Pid, x => x.Pids));

            //zz.efcore 不支持
            //await _deptRepository.UpdateRangeAsync(d => d.Pids.Contains($"[{dept.ID}]"), c => new SysDept { Pids = c.Pids.Replace(oldDeptPids, dept.Pids) });
            var originalDeptPids = $"{oldDeptDto.Pids}[{deptEnity.Id}],";
            var nowDeptPids = $"{deptEnity.Pids}[{deptEnity.Id}],";
            //var subDepts = await _deptRepository.Where(d => d.Pids.StartsWith(originalDeptPids))
            //                                    .Select(d => new { d.Id, d.Pids })
            //                                    .ToListAsync();
            var subDepts = _deptRepository.Where(d => d.Pids.StartsWith(originalDeptPids))
                                               .Select(d => new { d.Id, d.Pids })
                                               .ToList();
            foreach (var c in subDepts)
            {
                await _deptRepository.UpdateAsync(new SysDept { Id = c.Id, Pids = c.Pids.Replace(originalDeptPids, nowDeptPids) }, UpdatingProps<SysDept>(c => c.Pids));
            }
        }
        return 1;
    }

    public async Task<List<DepartmentTreeDto>> GetTreeListAsync()
    {
        var result = new List<DepartmentTreeDto>();
        var depts = _deptRepository.GetAll().ToList();
        if (!depts.Any())
            //if (depts.IsNullOrEmpty())
            return result;
        Func<SysDept, DepartmentTreeDto> selector = x => new DepartmentTreeDto
        {
            Id = x.Id,
            SimpleName = x.SimpleName,
            FullName = x.FullName,
            Ordinal = x.Ordinal,
            Pid = x.Pid,
            Pids = x.Pids,
            Tips = x.Tips,
            Version = x.Version
        };

        var roots = depts.Where(d => d.Pid == Guid.Empty)
                                    .OrderBy(d => d.Ordinal)
                                    .Select(selector)
                                    .ToList();
        roots.ForEach(node =>
        {
            GetChildren(node, depts);
            result.Add(node);
        });

        void GetChildren(DepartmentTreeDto currentNode, List<SysDept> allDeptNodes)
        {
            var childrenNodes = depts.Where(d => d.Pid == currentNode.Id)
                                                       .OrderBy(d => d.Ordinal)
                                                       .Select(selector)
                                                       .ToList();
            if (childrenNodes.Any())
            {
                currentNode.Children.AddRange(childrenNodes);
                foreach (var node in childrenNodes)
                {
                    GetChildren(node, allDeptNodes);
                }
            }
        }

        return result;
    }

    public List<DeptDto> GetAll()
    {
        var domains = _deptRepository.GetAll().ToList();
        var dtoTasks = domains.Select(x => new DeptDto()
        {
            Id = x.Id,
            FullName = x.FullName,
            Ordinal = x.Ordinal,
            Pid = x.Pid,
            Pids = x.Pids,
            SimpleName = x.SimpleName,
            Tips = x.Tips,
            Version = x.Version,
        }).ToList();
        return dtoTasks;
    }

    private async Task<SysDept> SetDeptPids(SysDept sysDept)
    {
        if (sysDept.Pid.HasValue && sysDept.Pid.Value != Guid.Empty)
        {
            var dept = await _deptRepository.FindAsync(sysDept.Pid.Value);
            string pids = dept?.Pids ?? "";
            sysDept.Pids = $"{pids}[{sysDept.Pid}],";
        }
        else
        {
            sysDept.Pid = Guid.Empty;
            sysDept.Pids = $"[{Guid.Empty}],";
        }
        return sysDept;
    }
}