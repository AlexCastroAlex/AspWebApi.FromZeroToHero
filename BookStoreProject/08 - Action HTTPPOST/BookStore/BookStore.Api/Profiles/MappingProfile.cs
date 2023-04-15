using AutoMapper;
using BookStore.Api.DTO;
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

            CreateMap<AuthorResponse, Author>()
               .ForMember(m => m.Id, opt => opt.MapFrom(s => s.Id))
               .ForMember(m => m.FirstName, opt => opt.MapFrom(s => s.FirstName))
               .ForMember(m => m.LastName, opt => opt.MapFrom(s => s.LastName))
              .ReverseMap();


            CreateMap<AuthorDtoCreation, Author>()
               .ForMember(m => m.Id, opt => opt.Ignore())
               .ForMember(m => m.FirstName, opt => opt.MapFrom(s => s.FirstName))
               .ForMember(m => m.LastName, opt => opt.MapFrom(s => s.LastName))
              .ReverseMap();



            CreateMap<BookDTO,Book>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.Title, opt => opt.MapFrom(s => s.Title))
                .ForMember(m => m.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(m => m.AuthorId, opt => opt.MapFrom(s => s.AuthorId))
               .ReverseMap();

           

        }
    }
}
