using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CountryPaths.Shared
{
    /// <summary>
    /// Represents a map with countries
    /// </summary>
    public class Map
    {
        public Countries Countries;
        public Countries ShortestRoute;

        private CountryPaths _paths;

        private Map() { }

        /// <summary>
        /// Creates map
        /// </summary>
        /// <returns>Map</returns>
        public static Map Create()
        {
            Map dij = new Map();

            dij.Countries = Countries.GetCountries();
            dij._paths = CountryPaths.GetCountryPaths(dij.Countries);
            dij.ShortestRoute = Countries.NewCountries();

            return dij;
        }

        /// <summary>
        /// Finds shortest route between two countries
        /// using Dijkstra algorithm
        /// </summary>
        /// <param name="start">first country</param>
        /// <param name="destination">second country</param>
        public void FindShortestPath(Country start, Country destination)
        {
            this.ResetValues();

            start.Weight = 0;
            Country current = start;

            while (true)
            {
                var unvisitedNeighborsList = this.GetUnvisitedNeighbors(current);

                foreach (var unvCtry in unvisitedNeighborsList)
                {
                    unvCtry.CalcDistance();
                    if (unvCtry.Distance + current.Weight < unvCtry.Country2.Weight)
                    {
                        unvCtry.Country2.Weight = unvCtry.Distance + current.Weight;
                    }
                }

                current.IsVisited = true;
                this.ShortestRoute.Add(current);

                if (destination.IsVisited || unvisitedNeighborsList.Count == 0)
                    break;

                var route = (from u in unvisitedNeighborsList
                             orderby u.Country2.Weight ascending
                             select u).FirstOrDefault();

                current = route.Country2;
            }
        }

        private void ResetValues()
        {
            this.ShortestRoute.Clear();
            foreach (var p in this._paths)
            {
                p.Reset();
            }

            foreach (var c in this.Countries)
            {
                c.Reset();
            }
        }

        private List<CountryPath> GetUnvisitedNeighbors(Country current)
        {
            return this._paths.Where(c => c.Country1.ID == current.ID && c.Country1.IsVisited == false && c.Country2.IsVisited == false).ToList();
        }
    }
}