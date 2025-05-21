using System;

namespace SmartEvent.Core.DTOs
{
    public class ParticipantDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool HasAttended { get; set; }
        public string RegistrationCode { get; set; }
    }
} 