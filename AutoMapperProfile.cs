using AutoMapper;
using MoviesProject.Application;
using MoviesProject.Application.Commands;
using MoviesProject.DomainLayer.Aggregates;
using MoviesProject.DomainLayer.ValueObjects;

namespace MoviesProject
{
    public class AutoMapperProfile : Profile
    {
       

        public AutoMapperProfile()
        {
            CreateMap<GenreDTO, Genre>().ReverseMap();
            CreateMap<LanguageDTO, Language>().ReverseMap();

            CreateMap<MovieCreateCommand, Movie>()
                .ForMember(dest => dest.Genre, src => src.MapFrom(src => src.GenreDTO))
                .ForMember(dest => dest.Language, src => src.MapFrom(src => src.LanguageDTO));

            CreateMap<Movie, MovieDTO>();
            CreateMap<ActorCreateCommand, Actor>();
            CreateMap<Actor, ActorDTO>();
            CreateMap<Movie, MovieDTO>()
                .ForMember(dest => dest.GenreDTO, src => src.MapFrom(src => src.Genre))
                .ForMember(dest => dest.LanguageDTO, src => src.MapFrom(src => src.Language)); ;
        }
    }
}
