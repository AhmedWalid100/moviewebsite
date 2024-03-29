﻿using AutoMapper;
using MoviesProject.Application.Commands;
using MoviesProject.Application.ResponseModels;
using MoviesProject.DomainLayer.Aggregates;
using MoviesProject.DomainLayer.Entity;
using MoviesProject.DomainLayer.Interfaces;
using MoviesProject.DomainLayer.ValueObjects;

namespace MoviesProject.Application
{
    public class MovieCommandHandler : IMovieCommandHandler
    {
        public IMovieRepository _movierepository;
        public IMapper _mapper;
        public IMovieActorRepository _movieActorRepository;
        public MovieCommandHandler(IMovieRepository movierepository, IMapper mapper
            , IMovieActorRepository movieActorRepository)
        {
            _movierepository = movierepository;
            _mapper = mapper;
            _movieActorRepository = movieActorRepository;
        }
        public async Task<CreateResponse<MovieDTO>> CreateMovieAsync(MovieCreateCommand movieCommand)
        {
            var movie=_mapper.Map<Movie>(movieCommand);
            //var language = new Language(movieDTO.LanguageDTO.originalLanguage, movieDTO.LanguageDTO.spokenLanguages);
            //var genre = new Genre(movieDTO.GenreDTO.primaryGenre, movieDTO.GenreDTO.subGenres);
            //var movie = new Movie(movieDTO.Title, movieDTO.releaseDate, movieDTO.Description,
            //    movieDTO.PosterURL, language, genre, movieDTO.Length);
            _movierepository.AddMovie(movie);
            await _movierepository.SaveChangesAsync();
            var returnedMovie = _mapper.Map<MovieDTO>(movie);
            CreateResponse<MovieDTO> response = new CreateResponse<MovieDTO>()
            {
                isSuccess = true,
                message = "Movie has been created successfully",
                data = returnedMovie
            };
            return response;
        }
        public async Task DeleteMovieAsync(int id)
        {
            Movie movie = _movierepository.GetMovie(id);
            _movierepository.RemoveMovie(movie);
            await _movierepository.SaveChangesAsync();
        }
        public async Task<CreateResponse<int>> UpdateMovieDetails(int id, MovieCreateCommand movieCommand)
        {
            var movie=_mapper.Map<Movie>(movieCommand);
            //var language = new Language(movieDTO.LanguageDTO.originalLanguage, movieDTO.LanguageDTO.spokenLanguages);
            //var genre = new Genre(movieDTO.GenreDTO.primaryGenre, movieDTO.GenreDTO.subGenres);
            //var movie = new Movie(movieDTO.Title, movieDTO.releaseDate, movieDTO.Description,
            //    movieDTO.PosterURL, language, genre, movieDTO.Length);
            await _movierepository.UpdateMovie(id, movie);
            await _movierepository.SaveChangesAsync();
            CreateResponse<int> response = new CreateResponse<int>()
            {
                isSuccess = true,
                message = "Updated successfully",
                data = id
            };
            return response;
        }
        public async Task AddMovieActor(int movieID, int actorID)
        {
            _movierepository.AddActor(movieID, actorID);
            await _movierepository.SaveChangesAsync();
        }
        public async Task RemoveMovieActor(int movieID, int actorID)
        {
            _movieActorRepository.RemoveMovieActor(movieID, actorID);
            await _movierepository.SaveChangesAsync();
        }
        public async Task AddCinema(int movieID, CinemaDTO cinemaDTO)
        {
            var movie = _movierepository.GetMovie(movieID);
            movie.AddCinema(cinemaDTO.Name, cinemaDTO.Address);
            await _movierepository.SaveChangesAsync();
        }

    }
}
