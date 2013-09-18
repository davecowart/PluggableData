using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace PluggableData.Data {
	public abstract class QueryPlugin : IQueryPlugin {
		public IUnitOfWork UnitOfWork { get; set; }
		protected abstract dynamic ExecutePlugin(params object[] args);

		/// <summary>
		/// Should only be called by ExecuteDynamicEndpoint in the Service class
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public dynamic Execute(params object[] args) {
			return ExecutePlugin(args);
		}

		/// <summary>
		/// Get the parameters from the extension method
		/// </summary>
		/// <param name="extensionMethod"></param>
		/// <returns></returns>
		private static ParameterInfo[] GetParameters(MethodInfo extensionMethod) {
			//skips the 'this' parameter of the extension method
			return extensionMethod.GetParameters().Skip(1).ToArray();
		}

		/// <summary>
		/// Convert the params object array into an expando object with a property for each argument
		/// </summary>
		/// <param name="extensionMethod">The extension method defined as part of the plugin</param>
		/// <param name="args">The parameters passed into the extension method</param>
		/// <returns></returns>
		protected ExpandoObject MapParameters(MethodInfo extensionMethod, params object[] args) {
			var expando = new ExpandoObject() as IDictionary<string, object>;
			var parameters = GetParameters(extensionMethod);
			for (var i = 0; i < args.Length; i++) {
				expando.Add(parameters[i].Name, args[i]);
			}
			return expando as ExpandoObject;
		}
	}
}
