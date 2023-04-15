using AutoMapper;
using BookStore.Api.Responses;
using BookStore.Repository.Models;

namespace BookStore.Api.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookReponseFull>()
                .ForMember(m => m.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(m => m.Title, opt => opt.MapFrom(s => s.Title))
                .ForMember(m => m.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(m => m.Author, opt => opt.MapFrom(s => s.Author))
               .ReverseMap();

        }
    }
}
