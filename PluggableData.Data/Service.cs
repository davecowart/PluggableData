using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;

namespace PluggableData.Data {
	public class Service {
		private readonly IUnitOfWork _unitOfWork = new UnitOfWork();

		public IUnitOfWork UnitOfWork { get { return _unitOfWork; } }

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
	}

	public interface IUnitOfWork { }

	public class UnitOfWork : IUnitOfWork { 	}
}
