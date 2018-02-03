using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;
using System.Data.Entity.Migrations;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Entity.Core;
using OrganizerApp.DAL.Interfaces;
using OrganizerApp.DalEntities.Entities;
using OrganizerApp.Dalnterfaces;
using System.Data.SqlClient;

namespace OrganizerApp.DAL.Repositorys
{
    public class ProjectsRepository : IRepository<IQueryable<Project> , IQueryable<Project>, Project>
    {
        private readonly AbstractDbContext _dbContext;


        public ProjectsRepository(AbstractDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IQueryable<Project> GetById(int id)
        {
            try
            {
                return _dbContext.Projects
                                 .Where(x => x.ID == id)
                                 .Include(x => x.ProjectTasks);
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public IQueryable<Project> GetAll()
        {
            try
            {
                return _dbContext.Projects
                                 .Include(x => x.ProjectTasks);
            } 
            catch (SqlException)
            {
                throw;
            }
            
        }

        public void Remove(int id)
        {
            Project recordToRemove = new Project()
            {
                ID = id
            };

            _dbContext.Entry(recordToRemove).State = EntityState.Deleted;

            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (UpdateException)
            {
                throw;
            }
            catch (DbEntityValidationException)
            {
                throw;
            }
            catch (NotSupportedException)
            {
                throw;
            }
            catch (ObjectDisposedException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            finally
            {
                _dbContext.Database.Connection.Close();
            }
        }

        public void Save(Project entity, params Func<Project, string>[] modifiedPropertyNames)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("Wartość encji musi być różna od null");
            }
            if (entity.ID == 0)
            {
                _dbContext.Projects.Add(entity);
            }
            else if (entity.ID < 0)
            {
                throw new ArgumentOutOfRangeException("ID obiektu nie może być ujemne. Podane ID: " + entity.ID);
            }
            else
            {
                if (_dbContext.Projects.Any(x => x.ID == entity.ID))
                {
                    Update(entity, modifiedPropertyNames);
                }
                else
                {
                    throw new KeyNotFoundException("Próbujesz zaktualizować rekord, który nie istnieje. Nie znaleziono istniejącego rekordu o ID: " + entity.ID);
                }
            }

            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (UpdateException)
            {
                throw;
            }
            catch (DbEntityValidationException)
            {
                throw;
            }
            catch (NotSupportedException)
            {
                throw;
            }
            catch (ObjectDisposedException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            finally
            {
                _dbContext.Database.Connection.Close();
            }

        }

        private void Update(Project entity, params Func<Project, string>[] modifiedPropertyNames)
        {
            _dbContext.Projects.Attach(entity);
            var entry = _dbContext.Entry(entity);

            if (modifiedPropertyNames.Count() > 0)
            {
                foreach (var modifiedPropertyName in modifiedPropertyNames)
                {
                    entry.Property(modifiedPropertyName.Invoke(entity)).IsModified = true;
                }
            }
            else
            {
                entry.State = EntityState.Modified;
            }
        }
    }
}
