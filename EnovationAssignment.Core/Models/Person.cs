namespace EnovationAssignment.Models
{
	public enum Gender
	{
		Male = 1,
		Female = 2,
		Others = 3
	}
	public class Person
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Gender Gender { get; set; }
	}
}
