using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Event_Calender.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Event_Calender.Controllers
{
    [Route("api/Events")]
    [ApiController]
    [Authorize]
    public class EventController : ControllerBase
    {
        private readonly IGoogleCalendarService _googleCalendarService;

        public EventController(IGoogleCalendarService googleCalendarService)
        {
            _googleCalendarService = googleCalendarService;
        }
        [HttpPost]
        public async Task <ActionResult> CreateEvent(Event request, CancellationToken cancellationToken)
        {
            Console.WriteLine(request);
            try
            {
                // Ensure the required fields are present and formatted correctly in the @event object.
                if (string.IsNullOrEmpty(request.Summary) || request.Start == null || request.End == null)
                {
                    return BadRequest("Required fields are missing or not properly formatted.");
                }

                // Validate the date and time to ensure the event is not in the past or on Friday/Saturday.
                DateTime now = DateTime.Now;
                if (request.Start.DateTime < now || request.End.DateTime < now || request.Start.DateTime.Value.DayOfWeek == DayOfWeek.Friday || request.Start.DateTime.Value.DayOfWeek == DayOfWeek.Saturday)
                {
                    return BadRequest("Invalid event date or time.");
                }
               

                return Ok(await _googleCalendarService.CreateEvent(request , cancellationToken));
            }
            catch (Exception ex)
            {
                // Handle any errors that might occur during the API call.
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }



        }
    }

