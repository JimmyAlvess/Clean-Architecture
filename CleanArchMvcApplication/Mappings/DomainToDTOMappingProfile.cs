using AutoMapper;
using CleanArchMvc.Domain.Entities;
using CleanArchMvcApplication.Dtos;

namespace CleanArchMvcApplication.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProcutDTO>().ReverseMap();
        }
    }
}
