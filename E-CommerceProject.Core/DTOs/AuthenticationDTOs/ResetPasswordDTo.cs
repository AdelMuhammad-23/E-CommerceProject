﻿namespace E_CommerceProject.Core.DTOs.AuthenticationDTOs
{
    public class ResetPasswordDTo
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string confirmPassword { get; set; }
    }
}