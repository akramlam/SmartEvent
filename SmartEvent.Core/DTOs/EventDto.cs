using System;

namespace SmartEvent.Core.DTOs
{
    public class EventDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required string Location { get; set; }
        public int MaxParticipants { get; set; }
        public int CurrentParticipants { get; set; }
        public bool IsPublic { get; set; }
        public required string OrganizerId { get; set; }
        public required string OrganizerName { get; set; }
    }
} 