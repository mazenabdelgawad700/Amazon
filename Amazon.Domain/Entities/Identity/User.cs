﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Amazon.Domain.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
    }
}
