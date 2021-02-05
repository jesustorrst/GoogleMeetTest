using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;

using System.IO;

namespace GoogleMeetTest
{
    class Program
    {
        public static void AddEvent()
        {
            CalendarService service;
            GoogleCredential credential;

            try
            {
                string[] scopes = new string[] { CalendarService.Scope.Calendar };
                using (var stream = new FileStream(@"C:\Prueba\meet.json", FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(scopes);
                }
                service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential
                });


                Event calendarEvent = new Event();
                DateTime start = DateTime.Now;
                calendarEvent.Kind = "";
                calendarEvent.Summary = "prueba";
                calendarEvent.Status = "confirmed";
                calendarEvent.Visibility = "public";
                calendarEvent.Description = "prueba";

                calendarEvent.Creator = new Event.CreatorData
                {
                    Email = "email@example.com", //email@example.com
                    Self = true
                };

                calendarEvent.Organizer = new Event.OrganizerData
                {
                    Email = "email@example.com",
                    Self = true
                };

                calendarEvent.Start = new EventDateTime
                {
                    DateTime = start,
                    TimeZone = "America/Mexico_City"
                };

                calendarEvent.End = new EventDateTime
                {
                    DateTime = start.AddHours(1),
                    TimeZone = "America/Mexico_City"
                };

                calendarEvent.Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=1" };
                calendarEvent.Sequence = 0;
                calendarEvent.HangoutLink = "";

                calendarEvent.ConferenceData = new ConferenceData
                {
                    CreateRequest = new CreateConferenceRequest
                    {
                        RequestId = "1234abcdef",
                        ConferenceSolutionKey = new ConferenceSolutionKey
                        {
                            Type = "hangoutsMeet"
                        },
                        Status = new ConferenceRequestStatus
                        {
                            StatusCode = "success"
                        }
                    },
                    EntryPoints = new List<EntryPoint>
                    {
                        new EntryPoint
                        {
                            EntryPointType = "video",
                            Uri = "",
                            Label = ""
                        }
                    },
                    ConferenceSolution = new ConferenceSolution
                    {
                        Key = new ConferenceSolutionKey
                        {
                            Type = "hangoutsMeet"
                        },
                        Name = "Google Meet",
                        IconUri = ""
                    },
                    ConferenceId = ""
                };

                //calendarEvent.EventType = "default";


                EventsResource.InsertRequest request = service.Events.Insert(calendarEvent, "email1@example.com");
                request.ConferenceDataVersion = 0;
                Event createEvent = request.Execute();
                string url = createEvent.HangoutLink;
            }
            catch (Exception ex)
            {

            }
        }
        static void Main(string[] args)
        {
            AddEvent();
        }

    }
}
