using BusinessLayer.Entities;
using System.Data.Entity;

namespace BusinessLayer.ContextClasses
{
    public class PMDashbordDBContext : DbContext

    {
        public PMDashbordDBContext() : base("PMDashbordDBContext") { }
        public virtual DbSet<Users> Users { get; set; }
    }
}