namespace Models.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.DataBaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            ContextKey = "Models.DataBaseContext";
        }

        protected override void Seed(Models.DataBaseContext context)
        {
            //base.Seed(databaseContext);

            if (context.GroupProducts.Count() != 0)
            {
                return;
            }

            try
            {
                DatabaseContextInitializer.Seed(context);
            }
            catch
            {
            }
         
        }
    }
}
