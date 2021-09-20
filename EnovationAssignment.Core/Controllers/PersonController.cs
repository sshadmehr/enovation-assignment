using AutoMapper;
using EnovationAssignment.Auth;
using EnovationAssignment.Models;
using EnovationAssignment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnovationAssignment.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[BasicAuthorization]
	public class PersonController: ControllerBase
	{
		private readonly IPersonService _personService;
		private readonly IMapper _mapper;
		public PersonController(IPersonService personService, IMapper mapper)
		{
			this._personService = personService;
			this._mapper = mapper;
		}

		[HttpGet]
		[Route("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ViewModels.PersonModel> Get(int id)
		{
			var model = await this._personService.Get(id);
			return _mapper.Map<ViewModels.PersonModel>(model);
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IEnumerable<ViewModels.PersonModel>> Filter([FromQuery]string? name, [FromQuery]Gender? gender)
		{
			var model = (await _personService.Search(name, gender)).ToList();
			return _mapper.Map<List<ViewModels.PersonModel>>(model);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<Person> Add([FromBody]ViewModels.PersonAddModel person)
		{
			var model = _mapper.Map<Person>(person);
			return await _personService.Add(model);
		}

		[HttpDelete]
		[Route("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<bool> Delete(int id)
		{
			return await _personService.Delete(id);
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<bool> Update([FromBody]ViewModels.PersonEditModel person)
		{
			var model = _mapper.Map<Person>(person);
			return await _personService.Update(model);
		}
	}
}
