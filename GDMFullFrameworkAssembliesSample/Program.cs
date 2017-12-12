using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataGenic.Gdm.API;
using DataGenic.Gdm.Core;
using static System.Console;

namespace GDMFullFrameworkAssembliesSample
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{


				WriteLine("Starting");

				WriteLine("Please enter user name");
				var userName = ReadLine();
				WriteLine("Please enter password");
				var password = ReadLine();
				var uri = new ServerUri("todo");
				
				uri.SetUser(userName, password);

				var actions = new GdmActions(uri);


				var modelUri = "model://CHMI_W_FLOW_FCT/EU.WATER_OUT.CHMI.BECHYNE.FCT";
				WriteLine($"Getting model for {modelUri}");
				var model = actions.GetModel(modelUri);


				var firstCuve = model.Curves.First(c => c.Name == "CURVE");
				var firstOd = firstCuve.CurveDates.First();
				var versions = firstOd.Versions.Select(v => $"{modelUri}/CURVE/{firstOd.OnDateAsString}/{v.Name}").ToArray();
				WriteLine($"Found {versions.Length} ondates");

				// One by one, slow but only for demo
				versions.ForEach(v =>
				{
					WriteLine($"Getting data for {v}");
					(double[] data, string fullRange) = actions.GetGenicData(v, "range://default");
					WriteLine($"Found {data.Length} items for full range: {fullRange}");
				});

			}
			catch (Exception exp)
			{
				WriteLine(exp.Message);
			}

			WriteLine("Press any key to exit");
			ReadKey();
		}
	}
}
