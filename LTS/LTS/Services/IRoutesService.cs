using LTS.DTOs;

namespace LTS.Services
{
    public interface IRoutesService
    {
        public RouteDTO GetRoute(Guid id);

        public List<RouteDTO> GetRoutes();

        public Task PostRoute(RouteDTO route);

    }
}
