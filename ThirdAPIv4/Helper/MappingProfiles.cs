using AutoMapper;
using ThirdAPI.Dtos;
using ThirdAPI.Models;

namespace ThirdAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Book, BookDto>();
            CreateMap<Author, AuthorDto>();
            CreateMap<Publisher, PublisherDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<BookPublisher, BookPublisherDto>();
            CreateMap<BookDto, Book>();
            CreateMap<AuthorDto, Author>();
            CreateMap<PublisherDto, Publisher>();
            CreateMap<ReviewDto, Review>();
            CreateMap<BookPublisherDto, BookPublisher>();
        }
    }
}