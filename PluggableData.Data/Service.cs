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

		public Service() {
			var catalogs = new List<ComposablePartCatalog> { new AssemblyCatalog(Assembly.GetExecutingAssembly()), new AssemblyCatalog(Assembly.GetCallingAssembly()) };
			var catalog = new AggregateCatalog(catalogs);
			var container = new CompositionContainer(catalog);
			container.ComposeParts(this);
		}

		[ImportMany]
		public IEnumerable<IQueryPlugin> Queries { get; set; }

		public string CoreEndpoint() {
			return "this would be how core data access methods are used";
		}

		public dynamic ExecuteDynamicEndpoint(Type queryType, params object[] args) {
			var query = Queries.Single(q => q.GetType() == queryType);
			query.UnitOfWork = _unitOfWork;
			return query.Execute(args);
		}
	}
}
