using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryPaths.Shared.DB
{
    internal class CountryPath
    {
		[Key]
		public virtual int CountryLinkId
		{
			get;
			set;
		}

		public virtual int CountryId1
		{
			get;
			set;
		}

		public virtual int CountryId2
		{
			get;
			set;
		}
	}
}
