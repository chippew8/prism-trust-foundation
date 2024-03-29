﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using prism_trust_foundation.Models;

namespace prism_trust_foundation.Services
{
	public class EventService
	{
		private readonly AuthDbContext _context;
		public EventService(AuthDbContext context)
		{
			_context = context;
		}

		public List<Event> GetAll()
		{
			return _context.Event.OrderBy(d => d.EventName).ToList();
		}
		public Event? GetEventById(int Id)
		{
			Event? myEvent = _context.Event.FirstOrDefault(x => x.EventId.Equals(Id));
			return myEvent;
		}

		public Event? GetEventByName(string Name)
		{
			Event? myEvent = _context.Event.FirstOrDefault(x => x.EventName.Equals(Name));
			return myEvent;
		}

		public void AddEvent(Event myEvent)
		{
			_context.Event.Add(myEvent);
			_context.SaveChanges();
		}

		public void UpdateEvent(Event myEvent)
		{
			_context.Event.Update(myEvent);
			_context.SaveChanges();
		}

	}
}