using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using OrganizerApp.Dalnterfaces;
using OrganizerApp.BllDtos.Projects;
using OrganizerApp.DalEntities.Entities;
using OrganizerApp.BLL.Interfaces;
using OrganizerApp.BLL.Helpers;
using OrganizerApp.BLL.Helpers.FilterData;
using OrganizerApp.DataCirculationHelpers;

namespace OrganizerApp.BLL.BusinessDataManagers
{
    public class ProjectsBusinessDataManager : IProjectsBusinessDataManager
    {
        private readonly IRepository<IQueryable<Project>, IQueryable<Project> , Project> _projectsRepository;


        public ProjectsBusinessDataManager(IRepository<IQueryable<Project>, IQueryable<Project>, Project> projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }


        public void Deactivate(int id)
        {
            Project project = new Project()
            {
                ID = id,
                ExecutionTime = "someday",
                StartTime = null
            };

            try
            {
                _projectsRepository.Save(project, x => nameof(x.ExecutionTime) , x => nameof(x.StartTime));
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
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
        }

        public void Done(int id)
        {
            Project project = new Project()
            {
                ID = id,
                State = "done"
            };

            try
            {
                _projectsRepository.Save(project, x => nameof(x.State));
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
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
        }

        public T GetById<T>(int id) where T : ProjectBase
        {
            IQueryable<Project> project;

            try
            {
                project = _projectsRepository.GetById(id);
            }
            catch (SqlException)
            {
                throw;
            }

            IQueryable<T> projectDto;

            try
            {
                projectDto = project.ProjectTo<T>();
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }

            return projectDto.FirstOrDefault();
        }

        public IEnumerable<T> GetFiltered<T>(ProjectGetFilteredArgs arguments) where T : ProjectBase
        {
            if(arguments == null)
            {
                throw new ArgumentNullException("Obiekt arguments musi mieć wartość różną od null");
            }

            IQueryable<Project> projects;

            try
            {
                projects = _projectsRepository.GetAll();
            }
            catch (SqlException)
            {
                throw;
            }

            switch(arguments.ProjectsType)
            {
                case ProjectType.Done:
                    {
                        projects = projects.Where(x => x.State == "done");
                        break;
                    }
                case ProjectType.Deleted:
                    {
                        projects = projects.Where(x => x.State == "deleted");
                        break;
                    }
                case ProjectType.Active:
                    {
                        projects = projects.Where(x => x.State == "todo")
                                           .Where(x => x.ExecutionTime != "someday");
                        break;  
                    }
                case ProjectType.Disactive:
                    {
                        projects = projects.Where(x => x.ExecutionTime == "someday")
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
                projects = projects.Where(x =>
                    x.Name.Contains(arguments.SearchPhrase) ||
                    x.Description.Contains(arguments.SearchPhrase)
                );
            }

            IQueryable<T> projectDtos;

            try
            {
                projectDtos = projects.ProjectTo<T>();
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }

            return projectDtos.ToList();
        }

        public void Remove(int id)
        {
            Project project = new Project()
            {
                ID = id,
                State = "deleted"
            };

            try
            {
                _projectsRepository.Save(project , x => nameof(x.State));
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
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
        }

        public void Save(ProjectDetail project)
        {
            var projectEntity = AutoMapper.Mapper.Map<Project>(project);

            try
            {
                _projectsRepository.Save(projectEntity);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch(ArgumentOutOfRangeException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
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
        }
    }
}
