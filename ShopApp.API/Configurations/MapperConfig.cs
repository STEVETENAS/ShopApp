using AutoMapper;
using ShopApp.API.Dtos;
using ShopApp.API.Models;

namespace ShopApp.API.Mappers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorCreateDto,Author>().ReverseMap();
            CreateMap<AuthorGetDto,Author>().ReverseMap();
            CreateMap<AuthorUpdateDto,Author>().ReverseMap();
        }
    }
}
