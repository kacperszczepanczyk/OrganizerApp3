using System.Linq;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Entity.Core;
using System.Data.Entity.Migrations;
using OrganizerApp.Dalnterfaces;
using OrganizerApp.DalEntities.Entities;
using OrganizerApp.DAL.Interfaces;
using System.Data.Entity;

namespace OrganizerApp.DAL.Repositorys
{
    public class TasksRepository : IRepository<IQueryable<Task>, IQueryable<Task>, Task>
    {
        private readonly AbstractDbContext _dbContext;


        public TasksRepository(AbstractDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IQueryable<Task> GetById(int id)
        {
            try
            {
                return _dbContext.Tasks
                                 .Where(x => x.ID == id);
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public IQueryable<Task> GetAll()
        {
            try
            {
                return _dbContext.Tasks;
            }
            catch (SqlException)
            {
                throw;
            }
        }

      

        public void Remove(int id)
        {
            Task recordToRemove = new Task()
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

        public void Save(Task entity, params Func<Task, string>[] modifiedPropertyNames)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("Wartość encji musi być różna od null");
            }
            if (entity.ID == 0)
            {
                _dbContext.Tasks.Add(entity);
            }
            else if (entity.ID < 0)
            {
                throw new ArgumentOutOfRangeException("ID obiektu nie może być ujemne. Podane ID: " + entity.ID);
            }
            else
            {
                if (_dbContext.Tasks.Any(x => x.ID == entity.ID))
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

        private void Update(Task entity, params Func<Task, string>[] modifiedPropertyNames)
        {
            _dbContext.Tasks.Attach(entity);
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
