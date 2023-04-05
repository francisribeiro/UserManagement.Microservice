using AutoMapper;
using UserManagement.Domain.Enums;
using UserManagement.Domain.Entities;
using UserManagement.Application.DTOs;
using UserManagement.Application.Contracts;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Exceptions.Role;
using UserManagement.Application.Exceptions.Permission;

namespace UserManagement.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;

        public RoleService(
            IMapper mapper,
            IRoleRepository roleRepository,
            IPermissionRepository permissionRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task<RoleDto> CreateRoleAsync(UserRoleType type)
        {
            var roleExists = await _roleRepository.RoleExistsAsync(type);

            if (roleExists)
                throw new RoleAlreadyExistsException(type);

            var newRole = new Role(type);

            await _roleRepository.CreateAsync(newRole);

            return _mapper.Map<RoleDto>(newRole);
        }

        public async Task UpdateRoleAsync(Guid id, UserRoleType newRoleType)
        {
            var roleExists = await _roleRepository.RoleExistsAsync(newRoleType);

            if (roleExists)
                throw new RoleInUseException(newRoleType);

            var role = await _roleRepository.GetByIdAsync(id) ?? throw new RoleNotFoundException(id);

            role.UpdateRoleType(newRoleType);

            await _roleRepository.UpdateAsync(role);
        }

        public async Task DeleteRoleAsync(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id) ?? throw new RoleNotFoundException(id);

            await _roleRepository.EnsureRoleNotInUseAsync(id);

            await _roleRepository.DeleteAsync(role);
        }

        public async Task<RoleDto> GetRoleByIdAsync(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id);

            return role == null ? throw new RoleNotFoundException(id) : _mapper.Map<RoleDto>(role);
        }

        public async Task<IEnumerable<RoleDto>> GetRolesListAsync()
        {
            var roles = await _roleRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<RoleDto> CreatePermissionOnRoleAsync(Guid roleId, Guid permissionId)
        {
            var role = await _roleRepository.GetByIdAsync(roleId) ?? throw new RoleNotFoundException(roleId);
            var permission = await _permissionRepository.GetByIdAsync(permissionId) ??
                             throw new PermissionNotFoundException(permissionId);

            if (role.HasPermission(permission.Type))
                throw new PermissionAlreadyExistsException(permissionId);

            role.AddPermission(permission);

            await _roleRepository.UpdateAsync(role);

            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> DeletePermissionFromRoleAsync(Guid roleId, Guid permissionId)
        {
            var role = await _roleRepository.GetByIdAsync(roleId) ?? throw new RoleNotFoundException(roleId);
            var permission = await _permissionRepository.GetByIdAsync(permissionId) ??
                             throw new PermissionNotFoundException(permissionId);

            role.RemovePermission(permission);

            await _roleRepository.UpdateAsync(role);

            return _mapper.Map<RoleDto>(role);
        }
    }
}