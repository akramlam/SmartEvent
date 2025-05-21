using Microsoft.AspNetCore.Mvc;
using SmartEvent.Core.Interfaces;
using SmartEvent.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartEvent.RegistrationService.Controllers
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
            catch (Exception ex)
            {
                if (ex.Message.Contains("already registered"))
                    return Conflict(ex.Message);
                if (ex.Message.Contains("maximum capacity"))
                    return BadRequest(ex.Message);
                if (ex.Message.Contains("not found"))
                    return NotFound(ex.Message);

                return StatusCode(500, $"Internal server error: {ex.Message}");
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
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                    return NotFound(ex.Message);

                return StatusCode(500, $"Internal server error: {ex.Message}");
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
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                    return NotFound(ex.Message);

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 