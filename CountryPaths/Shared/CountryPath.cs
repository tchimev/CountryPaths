using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CountryPaths.Shared
{
    /// <summary>
    /// Represents a path between two countries
    /// </summary>
    public class CountryPath
    {
        public int ID { get; set; }
        public Country Country1 { get; private set; }
        public Country Country2 { get; private set; }
        public double Distance { get; private set; }

        /// <summary>
        /// Creates country path
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="ctr1">first country</param>
        /// <param name="ctr2">second country</param>
        public CountryPath(int id, Country ctr1, Country ctr2)
        {
            this.ID = id;
            this.Country1 = ctr1;
            this.Country2 = ctr2;
            this.Distance = double.PositiveInfinity;
        }

        /// <summary>
        /// Calculates the distance 
        /// between the two countries
        /// </summary>
        public void CalcDistance()
        {
            double xySqr = Math.Pow((this.Country1.X - this.Country2.X), 2) + Math.Pow((this.Country1.Y-this.Country2.Y), 2);

            this.Distance = Math.Sqrt(xySqr);
        }

        public override string ToString()
        {
            return this.ID.ToString();
        }

        /// <summary>
        /// Sets distance to default value
        /// </summary>
        public void Reset()
        {
            this.Distance = double.PositiveInfinity;
        }
    }

    /// <summary>
    /// Represents a list of paths between two countries
    /// </summary>
    public class CountryPaths : List<CountryPath>
    {
        private CountryPaths() { }

        /// <summary>
        /// Fetches country paths from DB by countries
        /// </summary>
        /// <param name="countries">countries</param>
        /// <returns>country paths</returns>
        public static CountryPaths GetCountryPaths(Countries countries)
        {
            CountryPaths cps = new CountryPaths();
            //ServiceCountries.CountriesModelServiceClient sc = new ServiceCountries.CountriesModelServiceClient();
            //var paths = sc.ReadCountryPathses();

            //foreach (var item in paths)
            //{
            //    var c1 = countries.Where(c => c.ID == item.CountryId1).SingleOrDefault();
            //    var c2 = countries.Where(c => c.ID == item.CountryId2).SingleOrDefault();
            //    if (c1 != null && c2 != null)
            //        cps.Add(new CountryPath(item.CountryLinkId, c1, c2));
            //}

            return cps;
        }
    }
}