using AutoMapper;

namespace EnovationAssignment
{
	public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      // Add as many of these lines as you need to map your objects
      CreateMap<Models.Person, ViewModels.PersonModel>();
      CreateMap<ViewModels.PersonAddModel, Models.Person>();
      CreateMap<ViewModels.PersonEditModel, Models.Person>();
    }
  }
}
