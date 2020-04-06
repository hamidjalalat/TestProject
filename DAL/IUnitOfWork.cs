namespace DAL
{
	public interface IUnitOfWork : System.IDisposable
	{
		IUserRepository UserRepository { get; }
		IProductRepository ProductRepository { get; }

		void Save();
	}
}
