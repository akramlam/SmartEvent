using SmartEvent.Core.DTOs;
using SmartEvent.Core.Interfaces;
using SmartEvent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEvent.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IAttendeeRepository _attendeeRepository;

        public RegistrationService(IEventRepository eventRepository, IAttendeeRepository attendeeRepository)
        {
            _eventRepository = eventRepository;
            _attendeeRepository = attendeeRepository;
        }

        public async Task<AttendeeDto> RegisterForEventAsync(int eventId, RegistrationDto registrationDto)
        {
            // Check if event exists
            var eventEntity = await _eventRepository.GetEventByIdAsync(eventId);
            if (eventEntity == null)
                throw new Exception($"Event with ID {eventId} not found");

            // Check if attendee is already registered
            bool isAlreadyRegistered = await _attendeeRepository.AttendeeExistsAsync(eventId, registrationDto.Email);
            if (isAlreadyRegistered)
                throw new Exception("You are already registered for this event");

            // Check if event is full
            bool isFull = await IsEventFullAsync(eventId);
            if (isFull)
                throw new Exception("Event has reached maximum capacity");

            // Generate a unique registration code
            string registrationCode = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
            
            // Create the attendee
            var attendee = new Attendee
            {
                EventId = eventId,
                Name = registrationDto.Name,
                Email = registrationDto.Email,
                RegistrationDate = DateTime.Now,
                HasAttended = false,
                RegistrationCode = registrationCode,
                Event = eventEntity
            };

            var createdAttendee = await _attendeeRepository.CreateAttendeeAsync(attendee);
            
            return MapToAttendeeDto(createdAttendee);
        }

        public async Task<IEnumerable<AttendeeDto>> GetEventAttendeesAsync(int eventId)
        {
            // Check if event exists
            bool eventExists = await _eventRepository.EventExistsAsync(eventId);
            if (!eventExists)
                throw new Exception($"Event with ID {eventId} not found");

            var attendees = await _attendeeRepository.GetAttendeesByEventIdAsync(eventId);
            return attendees.Select(MapToAttendeeDto);
        }

        public async Task<bool> IsEventFullAsync(int eventId)
        {
            var eventEntity = await _eventRepository.GetEventByIdAsync(eventId);
            if (eventEntity == null)
                throw new Exception($"Event with ID {eventId} not found");

            // If maxAttendees is 0, it means unlimited
            if (eventEntity.MaxAttendees <= 0)
                return false;

            int currentAttendees = await _attendeeRepository.GetEventAttendeeCountAsync(eventId);
            return currentAttendees >= eventEntity.MaxAttendees;
        }

        private AttendeeDto MapToAttendeeDto(Attendee attendee)
        {
            return new AttendeeDto
            {
                Id = attendee.Id,
                EventId = attendee.EventId,
                Name = attendee.Name,
                Email = attendee.Email,
                RegistrationDate = attendee.RegistrationDate,
                HasAttended = attendee.HasAttended,
                RegistrationCode = attendee.RegistrationCode,
                EventTitle = attendee.Event?.Title
            };
        }
    }
} 