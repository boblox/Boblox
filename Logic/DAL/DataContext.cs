using System.Data.Entity;

namespace Logic.DAL
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("umbracoDbDSN")
        {

        }
    }
}
