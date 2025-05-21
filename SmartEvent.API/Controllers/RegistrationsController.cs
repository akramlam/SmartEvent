using Microsoft.AspNetCore.Mvc;
using SmartEvent.Core.DTOs;
using SmartEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartEvent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationsController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        // POST: api/registrations/events/5
        [HttpPost("events/{eventId}")]
        public async Task<IActionResult> RegisterForEvent(int eventId, [FromBody] RegistrationDto registrationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var Participant = await _registrationService.RegisterForEventAsync(eventId, registrationDto);
                return CreatedAtAction(nameof(GetEventParticipants), new { eventId }, Participant);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("already registered"))
                    return Conflict(e.Message);
                if (e.Message.Contains("maximum capacity"))
                    return BadRequest(e.Message);
                if (e.Message.Contains("not found"))
                    return NotFound(e.Message);

                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        // GET: api/registrations/events/5/Participants
        [HttpGet("events/{eventId}/Participants")]
        public async Task<IActionResult> GetEventParticipants(int eventId)
        {
            try
            {
                var Participants = await _registrationService.GetEventParticipantsAsync(eventId);
                return Ok(Participants);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("not found"))
                    return NotFound(e.Message);

                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        // GET: api/registrations/events/5/full
        [HttpGet("events/{eventId}/full")]
        public async Task<IActionResult> IsEventFull(int eventId)
        {
            try
            {
                bool isFull = await _registrationService.IsEventFullAsync(eventId);
                return Ok(isFull);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("not found"))
                    return NotFound(e.Message);

                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }
    }
} 