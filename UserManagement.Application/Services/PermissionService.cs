using AutoMapper;
using UserManagement.Domain.Entities;
using UserManagement.Application.DTOs;
using UserManagement.Application.Contracts;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Exceptions;

namespace UserManagement.Application.Services;
public class PermissionService : IPermissionService
{
    private readonly IMapper _mapper;
    private readonly IPermissionRepository _permissionRepository;

    public PermissionService(
        IPermissionRepository permissionRepository,
        IMapper mapper)
    {
        _permissionRepository = permissionRepository;
        _mapper = mapper;
    }
    
    public async Task<PermissionDto> CreatePermissionAsync(PermissionDto permissionDto)
    {
        var newPermission = new Permission(permissionDto.Type, permissionDto.Description);

        await _permissionRepository.CreateAsync(newPermission);

        return _mapper.Map<PermissionDto>(newPermission);
    }

    public async Task UpdatePermissionAsync(Guid id, PermissionDto permissionDto)
    {
        var permission = await _permissionRepository.GetByIdAsync(id) ?? throw new PermissionNotFoundException(id);

        permission.UpdateDescription(permissionDto.Description);

        await _permissionRepository.UpdateAsync(permission);
    }
    
    public async Task DeletePermissionAsync(Guid id)
    {
        var permission = await _permissionRepository.GetByIdAsync(id) ?? throw new PermissionNotFoundException(id);

        await _permissionRepository.EnsurePermissionNotInUseAsync(id);

        await _permissionRepository.DeleteAsync(permission);
    }

    public async Task<PermissionDto> GetPermissionByIdAsync(Guid id)
    {
        var permission = await _permissionRepository.GetByIdAsync(id);

        return permission == null ? throw new PermissionNotFoundException(id) : _mapper.Map<PermissionDto>(permission);
    }

    public async Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync()
    {
        var permissions = await _permissionRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }
}