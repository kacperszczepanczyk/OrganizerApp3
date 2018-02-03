using System.Web.Http;
using System.Web.Http.Cors;
using OrganizerApp.WebApi.Infrastructure.Validation;
using OrganizerApp.BLL.Interfaces;
using OrganizerApp.BllDtos.Projects;
using FluentValidation;
using OrganizerApp.BLL.Helpers.FilterData;
using OrganizerApp.DataCirculationHelpers;
using System.Net;

namespace OrganizerApp.WebApi.Controllers
{
    [EnableCors(origins: "http://localhost:63758" , headers: "*", methods: "*")]
    public class ProjectsController : ApiController
    {
        private readonly IProjectsBusinessDataManager _projectsDataManager;


        public ProjectsController(IProjectsBusinessDataManager projectsDataManager)
        {
            _projectsDataManager = projectsDataManager;
        }


        [HttpGet]
        public IHttpActionResult GetFiltered(string searchPhrase = null, ProjectType projectsType = ProjectType.All , ProjectResponseDataSetType responseDataSetType = ProjectResponseDataSetType.Detail)
        {
            switch (responseDataSetType)
            {
                case ProjectResponseDataSetType.Detail:
                    {
                        var projects = _projectsDataManager.GetFiltered<ProjectDetail>(new ProjectGetFilteredArgs(searchPhrase , projectsType));
                        return Ok(projects);
                    }
                case ProjectResponseDataSetType.Header:
                    {
                        var projects = _projectsDataManager.GetFiltered<ProjectBase>(new ProjectGetFilteredArgs(searchPhrase, projectsType));
                        return Ok(projects);
                    }
                case ProjectResponseDataSetType.HeaderWithPriority:
                    {
                        var projects = _projectsDataManager.GetFiltered<ProjectBaseWithPriority>(new ProjectGetFilteredArgs(searchPhrase, projectsType));
                        return Ok(projects); 
                    }
                case ProjectResponseDataSetType.HeaderWithPriorityAndState:
                    {
                        var projects = _projectsDataManager.GetFiltered<ProjectBaseWithPriorityAndState>(new ProjectGetFilteredArgs(searchPhrase, projectsType));
                        return Ok(projects);
                    }
            }

            throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public IHttpActionResult GetById(int id , ProjectResponseDataSetType responseDataSetType = ProjectResponseDataSetType.Detail)
        {
            switch (responseDataSetType)
            {
                case ProjectResponseDataSetType.Detail:
                    {
                        var project = _projectsDataManager.GetById<ProjectDetail>(id);
                        return Ok(project);
                    }
                case ProjectResponseDataSetType.Header:
                    {
                        var project = _projectsDataManager.GetById<ProjectBase>(id);
                        return Ok(project);
                    }
                case ProjectResponseDataSetType.HeaderWithPriority:
                    {
                        var project = _projectsDataManager.GetById<ProjectBaseWithPriority>(id);
                        return Ok(project);
                    }
            }

            throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
        }

        // void zwraca 204 - No Content
        [HttpPatch] //ajax requesty działają dla [HttpPost]
        public void Done(int id)
        {
            _projectsDataManager.Done(id);
        }

        // void zwraca 204 - No Content
        [HttpPatch] //ajax requesty działają dla [HttpPost]
        public void Remove(int id)
        {
            _projectsDataManager.Remove(id);
        }

        // void zwraca 204 - No Content
        [HttpPatch] //ajax requesty działają dla [HttpPost]
        public void Deactivate(int id)
        {
            _projectsDataManager.Deactivate(id);
        }

        // void zwraca 204 - No Content
        [HttpPost]
        public void Save(ProjectDetail project)
        {
            ProjectDetailValidator validator = new ProjectDetailValidator();
            validator.ValidateAndThrow(project);
            _projectsDataManager.Save(project);
        }
    }
}
 