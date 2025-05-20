using System;
using System.ComponentModel.DataAnnotations;

namespace SmartEvent.Core.DTOs
{
    public class EventCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }
        
        [StringLength(200)]
        public string Location { get; set; }
        
        [Range(1, 1000)]
        public int MaxAttendees { get; set; }
        
        public bool IsPublic { get; set; } = true;
        
        public string OrganizerId { get; set; }
    }
} 