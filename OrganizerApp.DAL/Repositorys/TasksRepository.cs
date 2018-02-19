using System.Linq;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using OrganizerApp.DalEntities.Entities;
using OrganizerApp.DAL.Interfaces;
using System.Data.Entity;
using OrganizerApp.Helpers;
using OrganizerApp.DalInterfaces;
using OrganizerApp.DalInterfaces.Task;
using OrganizerApp.DataCirculationHelpers;
using System.Linq.Expressions;

namespace OrganizerApp.DAL.Repositorys
{
    public class TasksRepository : ITasksRepository
    {
        private readonly AbstractDbContext _dbContext;


        public TasksRepository(AbstractDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Task GetById(int id)
        {
            try
            {
                return _dbContext.Tasks
                                 .Where(x => x.ID == id)
                                 .FirstOrDefault();
            }
            finally
            {
                _dbContext.Database.Connection.Close();
            }
        }

        public IEnumerable<Task> GetFiltered(TaskGetFilteredArgs arguments)
        {
            IQueryable<Task> query = _dbContext.Tasks;

            if (arguments.ProjectID != null)
            {
                query = query.Where(x => x.ProjectID == arguments.ProjectID);
            }

            switch (arguments.TasksType)
            {
                case TaskType.Done:
                    {
                        query = query.Where(x => x.State == "done");
                        break;
                    }
                case TaskType.Deleted:
                    {
                        query = query.Where(x => x.State == "deleted");
                        break;
                    }
                case TaskType.Active:
                    {
                        query = query.Where(x => x.State == "todo")
                                               .Where(x => x.ExecutionTime != "someday" || x.ProjectID != null);
                        break;
                    }
                case TaskType.Disactive:
                    {
                        query = query.Where(x => x.ProjectID == null)
                                               .Where(x => x.ExecutionTime == "someday");
                        break;
                    }
                default:
                    {
                        if (arguments.TasksType != TaskType.All)
                        {
                            throw new ArgumentException("Argument tasksType przyjął wartość po której nie mogę przefiltrować danych");
                        }
                        break;
                    }
            }

            if (arguments.TimeType != null)
            {
                query = query.Where(x => x.ExecutionTime == arguments.TimeType);
            }

            if (arguments.SearchPhrase != null)
            {
                query = query.Where(x =>
                    x.Name.Contains(arguments.SearchPhrase) ||
                    x.Description.Contains(arguments.SearchPhrase)
                );
            }

            if (arguments.Date != null)
            {
                query = query.Where(x => x.StartTime == arguments.Date);

            }

            try
            {
                return query.ToList();
            }
            finally
            {
                _dbContext.Database.Connection.Close();
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
                throw new ValidationException("ID obiektu nie może być ujemne. Podane ID: " + entity.ID);
            }
            else
            {
                bool isRecordExistsInDb = _dbContext.Tasks.Any(x => x.ID == entity.ID);
                if (!isRecordExistsInDb)
                {
                    throw new ValidationException("Próbujesz zaktualizować rekord, który nie istnieje. Nie znaleziono istniejącego rekordu o ID: " + entity.ID);
                }
                Update(entity, modifiedPropertyNames);
            }

            try
            {
                _dbContext.SaveChanges();
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
