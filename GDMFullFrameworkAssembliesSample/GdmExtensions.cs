using System;
using System.Collections.Generic;
using System.Text;
using DataGenic.DataLibrary;

namespace GDMFullFrameworkAssembliesSample
{
	public static class GdmExtensions
	{
		public static void Deconstruct(this GenicData gd, out double[] data, out string fullRange)
		{
			// assumption that we have a numeric curve
			data = gd.NumericSeries.Data;
			fullRange = gd.FullRangeUri;
		}
	}
}
