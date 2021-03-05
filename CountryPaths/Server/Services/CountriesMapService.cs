using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountryPaths.Server.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CountryPaths.Server
{
    public class CountriesMapService : CountriesMap.CountriesMapBase
    {
        public override Task<CountriesReplay> GetCountries(Empty request, ServerCallContext context)
        {
            var countryList = Shared.Countries.GetCountries();
            var countriesReplay = countryList.Select(c => new Country { Id = c.ID, Name = c.Name }).ToList();

            return Task.FromResult(new CountriesReplay
            {
                Countries = { countriesReplay }
            });
        }

        public override Task<RouteReplay> FindShortestRoute(CountriesRequest request, ServerCallContext context)
        {
            var map = Shared.Map.Create();
            var startCountry = map.Countries.Where(c => c.ID == request.StartCountryId).SingleOrDefault();
            var endCountry = map.Countries.Where(c => c.ID == request.EndCountryId).SingleOrDefault();

            if (startCountry != null && endCountry != null)
                map.FindShortestPath(startCountry, endCountry);

            var routeReplay = map.ShortestRoute.Select(c => new Country { Id = c.ID, Name = c.Name }).ToList();
            return Task.FromResult(new RouteReplay
            {
                Route = { routeReplay }
            });
        }
    }
}
