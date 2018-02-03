using System;
using OrganizerApp.WebUI.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Collections.Specialized;
using System.Web;
using Newtonsoft.Json;
using OrganizerApp.WebUI.Constants;
using OrganizerApp.BllDtos.Tasks;
using OrganizerApp.BllDtos.Projects;
using FluentValidation.Results;
using OrganizerApp.WebUI.Infrastructure.Validation;
using OrganizerApp.WebUI.Helpers.View.ContentGenerator.Implementations;
using OrganizerApp.DataCirculationHelpers;
using RestSharp;
using System.Threading;
using OrganizerApp.WebUI.Infrastructure;

namespace OrganizerApp.WebUI.Controllers
{
    public class TasksController : Controller
    {
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> List(string timeType , int? projectID , string searchPhrase , string date , TaskType tasksType = TaskType.All)
        {
            NameValueCollection queryParameters = HttpUtility.ParseQueryString(Request.Url.Query);
            queryParameters.Add("responseDataSetType", nameof(TaskResponseDataSetType.HeaderWithPriority));
            RestRequest request = new RestRequest(ApiUriInfo.Path.GetTasks, Method.GET);
            foreach (string key in queryParameters)
            {
                request.AddParameter(key, queryParameters[key]);
            }

            RestClient client = DependencyResolver.Current.GetService<RestClient>();
            var responseTask = client.ExecuteTaskAsync<List<TaskBaseWithPriority>>(request);

            TasksListViewModel viewModel = new TasksListViewModel()
            {
                DoneTaskActionUri = new UriBuilder(
                    ApiUriInfo.Scheme,
                    ApiUriInfo.Host,
                    ApiUriInfo.Port,
                    ApiUriInfo.Path.DoneTask
                ).ToString()
            };

            var response = await responseTask;
            if (response.IsSuccessful)
            {
                viewModel.Tasks = response.Data;
            }
            else
            {
                throw new ExternalDataCirculationException("Uzyskanie danych od zewnętrznego dostawcy zakończyło się niepowodzeniem");
            }

            return View("List" , viewModel);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Create()
        {
            RestRequest request = new RestRequest(ApiUriInfo.Path.GetProjects, Method.GET);
            request.AddParameter("projectsType", nameof(ProjectType.Active));
            request.AddParameter("responseDataSetType", nameof(ProjectResponseDataSetType.Header));

            RestClient client = DependencyResolver.Current.GetService<RestClient>();
            var responseTask = client.ExecuteTaskAsync<List<ProjectBase>>(request);
            
            TaskEditViewModel viewModel = new TaskEditViewModel()
            {
                Task = new TaskDetail { ID = 0 },
                ContentGenerator = new TaskEditContentGenerator()
            };

            var response = await responseTask;
            if (response.IsSuccessful)
            {
                viewModel.ActiveProjects = response.Data;
            }
            else
            {
                throw new ExternalDataCirculationException("Uzyskanie danych od zewnętrznego dostawcy zakończyło się niepowodzeniem");
            }

            return View("Edit" , viewModel);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Edit (int id)
        {
            CancellationTokenSource cancTokenSrc = new CancellationTokenSource();
            CancellationToken cancToken = cancTokenSrc.Token;

            RestRequest getTaskRequest = new RestRequest(ApiUriInfo.Path.GetTaskById, Method.GET);
            getTaskRequest.AddParameter("id", id);
            RestClient getTaskClient = DependencyResolver.Current.GetService<RestClient>();
            var getTaskResponseTask = System.Threading.Tasks.Task.Run(() => getTaskClient.Execute(getTaskRequest) , cancToken);

            RestRequest getProjectsRequest = new RestRequest(ApiUriInfo.Path.GetProjects, Method.GET);
            getProjectsRequest.AddParameter("projectsType", nameof(ProjectType.Active));
            getProjectsRequest.AddParameter("responseDataSetType", nameof(ProjectResponseDataSetType.Header));
            RestClient getProjectsClient = DependencyResolver.Current.GetService<RestClient>();
            var getProjectsResponseTask = getProjectsClient.ExecuteTaskAsync<List<ProjectBase>>(getProjectsRequest , cancToken);


            TaskEditViewModel viewModel = new TaskEditViewModel()
            {
                ContentGenerator = new TaskEditContentGenerator()
            };
             
            var getTaskResponse = await getTaskResponseTask;
            if (getTaskResponse.IsSuccessful)
            {
                viewModel.Task = JsonConvert.DeserializeObject<TaskDetail>(getTaskResponse.Content);
            }
            else
            {
                cancTokenSrc.Cancel();
                throw new ExternalDataCirculationException("Uzyskanie danych od zewnętrznego dostawcy zakończyło się niepowodzeniem");
            }

            var getProjectResponse = await getProjectsResponseTask;
            if (getProjectResponse.IsSuccessful)
            {
                viewModel.ActiveProjects = getProjectResponse.Data;
            }
            else
            {
                cancTokenSrc.Cancel();
                throw new ExternalDataCirculationException("Uzyskanie danych od zewnętrznego dostawcy zakończyło się niepowodzeniem");
            }

            return View(viewModel);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Edit(TaskDetail task)
        {
            TaskDetailValidator validator = new TaskDetailValidator();
            ValidationResult validationResults = validator.Validate(task);

            if (!validationResults.IsValid)
            {
                RestRequest getProjectsRequest = new RestRequest(ApiUriInfo.Path.GetProjects, Method.GET);
                getProjectsRequest.AddParameter("projectsType", nameof(ProjectType.Active));
                getProjectsRequest.AddParameter("responseDataSetType", nameof(ProjectResponseDataSetType.Header));
                RestClient getProjectsClient = DependencyResolver.Current.GetService<RestClient>();
                var getProjectsResponseTask = getProjectsClient.ExecuteTaskAsync<List<ProjectBase>>(getProjectsRequest);

                foreach (var error in validationResults.Errors)
                {
                    ModelState.AddModelError(nameof(TaskEditViewModel.Task) + '.' + error.PropertyName, error.ErrorMessage);
                }

                TaskEditViewModel viewModel = new TaskEditViewModel()
                {
                    Task = task,
                    ContentGenerator = new TaskEditContentGenerator()
                };

                var getProjectsResponse = await getProjectsResponseTask;
                if (getProjectsResponse.IsSuccessful)
                {
                    viewModel.ActiveProjects = getProjectsResponse.Data;
                }
                else
                {
                    throw new ExternalDataCirculationException("Uzyskanie danych od zewnętrznego dostawcy zakończyło się niepowodzeniem");
                }
                return View(viewModel);
            }

            
            RestRequest postTaskRequest = new RestRequest(ApiUriInfo.Path.SaveTask, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            string taskAsJson = JsonConvert.SerializeObject(task);
            postTaskRequest.AddParameter("application/json", taskAsJson, ParameterType.RequestBody);

            RestClient postTaskClient = DependencyResolver.Current.GetService<RestClient>();

            var postTaskResponse = await postTaskClient.ExecuteTaskAsync(postTaskRequest);

            if (postTaskResponse.IsSuccessful)
            {
                return RedirectToAction("List");
            }
            else
            {
                throw new ExternalDataCirculationException("Uzyskanie danych od zewnętrznego dostawcy zakończyło się niepowodzeniem");
            }
        }
        
        
        public async System.Threading.Tasks.Task<ActionResult> Search(string searchPhrase)
        {
            RestClient client = DependencyResolver.Current.GetService<RestClient>();
            RestRequest request = new RestRequest(ApiUriInfo.Path.GetTasks, Method.GET);
            request.AddParameter("searchPhrase", searchPhrase);
            request.AddParameter("responseDataSetType", nameof(DataCirculationHelpers.TaskResponseDataSetType.HeaderWithPriority));
            var responseTask = client.ExecuteTaskAsync<List<TaskBaseWithPriority>>(request);

            TasksListViewModel viewModel = new TasksListViewModel()
            {
                DoneTaskActionUri = new UriBuilder(
                    ApiUriInfo.Scheme,
                    ApiUriInfo.Host,
                    ApiUriInfo.Port,
                    ApiUriInfo.Path.DoneTask
                ).ToString()
            };

            var response = await responseTask;

            if(response.IsSuccessful)
            {
                viewModel.Tasks = response.Data;
            }
            else
            {
                throw new ExternalDataCirculationException("Uzyskanie danych od zewnętrznego dostawcy zakończyło się niepowodzeniem");
            }

            return View("List", viewModel);
        }
    }
}