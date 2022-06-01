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

            CreateMap<Book, BookDto>()
                .ForMember(p => p.AuthorName, b => b.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
                .ReverseMap();
            CreateMap<BookCreateDto, Book>().ReverseMap();
            CreateMap<BookUpdateDto, Book>().ReverseMap();
            CreateMap<BookWithAuthorDto, Book>().ReverseMap();

            CreateMap<UserDto, ApiUser>().ReverseMap();
            CreateMap<UserLoginDto, ApiUser>().ReverseMap();

        }
    }
}
