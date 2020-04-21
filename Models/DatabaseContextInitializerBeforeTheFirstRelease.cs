using System.Linq;
using System.Data.Entity;

namespace Models
{
	internal class DatabaseContextInitializerBeforeTheFirstRelease :
		System.Data.Entity.DropCreateDatabaseIfModelChanges<DataBaseContext>
	{
		public DatabaseContextInitializerBeforeTheFirstRelease() : base()
		{
		}

		protected override void Seed(DataBaseContext databaseContext)
		{
			//base.Seed(databaseContext);

			try
			{
				DatabaseContextInitializer.Seed(databaseContext);
			}
			catch
			{
			}
			//catch (System.Exception ex)
			//{
			//	Dtx.LogHandler.Report(GetType(), null, ex);
			//}
		}
	}
}
