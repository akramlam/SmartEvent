using SmartEvent.Core.DTOs;
using SmartEvent.Core.Interfaces;
using SmartEvent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEvent.Services
{
    public class EventService : SmartEvent.Core.Interfaces.IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IParticipantRepository _ParticipantRepository;

        public EventService(IEventRepository eventRepository, IParticipantRepository ParticipantRepository)
        {
            _eventRepository = eventRepository;
            _ParticipantRepository = ParticipantRepository;
        }

        public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
        {
            var events = await _eventRepository.GetAllEventsAsync();
            return await MapEventsToDtosAsync(events);
        }

        public async Task<EventDto> GetEventByIdAsync(int id)
        {
            var eventEntity = await _eventRepository.GetEventByIdAsync(id);
            if (eventEntity == null)
                return null;

            return await MapEventToDtoAsync(eventEntity);
        }

        public async Task<EventDto> CreateEventAsync(EventCreateDto eventDto)
        {
            var eventEntity = new Event
            {
                Title = eventDto.Title,
                Description = eventDto.Description,
                StartDate = eventDto.StartDate,
                EndDate = eventDto.EndDate,
                Location = eventDto.Location,
                MaxParticipants = eventDto.MaxParticipants,
                IsPublic = eventDto.IsPublic,
                OrganizerId = eventDto.OrganizerId,
                CreatedAt = DateTime.Now
            };

            var createdEvent = await _eventRepository.CreateEventAsync(eventEntity);
            return await MapEventToDtoAsync(createdEvent);
        }

        public async Task<EventDto> UpdateEventAsync(int id, EventCreateDto eventDto)
        {
            var existingEvent = await _eventRepository.GetEventByIdAsync(id);
            if (existingEvent == null)
                return null;

            existingEvent.Title = eventDto.Title;
            existingEvent.Description = eventDto.Description;
            existingEvent.StartDate = eventDto.StartDate;
            existingEvent.EndDate = eventDto.EndDate;
            existingEvent.Location = eventDto.Location;
            existingEvent.MaxParticipants = eventDto.MaxParticipants;
            existingEvent.IsPublic = eventDto.IsPublic;
            existingEvent.UpdatedAt = DateTime.Now;

            var updatedEvent = await _eventRepository.UpdateEventAsync(existingEvent);
            return await MapEventToDtoAsync(updatedEvent);
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            return await _eventRepository.DeleteEventAsync(id);
        }

        private async Task<EventDto> MapEventToDtoAsync(Event eventEntity)
        {
            var ParticipantCount = await _ParticipantRepository.GetEventParticipantCountAsync(eventEntity.Id);
            
            return new EventDto
            {
                Id = eventEntity.Id,
                Title = eventEntity.Title,
                Description = eventEntity.Description,
                StartDate = eventEntity.StartDate,
                EndDate = eventEntity.EndDate,
                Location = eventEntity.Location,
                MaxParticipants = eventEntity.MaxParticipants,
                CurrentParticipants = ParticipantCount,
                IsPublic = eventEntity.IsPublic,
                OrganizerId = eventEntity.OrganizerId,
                OrganizerName = "Organizer" // In a real app, you'd get this from a user service
            };
        }

        private async Task<IEnumerable<EventDto>> MapEventsToDtosAsync(IEnumerable<Event> events)
        {
            var result = new List<EventDto>();
            
            foreach (var eventEntity in events)
            {
                result.Add(await MapEventToDtoAsync(eventEntity));
            }
            
            return result;
        }
    }
} 