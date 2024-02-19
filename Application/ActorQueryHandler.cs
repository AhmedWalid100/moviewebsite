using AutoMapper;
using MoviesProject.DomainLayer.Interfaces;

namespace MoviesProject.Application
{
    public class ActorQueryHandler : IActorQueryHandler
    {
        public IActorRepository _actorrepository;
        public IMapper _mapper;
        public IMovieActorRepository _movieActorRepository;
        public ActorQueryHandler(IActorRepository actorrepository, IMapper mapper
            , IMovieActorRepository movieActorRepository)
        {
            _actorrepository = actorrepository;
            _mapper = mapper;
            _movieActorRepository = movieActorRepository;
        }
        public List<ActorDTO> GetAllActors()
        {
            var actors = _actorrepository.GetActors();
            var actorsDTO = actors.Select(actor => _mapper.Map<ActorDTO>(actor)).ToList();
            return actorsDTO;
        }
        public ActorDTO GetActorById(int id)
        {
            return _mapper.Map<ActorDTO>(_actorrepository.GetActor(id));
        }
        public List<MovieDTO> GetMoviesByActorID(int actorID)
        {
            return _movieActorRepository.GetMoviesByActorID(actorID);

        }
    }
}
