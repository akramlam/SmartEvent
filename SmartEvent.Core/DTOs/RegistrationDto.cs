using System;
using System.ComponentModel.DataAnnotations;

namespace SmartEvent.Core.DTOs
{
    public class RegistrationDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
    }
} 