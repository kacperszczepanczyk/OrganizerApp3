using OrganizerApp.WebUI.Models;
using OrganizerApp.BllDtos.Projects;
using OrganizerApp.WebUI.Infrastructure.Validation;
using FluentValidation.Results;
using OrganizerApp.WebUI.Helpers.View.ContentGenerator.Implementations;
using OrganizerApp.DataCirculationHelpers;
using System.Web.Mvc;
using System.Threading.Tasks;
using OrganizerApp.WebUI.Helpers.Api.OrganizerApp.Projects;

namespace OrganizerApp.WebUI.Controllers
{
    public class ProjectsController : Controller
    {
        private IOrganizerAppProjectApiRequestHandler _apiRequestHandler;


        public ProjectsController(IOrganizerAppProjectApiRequestHandler apiRequestHandler)
        {
            _apiRequestHandler = apiRequestHandler;
        }


        [HttpGet]
        public async Task<ActionResult> List(string searchPhrase, ProjectType projectsType = ProjectType.All)
        {
            var getProjectsTask = _apiRequestHandler.GetProjectsHeadersWithPriorityAndState(searchPhrase, projectsType);
            ProjectsListViewModel viewModel = new ProjectsListViewModel();
            viewModel.Projects = await getProjectsTask;

            return View(viewModel);
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
            var getProjectTask = _apiRequestHandler.GetProjectById(id);
            ProjectEditViewModel viewModel = new ProjectEditViewModel();
            viewModel.Project = await getProjectTask;

            return View(viewModel);
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
                };

                foreach (var error in validationResults.Errors)
                {
                    ModelState.AddModelError(nameof(ProjectEditViewModel.Project) + '.' + error.PropertyName, error.ErrorMessage);
                }

                return View(viewModel);
            }

            await _apiRequestHandler.SaveProject(project);
            return RedirectToAction("List");
        }

        
        public async Task<ActionResult> Search(string searchPhrase)
        {
            var searchProjectsTask = _apiRequestHandler.SearchProjects(searchPhrase);
            ProjectsListViewModel viewModel = new ProjectsListViewModel();
            viewModel.Projects = await searchProjectsTask;

            return View("List" , viewModel);
        }
    }
}