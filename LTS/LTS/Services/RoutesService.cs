using AutoMapper;
using LTS.Data.RoutesMongoDb;
using LTS.DTOs;
using LTS.Models;
using Route = LTS.Models.Route;

namespace LTS.Services
{
    public class RoutesService : IRoutesService
    {
        private readonly IRoutesRepository<Route> _routesRepository;
        private readonly IMapper _mapper;

        public RoutesService(IRoutesRepository<Route> routesRepository, IMapper mapper) 
        { 
            _routesRepository = routesRepository;
            _mapper = mapper;
        }  

        public RouteDTO GetRoute(Guid id)
        {
            Route route = _routesRepository.FilterByID(x => x.Id == id.ToString());
            return ConvertToDTO(route);
        }

        public List<RouteDTO> GetRoutes()
        {
            throw new NotImplementedException();
        }

        public async Task PostRoute(RouteDTO route)
        {
            try
            {
                await _routesRepository.InsertOneAsync(ConvertToModel(route));           
            }
            catch (Exception)
            {

            }
        }

        private Route ConvertToModel(RouteDTO dto)
        {
            Route route = new Route();
            List<Segment> segments = new List<Segment>();


            route.Id = dto.Id.ToString(); ;
            route.PriceTotal = dto.PriceTotal;

            foreach(SegmentDTO seg in dto.Segments) 
            {
                Segment segment = new Segment();

                segment.Time = seg.Time;
                segment.Price = seg.Price;
                segment.Start = _mapper.Map<Node>(seg.Start);
                segment.Way = _mapper.Map<Way>(seg.Way);
                segment.End = _mapper.Map<Node>(seg.End);
                segments.Add(segment);
            }
            route.Segments = segments;
            return route;
        }

        private RouteDTO ConvertToDTO(Route route)
        {
            RouteDTO dto = new RouteDTO();
            List<SegmentDTO> segments = new List<SegmentDTO>();

            dto.Id = Guid.Parse(route.Id); 
            dto.PriceTotal = dto.PriceTotal;

            foreach (Segment seg in route.Segments)
            {
                SegmentDTO segment = new SegmentDTO();

                segment.Time = seg.Time;
                segment.Price = seg.Price;
                segment.Start = _mapper.Map<NodeDTO>(seg.Start);
                segment.Way = _mapper.Map<WayDTO>(seg.Way);
                segment.End = _mapper.Map<NodeDTO>(seg.End);
                segments.Add(segment);
            }
            dto.Segments = segments;
            return dto;
        }



    }
}
