using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnologyPractice
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Organization, OrganizationDto>();
            CreateMap<Contact, ContactDto>();
            CreateMap<Organization, OrganizationCreationDto>();
            CreateMap<OrganizationCreationDto, Organization>();
            CreateMap<OrganizationUpdateDto, Organization>().ReverseMap();
        }
    }
}
