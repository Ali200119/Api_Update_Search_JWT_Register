using System;
using Domain.Common;

namespace Domain.Models
{
	public class City: BaseEntity
	{
		public string Name { get; set; }
		public int CountryId { get; set; }
		public Country Country { get; set; }
	}
}