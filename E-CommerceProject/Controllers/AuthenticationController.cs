using AutoMapper;
using E_CommerceProject.Base;
using E_CommerceProject.Core.DTOs.AuthenticationDTOs;
using E_CommerceProject.Core.Entities.Identity;
using E_CommerceProject.Core.Interfaces;
using E_CommerceProject.Core.Responses;
using E_CommerceProject.Infrastructure.Helper;
using Microsoft.AspNetCore.Identity;
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationRepository _authenticationRepository;
        #endregion

        #region constructor
        public AuthenticationController(IMapper mapper,
                                 IAuthenticationRepository authenticationRepository,
                                 SignInManager<User> signInManager,
                                 UserManager<User> userManager)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _authenticationRepository = authenticationRepository ?? throw new ArgumentNullException();
            _signInManager = signInManager ?? throw new ArgumentNullException();
            _userManager = userManager ?? throw new ArgumentNullException();
        }
        #endregion




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
