﻿using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using System.Text;
using System.Web;
using Event_Calender.DTO;

namespace Event_Calender.Services
{
    public class GoogleCalendarService : IGoogleCalendarService
    {
        private readonly IGoogleCalenderSetting _settings;

        public GoogleCalendarService(IGoogleCalenderSetting settings)
        {
            _settings = settings;
        }

        public async Task<Event> CreateEvent(Event request , CancellationToken cancellationToken)
        {
            UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets()
                {
                    ClientId = _settings.ClientId,
                    ClientSecret = _settings.ClientSecret
                },
                _settings.Scope,
                _settings.User,new CancellationToken());

            var services = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _settings.ApplicationName,
            });

            var eventRequest = services.Events.Insert(request, _settings.CalendarId);
            var requestCreate = await eventRequest.ExecuteAsync();
            return requestCreate;
        }

   
    }
}
