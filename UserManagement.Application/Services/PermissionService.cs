using AutoMapper;
using UserManagement.Domain.Entities;
using UserManagement.Application.DTOs;
using UserManagement.Application.Contracts;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Exceptions;
using UserManagement.Application.Exceptions.Permission;

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
        var permissionExists = await _permissionRepository.ExistsAsync(permissionDto.Type);

        if (permissionExists)
            throw new PermissionAlreadyExistsException(permissionDto.Type);       

        var newPermission = new Permission(permissionDto.Type, permissionDto.Description);
        
        await _permissionRepository.CreateAsync(newPermission);

        return _mapper.Map<PermissionDto>(newPermission);
    }

    public async Task UpdatePermissionAsync(Guid id, PermissionDto permissionDto)
    {
        var permission = await _permissionRepository.GetByIdAsync(id) ?? throw new PermissionNotFoundException(id);

        if (permission.Type != permissionDto.Type)
        {
            var permissionExists = await _permissionRepository.ExistsAsync(permissionDto.Type);

            if (permissionExists)
                throw new PermissionAlreadyExistsException(permissionDto.Type);         
        }

        if (IsPermissionUpdateNeeded(permission, permissionDto))
        {
            permission.Update(permissionDto.Description, permissionDto.Type);
            
            await _permissionRepository.UpdateAsync(permission);
        }
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

    private static bool IsPermissionUpdateNeeded(Permission permission, PermissionDto permissionDto)
    {
        return permission.Type != permissionDto.Type || permission.Description != permissionDto.Description;
    }
}