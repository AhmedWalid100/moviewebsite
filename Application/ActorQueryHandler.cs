using AutoMapper;
using MoviesProject.DomainLayer.Interfaces;
using MoviesProject.Infrastructure.Repos;

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
        public ReturnedCountAndData<ActorDTO> GetAllActors(int page, int pageSize,
            string? searchName, string orderColumn, string orderBy)
        {
            var actors = _actorrepository.GetActors(page, pageSize,
            searchName,  orderColumn,orderBy);
            var actorsDTO = actors.data.Select(actor => _mapper.Map<ActorDTO>(actor)).ToList();
            var returnedData = new ReturnedCountAndData<ActorDTO>()
            {
                count = actors.count,
                data = actorsDTO
            };
            return returnedData;
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
