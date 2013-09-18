using System;

namespace PluggableData.Data {
	public interface IQueryPlugin {
		Type[] MethodArgs { get; }
		IUnitOfWork UnitOfWork { set; }
		dynamic Execute(params object[] args);
	}
}
