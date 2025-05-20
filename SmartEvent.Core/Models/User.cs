using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEvent.Core.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }
        
        [Required]
        [StringLength(50)]
        public required string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public required string Email { get; set; }
        
        [Required]
        public required string PasswordHash { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? LastLogin { get; set; }
        
        // Navigation property
        public ICollection<Event> OrganizedEvents { get; set; } = new List<Event>();
    }
} 