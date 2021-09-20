using EnovationAssignment.Models;
using System;
using System.Text.Json.Serialization;

namespace EnovationAssignment.ViewModels
{
	public class PersonModel
	{
		public int Id { get; set; }
		public string Name { get; set; }

		[JsonIgnore]
		public Gender Gender { get; set; }

		[JsonPropertyName("Gender")]
		public string GenderTitle => Enum.GetName(typeof(Gender), Gender);

	}
}
