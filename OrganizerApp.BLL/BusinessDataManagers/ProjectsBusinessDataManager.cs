using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OrganizerApp.BllDtos.Projects;
using OrganizerApp.DalEntities.Entities;
using OrganizerApp.BLL.Interfaces;
using OrganizerApp.DataCirculationHelpers;
using OrganizerApp.DalInterfaces;
using OrganizerApp.DalInterfaces.Project;

namespace OrganizerApp.BLL.BusinessDataManagers
{
    public class ProjectsBusinessDataManager : IProjectsBusinessDataManager
    {
        private readonly IProjectsRepository _projectsRepository;


        public ProjectsBusinessDataManager(IProjectsRepository projectsRepository)
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

            _projectsRepository.Save(project, x => nameof(x.ExecutionTime) , x => nameof(x.StartTime));
           
        }

        public void Done(int id)
        {
            Project project = new Project()
            {
                ID = id,
                State = "done"
            };

            _projectsRepository.Save(project, x => nameof(x.State));
        }

        public T GetById<T>(int id) where T : ProjectBase
        {
            var project = _projectsRepository.GetById(id);
            var projectDto = Mapper.Map<T>(project);

            return projectDto;
        }

        public IEnumerable<T> GetFiltered<T>(ProjectGetFilteredArgs arguments) where T : ProjectBase
        {
            if(arguments == null)
            {
                throw new ArgumentNullException("Obiekt arguments musi mieć wartość różną od null");
            }

            var projects = _projectsRepository.GetFiltered(arguments);
            var projectDtos = Mapper.Map<IEnumerable<T>>(projects);

            return projectDtos;
        }

        public void Remove(int id)
        {
            Project project = new Project()
            {
                ID = id,
                State = "deleted"
            };

            _projectsRepository.Save(project , x => nameof(x.State));
        }

        public void Save(ProjectDetail project)
        {
            var projectEntity = Mapper.Map<Project>(project);
            _projectsRepository.Save(projectEntity);
        }
    }
}
