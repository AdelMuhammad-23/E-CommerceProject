using AutoMapper;
using E_CommerceProject.Core.DTOs.AuthenticationDTOs;
using E_CommerceProject.Core.Entities.Identity;
using E_CommerceProject.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        #region Fields
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        #endregion
        #region Constructors
        public AuthorizationController(IAuthorizationRepository authorizationRepository,
                                         IMapper mapper,
                                         UserManager<User> userManager)
        {
            _authorizationRepository = authorizationRepository;
            _mapper = mapper;
            _userManager = userManager;
        }
        #endregion

        [HttpGet("GetManagerUserRole/{id}")]
        public async Task<IActionResult> GetManagerUserRole([FromRoute] int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var manageUserRoleResponse = await _authorizationRepository.GetManageUserRoleResponse(user);
            if (manageUserRoleResponse == null)
                return NotFound();

            return Ok(manageUserRoleResponse);
        }

        [HttpPut("EditUserRole")]
        public async Task<IActionResult> EditUserRole([FromBody] EditUserRole command)
        {
            var editUserRoleCommand = await _authorizationRepository.EditUserRoleAsync(command);
            switch (editUserRoleCommand)
            {
                case "User Not Found": return NotFound();
                case "Failed to remove old UserRoles": return BadRequest();
                case "Failed to Add New UserRoles": return BadRequest();
                case "Failed to Update UserRoles": return BadRequest();
            }
            return Ok();
        }

        [HttpGet("GetManagerUserClaims/{id}")]
        public async Task<IActionResult> GetManagerUserClaims([FromRoute] int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();

            var ManageUserClaims = await _authorizationRepository.ManageUserClaims(user);

            return Ok(ManageUserClaims);
        }

        [HttpPut("EditUserClaims")]
        public async Task<IActionResult> EditUserClaims([FromBody] EditUserClaims Command)
        {

            var editUserRoleCommand = await _authorizationRepository.EditUserClaimsAsync(Command);
            switch (editUserRoleCommand)
            {
                case "User Not Found": return NotFound();
                case "Failed to remove old UserClaims": return BadRequest();
                case "Failed to Add New UserClaims": return BadRequest();
                case "Failed to Edit  UserClaims": return BadRequest();
            }
            return Ok();
        }
    }
}
