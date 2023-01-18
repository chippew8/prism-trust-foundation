﻿using prism_trust_foundation.Models;

namespace prism_trust_foundation.Services
{
    public class EventService
    {
        private static List<Event> AllEvents = new()
        {
            new Event {EventId = "1", EventName = "Event1", Description = "Example Event", EventDate = "01-01-2023", EventType = "Donation Drive", EventVenue = "Nanyang Polytechnic"},
            new Event {EventId = "2", EventName = "Event2", Description = "Second Example Event", EventDate = "02-01-2023", EventType = "Recycling Drive", EventVenue = "Republic Polytechnic"},
            new Event {EventId = "3", EventName = "Event3", Description = "Third Example Event", EventDate = "03-01-2023", EventType = "Recycling Drive", EventVenue = "Temasek Polytechnic"},
            new Event {EventId = "4", EventName = "Event4", Description = "Fourth Example Event", EventDate = "04-01-2023", EventType = "Donation Drive", EventVenue = "Ngee Ann Polytechnic"}

        };

        public List<Event> GetAll()
        {
            return AllEvents.OrderBy(d => d.EventName).ToList();
        }
        public Event? GetEventById(string Id)
        {
            Event? myEvent = AllEvents.FirstOrDefault(x => x.EventId.Equals(Id));
            return myEvent;
        }

        public void AddEvent(Event myEvent)
        {
            AllEvents.Add(myEvent);
        }

        
    }
}
