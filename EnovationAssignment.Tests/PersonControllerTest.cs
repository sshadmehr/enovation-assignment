using EnovationAssignment.Exceptions;
using EnovationAssignment.Models;
using EnovationAssignment.Persistance;
using EnovationAssignment.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EnovationAssignment.Tests
{
	public class PersonControllerTest
	{
		[Fact]
		public async void Person_ReturnPersonObject_WithCorrectId()
		{
			using (var context = InitializeDb("test"))
			{
				// Arrange
				PersonService service = new PersonService(context);

				// Act
				var result = await service.Get(1);

				// Assert
				Assert.NotNull(result);
				Assert.IsType<Person>(result);
				Assert.True(result.Name == "Joe");
				Assert.True(result.Gender == Models.Gender.Male);
			}
		}


		[Fact]
		public async void Person_ReturnNotFoundException_WithWrongId()
		{
			using (var context = InitializeDb("test"))
			{
				// Arrange
				PersonService service = new PersonService(context);

				// Act
				// Assert
				await Assert.ThrowsAsync<PersonNotFoundException>(() => service.Get(10));
			}
		}

		private AppDbContext InitializeDb(string name)
		{
			var options = new DbContextOptionsBuilder<AppDbContext>()
					.UseInMemoryDatabase(name)
					.Options;

			var appDbContext = new AppDbContext(options);
			new ContextSeed(appDbContext).Seed();

			return appDbContext;
		}
	}
}
