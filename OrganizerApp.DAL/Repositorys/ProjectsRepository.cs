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
using OrganizerApp.Helpers;
using OrganizerApp.DalInterfaces;
using OrganizerApp.DalInterfaces.Project;
using OrganizerApp.DataCirculationHelpers;
using System.Linq.Expressions;

namespace OrganizerApp.DAL.Repositorys
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly AbstractDbContext _dbContext;


        public ProjectsRepository(AbstractDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Project GetById(int id)
        {
            try
            {
                return _dbContext.Projects
                                 .Where(x => x.ID == id)
                                 .Include(x => x.ProjectTasks)
                                 .FirstOrDefault();
            }
            finally
            {
                _dbContext.Database.Connection.Close();
            }
        }

        public IEnumerable<Project> GetFiltered(ProjectGetFilteredArgs arguments)
        {
            IQueryable<Project> query = _dbContext.Projects.Include(x => x.ProjectTasks);

            switch (arguments.ProjectsType)
            {
                case ProjectType.Done:
                    {
                        query = query.Where(x => x.State == "done");
                        break;
                    }
                case ProjectType.Deleted:
                    {
                        query = query.Where(x => x.State == "deleted");
                        break;
                    }
                case ProjectType.Active:
                    {
                        query = query.Where(x => x.State == "todo")
                                           .Where(x => x.ExecutionTime != "someday");
                        break;
                    }
                case ProjectType.Disactive:
                    {
                        query = query.Where(x => x.ExecutionTime == "someday")
                                           .Where(x => x.State != "todo");
                        break;
                    }
                default:
                    {
                        if (arguments.ProjectsType != ProjectType.All)
                        {
                            throw new ArgumentException("Argument projectsType przyjął nieprawidłową wartość (wartość po której nie mogę przefiltrować danych)");
                        }
                        break;
                    }
            }

            if (arguments.SearchPhrase != null)
            {
                query = query.Where(x =>
                    x.Name.Contains(arguments.SearchPhrase) ||
                    x.Description.Contains(arguments.SearchPhrase)
                );
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
            Project recordToRemove = new Project()
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
                throw new ValidationException("ID obiektu nie może być ujemne. Podane ID: " + entity.ID);
            }
            else
            {
                bool isRecordExistsInDb = _dbContext.Projects.Any(x => x.ID == entity.ID);
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
