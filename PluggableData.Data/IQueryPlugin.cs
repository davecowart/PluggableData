using System;

namespace PluggableData.Data {
	public interface IQueryPlugin {
		Type[] MethodArgs { get; }
		dynamic Execute(params object[] args);
	}
}
