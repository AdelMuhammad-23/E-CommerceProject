using AutoMapper;
using E_CommerceProject.Core.DTOs.RolesDTOs;
using E_CommerceProject.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RolesController(RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpGet("Get-All-Roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            if (roles == null || roles.Count == 0)
                return NotFound("Not found roles");

            return Ok(roles);
        }
        [HttpGet("Get-Role-By/{id}")]
        public async Task<IActionResult> GetAllRoles([FromRoute] int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
                return NotFound("Not found roles");

            return Ok(role);
        }

        [HttpPost("Create-Role")]
        public async Task<IActionResult> CreateRole([FromForm] CreateRoleDTO roleDto)
        {
            if (roleDto == null)
                return NotFound("Role can't be Empty");
            var roleMapping = _mapper.Map<Role>(roleDto);
            var role = await _roleManager.CreateAsync(roleMapping);
            if (role.Succeeded)
                return Ok("Add role is successfully");
            return BadRequest("Error when add role");
        }
        [HttpPut("Update-Role")]
        public async Task<IActionResult> UpdateRole([FromForm] UpdateRoleDTO roleDto)
        {
            var exctingRole = await _roleManager.FindByIdAsync(roleDto.Id.ToString());
            if (exctingRole == null)
                return NotFound("Role is not found");
            if (roleDto == null)
                return NotFound("Role can't be Empty");
            var roleMapping = _mapper.Map(roleDto, exctingRole);
            var role = await _roleManager.UpdateAsync(roleMapping);
            if (role.Succeeded)
                return Ok("Update role is successfully");
            return BadRequest("Error when update role");
        }
        [HttpDelete("Delete-Role/{id}")]
        public async Task<IActionResult> UpdateRole([FromRoute] int id)
        {
            var exctingRole = await _roleManager.FindByIdAsync(id.ToString());
            if (exctingRole == null)
                return NotFound("Role is not found");
            var role = await _roleManager.DeleteAsync(exctingRole);
            if (role.Succeeded)
                return Ok("Delete role is successfully");
            return BadRequest("Error when Delete role");
        }
    }
}
