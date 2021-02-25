using CountryPaths.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CountryPaths.Shared
{
    /// <summary>
    /// Represents a country
    /// </summary>
    public class Country
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool IsVisited { get; set; }
        public double Weight { get; set; }

        /// <summary>
        /// Creates country
        /// </summary>
        /// <param name="dbCountry"></param>
        internal Country(Models.Country dbCountry)
        {
            this.ID = dbCountry.CountryId;
            this.Name = dbCountry.Name;
            this.X = dbCountry.XCoordinate;
            this.Y = dbCountry.YCoordinate;
            this.IsVisited = false;
            this.Weight = double.PositiveInfinity;
        }

        public override string ToString()
        {
            return this.Name + "-" + this.ID.ToString();
        }

        /// <summary>
        /// Set country weight and isVisited
        /// values to default
        /// </summary>
        public void Reset()
        {
            this.IsVisited = false;
            this.Weight = double.PositiveInfinity;
        }
    }

    /// <summary>
    /// Represents list of countries
    /// </summary>
    public class Countries : List<Country>
    {
        private Countries() { }

        /// <summary>
        /// Fetches countries from DB
        /// </summary>
        /// <returns>countries</returns>
        public static Countries GetCountries()
        {
            Countries cs = new Countries();

            using (var db = new CountryPathsContext())
            {
                foreach (var item in db.Countries)
                {
                    cs.Add(new Country(item));
                }
            }

            return cs;
        }

        /// <summary>
        /// Creates list of countries
        /// </summary>
        /// <returns>countries</returns>
        public static Countries NewCountries()
        {
            return new Countries();
        }
    }
}