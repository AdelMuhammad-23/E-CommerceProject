using AutoMapper;
using E_CommerceProject.Base;
using E_CommerceProject.Core.DTOs.AuthenticationDTOs;
using E_CommerceProject.Core.Entities.Identity;
using E_CommerceProject.Core.Interfaces;
using E_CommerceProject.Core.Responses;
using E_CommerceProject.Infrastructure.Helper;
using E_CommerceProject.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace E_CommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        private readonly EmailService _emailService;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IUserRepository _userRepository;
        #endregion

        #region constructor
        public AuthenticationController(IMapper mapper,
                                 IAuthenticationRepository authenticationRepository,
                                 SignInManager<User> signInManager,
                                 EmailService emailService,
                                 IUserRepository userRepository,
                                 UserManager<User> userManager)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _authenticationRepository = authenticationRepository ?? throw new ArgumentNullException();
            _signInManager = signInManager ?? throw new ArgumentNullException();
            _userManager = userManager ?? throw new ArgumentNullException();
            _emailService = emailService;
            _userRepository = userRepository ?? throw new ArgumentNullException();

        }
        #endregion


        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                if (string.IsNullOrEmpty(toEmail))
                    return BadRequest("Recipient email is required.");

                await _emailService.SendEmailAsync(toEmail, subject, body);
                return Ok("Email sent successfully.");
            }
            catch (SmtpException ex)
            {
                return StatusCode(500, $"SMTP Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
                return BadRequest("Invalid confirmation request.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            var decodedToken = Uri.UnescapeDataString(token);

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (!result.Succeeded)
                return BadRequest("Email confirmation failed.");

            return Ok("Email confirmed successfully.");
        }





        [HttpPost("RefreshToken")]
        public async Task<Responses<JwtAuthResult>> RefreshToken([FromBody] RefreshTokenDTO refreshToken)
        {
            var jwtToken = _authenticationRepository.ReadJwtToken(refreshToken.AccessToken);
            var userIdAndExpireDate = await _authenticationRepository.ValidateDetails(jwtToken, refreshToken.AccessToken, refreshToken.RefreshToken);
            switch (userIdAndExpireDate)
            {
                case ("Algorithms is not correct", null):
                    return Unauthorized<JwtAuthResult>("Algorithms Is Not Correct");
                case (("Token is not expired", null)):
                    return BadRequest<JwtAuthResult>("Token Is Not Expired");
                case ("Refresh Token is Not Found", null):
                    return NotFound<JwtAuthResult>("Refresh Token Is Not Found");
                case ("Refresh Token is expired", null):
                    return Unauthorized<JwtAuthResult>("Refresh Token Is Expired");
            }

            var (userId, expiryDate) = userIdAndExpireDate;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound<JwtAuthResult>();
            }
            var result = await _authenticationRepository.GetNewRefreshToken(user, jwtToken, expiryDate, refreshToken.RefreshToken);
            return Success(result);
        }
    }
}
