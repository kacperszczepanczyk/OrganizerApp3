using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using Newtonsoft.Json;
using OrganizerApp.WebUI.Constants;
using OrganizerApp.WebUI.Models;
using OrganizerApp.BllDtos.Projects;
using OrganizerApp.WebUI.Infrastructure.Validation;
using FluentValidation.Results;
using OrganizerApp.WebUI.Helpers.View.ContentGenerator.Implementations;
using OrganizerApp.DataCirculationHelpers;
using System.Web.Mvc;
using System.Threading.Tasks;
using RestSharp;
using OrganizerApp.WebUI.Infrastructure;

namespace OrganizerApp.WebUI.Controllers
{
    public class ProjectsController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> List(string searchPhrase, ProjectType projectsType = ProjectType.All)
        {
            NameValueCollection queryParameters = HttpUtility.ParseQueryString(Request.Url.Query);
            queryParameters.Add("responseDataSetType", nameof(ProjectResponseDataSetType.HeaderWithPriorityAndState));
            RestRequest request = new RestRequest(ApiUriInfo.Path.GetProjects , Method.GET);
            foreach (string key in queryParameters)
            {
                request.AddParameter(key, queryParameters[key]);
            }

            RestClient client = DependencyResolver.Current.GetService<RestClient>();

            var responseTask = client.ExecuteTaskAsync<List<ProjectBaseWithPriorityAndState>>(request);

            ProjectsListViewModel viewModel = new ProjectsListViewModel()
            {
                DoneProjectActionUri = new UriBuilder(
                    ApiUriInfo.Scheme,
                    ApiUriInfo.Host,
                    ApiUriInfo.Port,
                    ApiUriInfo.Path.DoneProject
                ).ToString()
            };

            var response = await responseTask;

            if (response.IsSuccessful)
            { 
                viewModel.Projects = response.Data;
            }
            else
            {
                throw new ExternalDataCirculationException("Uzyskanie danych od zewnętrznego dostawcy zakończyło się niepowodzeniem");
            }

            return View("List", viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ProjectEditViewModel viewModel = new ProjectEditViewModel()
            {
                Project = new ProjectDetail { ID = 0 },
                ContentGenerator = new ProjectEditContentGenerator()
            };
            return View("Edit", viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            RestRequest request = new RestRequest(ApiUriInfo.Path.GetProjectById , Method.GET);
            request.AddParameter("id", id);

            RestClient client = DependencyResolver.Current.GetService<RestClient>();
            var responseTask = Task.Run(() => client.Execute(request));
            ProjectEditViewModel viewModel = new ProjectEditViewModel()
            {
                ContentGenerator = new ProjectEditContentGenerator()
            };

            var response = await responseTask;

            if (response.IsSuccessful)
            {
                viewModel.Project = JsonConvert.DeserializeObject<ProjectDetail>(response.Content);
            }
            else
            {
                throw new ExternalDataCirculationException("Uzyskanie danych od zewnętrznego dostawcy zakończyło się niepowodzeniem");
            }

            return View("Edit", viewModel);
        }



        [HttpPost]
        public async Task<ActionResult> Edit(ProjectDetail project)
        {
            ProjectDetailValidator validator = new ProjectDetailValidator();
            ValidationResult validationResults = validator.Validate(project);

            if(!validationResults.IsValid)
            {
                ProjectEditViewModel viewModel = new ProjectEditViewModel()
                {
                    Project = project,
                    ContentGenerator = new ProjectEditContentGenerator()
                };

                foreach (var error in validationResults.Errors)
                {
                    ModelState.AddModelError(nameof(ProjectEditViewModel.Project) + '.' + error.PropertyName, error.ErrorMessage);
                }

                return View(viewModel);
            }
            
            string projectAsJson = JsonConvert.SerializeObject(project);
            RestClient client = DependencyResolver.Current.GetService<RestClient>();
            RestRequest request = new RestRequest(ApiUriInfo.Path.SaveProject, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddParameter("application/json", projectAsJson, ParameterType.RequestBody);

            var response = await client.ExecuteTaskAsync<ProjectDetail>(request);

            if (response.IsSuccessful) 
            {
                return RedirectToAction("List");
            }
            else
            {
                throw new ExternalDataCirculationException("Uzyskanie danych od zewnętrznego dostawcy zakończyło się niepowodzeniem");
            }
        }

        
        public async Task<ActionResult> Search(string searchPhrase)
        {
            RestClient client = DependencyResolver.Current.GetService<RestClient>();
            RestRequest request = new RestRequest(ApiUriInfo.Path.GetProjects, Method.GET);
            request.AddParameter("searchPhrase", searchPhrase);
            request.AddParameter("responseDataSetType", nameof(DataCirculationHelpers.ProjectResponseDataSetType.HeaderWithPriorityAndState));
            var responseTask = client.ExecuteTaskAsync<List<ProjectBaseWithPriorityAndState>>(request);

            ProjectsListViewModel viewModel = new ProjectsListViewModel()
            {
                DoneProjectActionUri = new UriBuilder(
                    ApiUriInfo.Scheme,
                    ApiUriInfo.Host,
                    ApiUriInfo.Port,
                    ApiUriInfo.Path.DoneTask
                ).ToString()
            };

            var response = await responseTask;

            if (response.IsSuccessful)
            {
                viewModel.Projects = response.Data;
            }
            else
            {
                throw new ExternalDataCirculationException("Uzyskanie danych od zewnętrznego dostawcy zakończyło się niepowodzeniem");
            }

            return View("List", viewModel);
        }
    }
}