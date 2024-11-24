using AutoMapper;
using E_CommerceProject.Base;
using E_CommerceProject.Core.DTOs;
using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Entities.Identity;
using E_CommerceProject.Core.Interfaces;
using E_CommerceProject.Core.Responses;
using E_CommerceProject.Infrastructure.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AccountController : AppControllerBase
{
    #region Fields
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IAuthenticationRepository _authenticationRepository;
    #endregion

    #region constructor
    public AccountController(IMapper mapper,
                             IUserRepository userRepository,
                             IAuthenticationRepository authenticationRepository,
                             SignInManager<User> signInManager,
                             UserManager<User> userManager)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _authenticationRepository = authenticationRepository ?? throw new ArgumentNullException();
        _signInManager = signInManager ?? throw new ArgumentNullException();
        _userManager = userManager ?? throw new ArgumentNullException();
    }
    #endregion

    #region Endpoints
    [AllowAnonymous]
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
    [AllowAnonymous]
    [HttpPost("SignIn")]
    public async Task<Responses<JwtAuthResult>> SignIn(SignInDTO signIn)
    {
        //Check if user is exist or not
        var user = await _userManager.FindByNameAsync(signIn.UserName);
        //Return The UserName Not Found
        if (user == null) return BadRequest<JwtAuthResult>("User Name Is Not Exist");
        //try To Sign in 
        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, signIn.Password, false);
        //if Failed Return Passord is wrong
        if (!signInResult.Succeeded) return BadRequest<JwtAuthResult>("Password Is Not Correct");


        //Generate Token
        var result = await _authenticationRepository.GetJwtToken(user);
        //return Token 
        return Success(result);
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
