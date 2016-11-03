using System.Data.Entity.Migrations;
using Logic.DAL;

namespace Logic.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Logic.DAL.DataContext";
        }

        protected override void Seed(DataContext context)
        {
        }
    }
}
