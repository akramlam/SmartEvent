using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEvent.Core.Models
{
    public class Participant
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int EventId { get; set; }
        
        public required Event Event { get; set; }
        
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public required string Email { get; set; }
        
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        
        public bool HasAttended { get; set; }
        
        public required string RegistrationCode { get; set; }


    }
} 