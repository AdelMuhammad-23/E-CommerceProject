using AutoMapper;
using E_CommerceProject.Core.DTOs;
using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Entities.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    #region Fields
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    #endregion

    #region constructor
    public AccountController(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }
    #endregion

    #region Endpoints
    [HttpPost("SignUp")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO register)
    {
        if (register == null) return BadRequest("Invalid register data.");
        if (string.IsNullOrWhiteSpace(register.Password)) return BadRequest("Password cannot be empty.");

        var user = _mapper.Map<User>(register);

        // Call AddUserAsync
        var result = await _userRepository.AddUserAsync(user, register.Password);

        // If result is not a known case, pass it as error description
        return result switch
        {
            "EmailIsExist" or "UserNameIsExist" or "Success" or "PasswordCannotBeEmpty"
                => HandleRegisterResponse(result),
            _ => HandleRegisterResponse(result, result) // Pass the actual error description for unknown cases
        };
    }

    [HttpPost("AddAddress")]
    public async Task<IActionResult> AddAddress(AddAddressDTO addressDto)
    {
        var address = _mapper.Map<Address>(addressDto);

        var user = await _userRepository.GetUserByIdAsync(addressDto.UserId);
        if (user == null) return BadRequest("User is not found");

        address.UserId = user.Id;
        var result = await _userRepository.AddAddressUserAsync(address);

        return result == "Success"
         ? Ok(new { Message = "Address added successfully." })
         : BadRequest("Failed to add address.");
    }



    #endregion

    #region Method Helper
    private IActionResult HandleRegisterResponse(string result, string? errorDescription = null)
    {
        return result switch
        {
            "EmailIsExist" => Conflict("Email already exists."),
            "UserNameIsExist" => Conflict("Username already exists."),
            "Success" => Ok(new { Message = "Registration successful." }),
            "PasswordCannotBeEmpty" => BadRequest("Password cannot be empty."),
            _ => StatusCode(StatusCodes.Status500InternalServerError,
                            new { Message = "An unexpected error occurred.", Error = errorDescription ?? "Unknown error." })
        };
    }

    #endregion
}
