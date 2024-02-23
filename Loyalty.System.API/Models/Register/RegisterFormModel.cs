﻿namespace Loyalty.System.API.Models.Register
{
    public class RegisterFormModel
    {
        public string? GivenName { get; set; }
        public string? SurName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool? EmailConfirmed { get; set; }
    }
}
