using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryPaths.Shared.DB
{
    internal class Country
    {
		public virtual int CountryId
		{
			get;
			set;
		}

		public virtual string Name
		{
			get;
			set;
		}

		public virtual int XCoordinate
		{
			get;
			set;
		}

		public virtual int YCoordinate
		{
			get;
			set;
		}

	}
}
