using EnovationAssignment.Exceptions;
using EnovationAssignment.Models;
using EnovationAssignment.Persistance;
using EnovationAssignment.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace EnovationAssignment.Tests
{
	public class PersonControllerTest : IDisposable
	{
		private const int _invalidId = 100;
		private const int _validId = 3;
		private readonly PersonService _service;
		private readonly AppDbContext _context;
		public PersonControllerTest()
		{
			_context = InitializeDb("test");
			_service = new PersonService(_context);
		}
		[Fact]
		public async void Person_ReturnPersonObject_WithCorrectId()
		{
			// Arrange
			var expectedName = "Lili";
			var expectedGender = Gender.Female;

			// Act
			var result = await _service.Get(_validId);

			// Assert
			Assert.NotNull(result);
			Assert.IsType<Person>(result);
			Assert.True(result.Name == expectedName);
			Assert.True(result.Gender == expectedGender);
		}
		[Fact]
		public async void Person_ReturnNotFoundException_WithWrongId()
		{
			// Act
			// Assert
			await Assert.ThrowsAsync<PersonNotFoundException>(() => _service.Get(_invalidId));

		}
		[Fact]
		public async void Person_ReturnInvalidInputException_AddWithEmptyName()
		{
			var person = new Person
			{
				Name = string.Empty,
				Gender = Models.Gender.Male,
			};

			// Act
			// Assert
			await Assert.ThrowsAsync<InvalidInputException>(() => _service.Add(person));
		}
		[Fact]
		public async void Person_ReturnNewAddedPerson_AddNewPerson()
		{
			var expectedName = "Janet";
			var expectedGender = Gender.Female;

			var person = new Person
			{
				Name = expectedName,
				Gender = expectedGender,
			};
			// Act
			var newPerson = await _service.Add(person);
			var result = await _service.Get(newPerson.Id);

			// Assert
			Assert.NotNull(newPerson);
			Assert.IsType<Person>(result);
			Assert.Equal(result, newPerson);
			Assert.True(result.Name == expectedName);
			Assert.True(result.Gender == expectedGender);
		}
		[Fact]
		public async void Person_ReturnTrue_EditExistingPerson()
		{

			var person = new Person
			{
				Id = _validId,
				Name = "Jane",
				Gender = Models.Gender.Female,
			};
			// Act
			var result = await _service.Update(person);
			var editedPerson = await _service.Get(person.Id);

			// Assert
			Assert.True(result);
			Assert.Equal(person.Name, editedPerson.Name);
			Assert.Equal(person.Gender, editedPerson.Gender);

		}
		[Fact]
		public async void Person_ReturnInvalidInputException_EditWithEmptyName()
		{

			var person = new Person
			{
				Id = _validId,
				Name = "",
				Gender = Models.Gender.Female,
			};

			// Act
			// Assert
			await Assert.ThrowsAsync<InvalidInputException>(() => _service.Update(person));
			Assert.NotEqual("", (await _service.Get(_validId)).Name);
		}
		[Fact]
		public async void Person_ReturnNotFoundException_EditNotExistingPerson()
		{
			var person = new Person
			{
				Id = _invalidId,
				Name = "Harry",
				Gender = Models.Gender.Male,
			};
			// Act
			// Assert
			await Assert.ThrowsAsync<PersonNotFoundException>(() => _service.Update(person));
		}
		[Fact]
		public async void Person_RetureTrue_RemoveWithExistingId()
		{
			// Act
			var result = _service.Delete(_validId);

			// Assert
			Assert.True(result.Result);
			await Assert.ThrowsAsync<PersonNotFoundException>(() => _service.Get(_validId));
		}
		[Fact]
		public async void Person_RetureNotfoundException_RemoveWithNotExistingId()
		{
			// Act
			// Assert
			await Assert.ThrowsAsync<PersonNotFoundException>(() => _service.Delete(_invalidId));
		}
		[Fact]
		public async void Person_Search_WithNoCriteria()
		{
			var totalRecordsInDb = 3;
			var searchResult = await _service.Search(null, null);
			Assert.Equal(totalRecordsInDb, searchResult.Count());
		}
		[Fact]
		public async void Person_Search_SpecificGender()
		{
			// Arrange
			var gender = Gender.Male;
			var totalMaleRecords = 2;

			// Act
			var searchResult = await _service.Search(null, gender);

			// Assert
			foreach (var person in searchResult)
			{
				Assert.Equal(gender, person.Gender);
			}

			Assert.True(totalMaleRecords == searchResult.Count());
		}
		[Fact]
		public async void Person_Search_ReturnNoRecords()
		{
			// Arrange
			var gender = Gender.Others;
			var expectedRecordsCount = 0;

			// Act
			var searchResult = await _service.Search(null, gender);

			// Assert
			
			Assert.True(expectedRecordsCount == searchResult.Count());
		}
		[Fact]
		public async void Person_Search_SimilarNames()
		{
			// Arrange
			var keyword = "li";
			var expectedRecordsCount = 1;

			// Act
			var searchResult = await _service.Search(keyword, null);

			// Assert
			Assert.True(expectedRecordsCount == searchResult.Count());
		}
		[Fact]
		public async void Person_Search_CaseInsensitiveNames()
		{
			// Arrange
			var keyword = "jack";
			var expectedRecordsCount = 1;

			// Act
			var searchResult = await _service.Search(keyword, null);

			// Assert
			Assert.True(expectedRecordsCount == searchResult.Count());
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

		public void Dispose()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
		}
	}
}
