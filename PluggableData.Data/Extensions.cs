using System;
using System.Linq;
using PluggableData.Data;

namespace ExtensionMethods {
	public static class Extensions {
		/// <summary>
		/// This method should only be called by a plugin's extension methods
		/// It's public because it needs to be accessible by various libraries
		/// But placed in a unique namespace so it isn't automatically accessibly to the caller of Service
		/// </summary>
		/// <param name="service">The service the extension method attaches to</param>
		/// <param name="queryType">The type of plugin; used to find the correct plugin from Queries</param>
		/// <param name="args">The parameters passed in by the extension method</param>
		/// <returns>The output of the query - properly typed but passed as a dynamic</returns>
		public static dynamic ExecuteDynamicEndpoint(this Service service, Type queryType, params object[] args) {
			var query = service.Queries.Single(q => q.GetType() == queryType);
			query.UnitOfWork = service.UnitOfWork;
			return query.Execute(args);
		}
	}
}