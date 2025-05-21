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
        private readonly IParticipantRepository _ParticipantRepository;

        public RegistrationService(IEventRepository eventRepository, IParticipantRepository ParticipantRepository)
        {
            _eventRepository = eventRepository;
            _ParticipantRepository = ParticipantRepository;
        }

        public async Task<ParticipantDto> RegisterForEventAsync(int eventId, RegistrationDto registrationDto)
        {
            // Check if event exists
            var eventEntity = await _eventRepository.GetEventByIdAsync(eventId);
            if (eventEntity == null)
                throw new Exception($"Event with ID {eventId} not found");

            // Check if Participant is already registered
            bool isAlreadyRegistered = await _ParticipantRepository.ParticipantExistsAsync(eventId, registrationDto.Email);
            if (isAlreadyRegistered)
                throw new Exception("You are already registered for this event");

            // Check if event is full
            bool isFull = await IsEventFullAsync(eventId);
            if (isFull)
                throw new Exception("Event has reached maximum capacity");

            // Generate a unique registration code
            string registrationCode = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
            
            // Create the Participant
            var Participant = new Participant
            {
                EventId = eventId,
                Name = registrationDto.Name,
                Email = registrationDto.Email,
                RegistrationDate = DateTime.Now,
                HasAttended = false,
                RegistrationCode = registrationCode,
                Event = eventEntity
            };

            var createdParticipant = await _ParticipantRepository.CreateParticipantAsync(Participant);
            
            return MapToParticipantDto(createdParticipant);
        }

        public async Task<IEnumerable<ParticipantDto>> GetEventParticipantsAsync(int eventId)
        {
            // Check if event exists
            bool eventExists = await _eventRepository.EventExistsAsync(eventId);
            if (!eventExists)
                throw new Exception($"Event with ID {eventId} not found");

            var Participants = await _ParticipantRepository.GetParticipantsByEventIdAsync(eventId);
            return Participants.Select(MapToParticipantDto);
        }

        public async Task<bool> IsEventFullAsync(int eventId)
        {
            var eventEntity = await _eventRepository.GetEventByIdAsync(eventId);
            if (eventEntity == null)
                throw new Exception($"Event with ID {eventId} not found");

            // If maxParticipants is 0, it means unlimited
            if (eventEntity.MaxParticipants <= 0)
                return false;

            int currentParticipants = await _ParticipantRepository.GetEventParticipantCountAsync(eventId);
            return currentParticipants >= eventEntity.MaxParticipants;
        }

        private ParticipantDto MapToParticipantDto(Participant Participant)
        {
            return new ParticipantDto
            {
                Id = Participant.Id,
                EventId = Participant.EventId,
                Name = Participant.Name,
                Email = Participant.Email,
                RegistrationDate = Participant.RegistrationDate,
                HasAttended = Participant.HasAttended,
                RegistrationCode = Participant.RegistrationCode,
                EventTitle = Participant.Event?.Title
            };
        }
    }
} 