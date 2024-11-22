using AutoMapper;
using E_CommerceProject.Core.DTOs;
using E_CommerceProject.Core.Entities.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public AccountController(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO register)
    {
        if (register == null) return BadRequest("Invalid register data.");
        if (string.IsNullOrWhiteSpace(register.Password)) return BadRequest("Password cannot be empty.");

        // Map DTO to User entity
        var user = _mapper.Map<User>(register);

        // Add user to the repository
        var result = await _userRepository.AddUserAsync(user, register.Password);

        // Handle the result using a helper method for cleaner code
        return HandleRegisterResponse(result);
    }

    private IActionResult HandleRegisterResponse(string result)
    {
        return result switch
        {
            "EmailIsExist" => Conflict("Email already exists."),
            "UserNameIsExist" => Conflict("Username already exists."),
            "Success" => Ok(new { Message = "Registration successful." }),
            "PasswordCannotBeEmpty" => BadRequest("Password cannot be empty."),
            _ => StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
        };
    }
}
