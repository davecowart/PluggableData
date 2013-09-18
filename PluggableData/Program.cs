using System;
using PluggableData.Data;

namespace PluggableData {
	class Program {
		static void Main(string[] args) {
			var service = new Service();
			var output = service.ExampleQuery(10, "test");
			foreach (var line in output) {
				Console.WriteLine(line);
			}

			Console.WriteLine(service.CoreEndpoint());

			var output2 = service.ExampleQuery(5, "second test");
			foreach (var line in output2) {
				Console.WriteLine(line);
			}

		}
	}
}
