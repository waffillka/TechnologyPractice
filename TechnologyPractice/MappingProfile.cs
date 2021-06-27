using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace TechnologyPractice
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Organization, OrganizationDto>();
            CreateMap<Contact, ContactDto>();
            CreateMap<ContactCreationDto, Contact>();
            CreateMap<Contact, ContactCreationDto>();
            CreateMap<ContactUpdateDto, Contact>().ReverseMap();

            CreateMap<Organization, OrganizationCreationDto>();
            CreateMap<OrganizationCreationDto, Organization>();
            CreateMap<OrganizationUpdateDto, Organization>().ReverseMap();

            CreateMap<UserForRegistrationDto, User>();
            CreateMap<UserForRegistrationWithoutRoleDto, User>();
            
            CreateMap<UserToReturn, User>();
            CreateMap<User, UserToReturn>();
        }
    }
}
