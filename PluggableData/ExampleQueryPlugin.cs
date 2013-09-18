using System.Collections.Generic;
using System.ComponentModel.Composition;
using PluggableData.Data;

namespace PluggableData {
	/// <summary>
	/// The Export attribute tells MEF to inject the plugin into the Service instance
	/// </summary>
	[Export(typeof(IQueryPlugin))]
	public class ExampleQueryPlugin : QueryPlugin {
		/// <summary>
		/// Execute the actual query using the UnitOfWork provided by the calling instance of Service
		/// </summary>
		/// <param name="args">Parameters passed in by the extension method defined below</param>
		/// <returns>The result of the query - properly typed but passed as a dynamic</returns>
		protected override dynamic ExecutePlugin(params object[] args) {
			dynamic parameters = MapParameters(typeof(ServiceExtensions).GetMethod("ExampleQuery"), args);
			var list = new List<string>();
			for (var i = 0; i < parameters.count; i++) {
				list.Add(parameters.output);
			}
			return list;
		}
	}

	public static class ServiceExtensions {
		/// <summary>
		/// A convenience method for use in the Service class; lets Intellisense do its thing
		/// </summary>
		/// <param name="service">The service instance the extension method is attached to</param>
		/// <param name="count">An example parameter</param>
		/// <param name="output">An example parameter</param>
		/// <returns>The output of the query - propery typed</returns>
		public static IEnumerable<string> ExampleQuery(this Service service, int count, string output) {
			return service.ExecuteDynamicEndpoint(typeof(ExampleQueryPlugin), count, output) as IEnumerable<string>;
		}
	}
}
