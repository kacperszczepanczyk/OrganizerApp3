using OrganizerApp.DAL.Interfaces;
using OrganizerApp.DalEntities.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace OrganizerApp.DAL.Contexts
{
    public class StandardDbContext : AbstractDbContext
    {
        public override DbSet<Task> Tasks { get; set; }
        public override DbSet<Project> Projects { get; set; }


        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            var result = base.ValidateEntity(entityEntry, items);
            var falseErrors = result.ValidationErrors
              .Where(error =>
              {
                  if (entityEntry.State != EntityState.Modified) return false;
                  var member = entityEntry.Member(error.PropertyName);
                  var property = member as DbPropertyEntry;
                  if (property != null)
                      return !property.IsModified;
                  else
                      return false;
              });

            foreach (var error in falseErrors.ToArray())
            {
                result.ValidationErrors.Remove(error);
            }
            return result;
        }
    }
}
