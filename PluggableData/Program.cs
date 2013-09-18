using System;
using PluggableData.Data;

namespace PluggableData {
	class Program {
		static void Main(string[] args) {
			//instantiate the service
			var service = new Service();

			//call a method from a plugin
			var pluginOuput1 = service.ExampleQuery(10, "test");
			var coreOutput = service.CoreEndpoint();
			var pluginOutput2 = service.ExampleQuery(5, "second test");

			//display results
			foreach (var line in pluginOuput1) {
				Console.WriteLine(line);
			}

			Console.WriteLine(coreOutput);

			foreach (var line in pluginOutput2) {
				Console.WriteLine(line);
			}
		}
	}
}
