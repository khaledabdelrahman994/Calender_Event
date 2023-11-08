namespace Event_Calender.Services
{
    public interface IGoogleCalenderSetting
    {
        string ApplicationName { get; set; }
        string CalendarId { get; set; }
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        string[] Scope { get; set; }
        string User { get; set; }
    }
}