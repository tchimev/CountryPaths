using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CountryPaths.Shared
{
    public class Dijkstra
    {
        public Countries Countries;
        public CountryPaths ShortestPath;

        private CountryPaths _paths;

        private Dijkstra() { }

        public static Dijkstra Create()
        {
            Dijkstra dij = new Dijkstra();

            dij.Countries = Countries.GetCountries();
            dij._paths = CountryPaths.GetCountryPaths(dij.Countries);
            //dij.ShortestPath = CountryPaths.NewCountryPaths();

            return dij;
        }

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
                if (destination.IsVisited || unvisitedNeighborsList.Count == 0)
                    break;

                var route = (from u in unvisitedNeighborsList
                             orderby u.Country2.Weight ascending
                             select u).FirstOrDefault();

                current = route.Country2;
                this.ShortestPath.Add(route);
            }
        }

        private void ResetValues()
        {
            this.ShortestPath.Clear();
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