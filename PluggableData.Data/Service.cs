using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;

namespace PluggableData.Data {
	public class Service {
		private IUnitOfWork _unitOfWork = new UnitOfWork();

		/// <summary>
		/// Constructor inspects the executing and calling assemblies to find plugins
		/// The container can probably be held elsewhere as a singleton so 
		/// the inspection only happens at startup
		/// </summary>
		public Service() {
			var catalogs = new List<ComposablePartCatalog> { new AssemblyCatalog(Assembly.GetExecutingAssembly()), new AssemblyCatalog(Assembly.GetCallingAssembly()) };
			var catalog = new AggregateCatalog(catalogs);
			var container = new CompositionContainer(catalog);
			container.ComposeParts(this);
		}

		/// <summary>
		/// This property is populated by MEF after it inspects the assemblies
		/// </summary>
		[ImportMany]
		public IEnumerable<IQueryPlugin> Queries { get; set; }

		/// <summary>
		/// This is an example of a Core query (e.g. FindById(id))
		/// </summary>
		/// <returns></returns>
		public string CoreEndpoint() {
			return "this would be how core data access methods are used";
		}

		/// <summary>
		/// This method should only be called by a plugin's extension methods
		/// It's public because it needs to be accessible by various libraries
		/// </summary>
		/// <param name="queryType">The type of plugin; used to find the correct plugin from Queries</param>
		/// <param name="args">The parameters passed in by the extension method</param>
		/// <returns>The output of the query - properly typed but passed as a dynamic</returns>
		public dynamic ExecuteDynamicEndpoint(Type queryType, params object[] args) {
			var query = Queries.Single(q => q.GetType() == queryType);
			query.UnitOfWork = _unitOfWork;
			return query.Execute(args);
		}
	}
}
