using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace prism_trust_foundation.Models
{
	public class Event
	{

		[Key]
		public int EventId { get; set; }
		[Required]
		public string EventName { get; set; } = string.Empty;
		[Required]
		public string EventType { get; set; } = string.Empty;
		[Required]
		public string Description { get; set; } = string.Empty;
		[Required]
		public string EventVenue { get; set; } = string.Empty;
		[Required]
		public string EventDate { get; set; } = string.Empty;
		//public List<VolunteerShift> VolunteerShifts { get; set; }
		[MaxLength(50)]
		public string? ImageURL { get; set; }
		public Boolean IsActive { get; set; } = false;
/*		public TimeOnly Start_Time { get; set; }
		public TimeOnly End_Time { get; set; }*/

		/*public ICollection<Timeslot> Timeslots { get; set; }*/
	}
}