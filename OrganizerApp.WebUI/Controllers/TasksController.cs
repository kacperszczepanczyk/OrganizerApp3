using System;
using OrganizerApp.WebUI.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Collections.Specialized;
using System.Web;
using Newtonsoft.Json;
using OrganizerApp.BllDtos.Tasks;
using OrganizerApp.BllDtos.Projects;
using FluentValidation.Results;
using OrganizerApp.WebUI.Infrastructure.Validation;
using OrganizerApp.WebUI.Helpers.View.ContentGenerator.Implementations;
using OrganizerApp.DataCirculationHelpers;
using OrganizerApp.WebUI.Helpers.Api.OrganizerApp.Tasks;
using OrganizerApp.WebUI.Helpers.Api.OrganizerApp.Projects;

namespace OrganizerApp.WebUI.Controllers
{
    public class TasksController : Controller
    {
        private IOrganizerAppTaskApiRequestHandler _taskApiRequestHandler;
        private IOrganizerAppProjectApiRequestHandler _projectApiRequestHandler;


        public TasksController(IOrganizerAppTaskApiRequestHandler apiRequestHandler , IOrganizerAppProjectApiRequestHandler projectApiRequestHandler)
        {
            _taskApiRequestHandler = apiRequestHandler;
            _projectApiRequestHandler = projectApiRequestHandler;
        }


        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> List(string timeType , int? projectID , string searchPhrase , string date , TaskType tasksType = TaskType.All)
        {
            var getTasksTask = _taskApiRequestHandler.GetTasksHeadersWithPriority(timeType , projectID , searchPhrase , date , tasksType);
            TasksListViewModel viewModel = new TasksListViewModel();
            viewModel.Tasks = await getTasksTask;

            return View(viewModel);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Create()
        {
            var getProjectsTask = _projectApiRequestHandler.GetProjectsHeaders(projectsType: ProjectType.Active);
            TaskEditViewModel viewModel = new TaskEditViewModel()
            {
                Task = new TaskDetail { ID = 0 }
            };
            viewModel.ActiveProjects = await getProjectsTask;

            return View("Edit", viewModel);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Edit (int id)
        {
            var getProjectsTask = _projectApiRequestHandler.GetProjectsHeaders(projectsType: ProjectType.Active);
            var getTaskTask = _taskApiRequestHandler.GetTaskById(id);

            TaskEditViewModel viewModel = new TaskEditViewModel();
            viewModel.ActiveProjects = await getProjectsTask;
            viewModel.Task = await getTaskTask;

            return View(viewModel);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Edit(TaskDetail task)
        {

            TaskDetailValidator validator = new TaskDetailValidator();
            ValidationResult validationResults = validator.Validate(task);

            if (!validationResults.IsValid)
            {
                var getProjectsTask = _projectApiRequestHandler.GetProjectsHeaders(projectsType: ProjectType.Active);

                foreach (var error in validationResults.Errors)
                {
                    ModelState.AddModelError(nameof(TaskEditViewModel.Task) + '.' + error.PropertyName, error.ErrorMessage);
                }

                TaskEditViewModel viewModel = new TaskEditViewModel()
                {
                    Task = task,
                };
                viewModel.ActiveProjects = await getProjectsTask;
                
                return View(viewModel);
            }

            await _taskApiRequestHandler.SaveTask(task);

            return RedirectToAction("List");
        }


        public async System.Threading.Tasks.Task<ActionResult> Search(string searchPhrase)
        {
            var searchTasksTask = _taskApiRequestHandler.SearchTasks(searchPhrase);
            TasksListViewModel viewModel = new TasksListViewModel();
            viewModel.Tasks = await searchTasksTask;

            return View("List", viewModel);
        }
    }
}