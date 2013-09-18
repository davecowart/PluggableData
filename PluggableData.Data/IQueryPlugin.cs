namespace PluggableData.Data {
	public interface IQueryPlugin {
		IUnitOfWork UnitOfWork { set; }
		dynamic Execute(params object[] args);
	}
}
