using Microsoft.AspNetCore.Mvc;
using LTS.Models;
using Route = LTS.Models.Route;
using LTS.Services;
using LTS.DTOs;

namespace LTS.Controllers
{
    [ApiController]
    [Route("Routes")]
    public class LTSController : ControllerBase
    {
        private readonly IRoutesService _routesService;
        public LTSController(IRoutesService service) 
        { 
            _routesService = service;      
        }

        [HttpGet("GetRoute")]
        public RouteDTO GetRoutes(Guid id)
        {
            try
            {
                return _routesService.GetRoute(id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("GetRoutes")]
        public List<RouteDTO> GetRoutes()
        {
            List<RouteDTO> route = new();
            return route;
        }

        [HttpPost("PostRoute")]
        public async Task PostRoute(RouteDTO route)
        {
            try
            {
                await _routesService.PostRoute(route);
            }
            catch (Exception)
            {

                throw;
            }



        }
    }
}