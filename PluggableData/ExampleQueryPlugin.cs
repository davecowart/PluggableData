using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using PluggableData.Data;

namespace PluggableData {
	[Export(typeof(IQueryPlugin))]
	public class ExampleQueryPlugin : IQueryPlugin {
		public Type[] MethodArgs { get { return new[] { typeof(int), typeof(string) }; } }
		public IUnitOfWork UnitOfWork { get; set; }

		public dynamic Execute(params object[] args) {
			//todo: better parameter mapping
			var count = (int)args[0];
			var output = (string)args[1];
			var list = new List<string>();
			for (var i = 0; i < count; i++) {
				list.Add(output);
			}
			//this would normally run the actual query and perform any necessary mapping
			return list;
		}
	}


	public static partial class ServiceExtensions {
		public static IEnumerable<string> ExampleQuery(this Service service, int count, string output) {
			return service.ExecuteDynamicEndpoint(typeof(ExampleQueryPlugin), count, output) as IEnumerable<string>;
		}
	}
}
