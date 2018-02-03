using System.Data.Entity;
using OrganizerApp.DalEntities.Entities;

namespace OrganizerApp.DAL.Interfaces
{
    public abstract class AbstractDbContext : DbContext
    {
        public abstract DbSet<Project> Projects { get; set; }
        public abstract DbSet<Task> Tasks { get; set; }
    }
}
