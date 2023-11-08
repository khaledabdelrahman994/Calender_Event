using Event_Calender.DTO;
using Google.Apis.Calendar.v3.Data;

namespace Event_Calender.Services
{
    public interface IGoogleCalendarService
    {
        Task<Event> CreateEvent(Event request, CancellationToken cancellationToken);
    }
}