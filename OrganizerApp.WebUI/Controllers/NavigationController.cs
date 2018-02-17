using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Collections.Specialized;
using OrganizerApp.BllDtos.Projects;
using OrganizerApp.DataCirculationHelpers;
using RestSharp;
using System.Threading.Tasks;
using OrganizerApp.WebUI.Helpers.Api.OrganizerApp.Projects;

namespace OrganizerApp.WebUI.Controllers
{
    public class NavigationController : Controller
    {
        private IOrganizerAppProjectApiRequestHandler _apiRequestHandler;


        public NavigationController(IOrganizerAppProjectApiRequestHandler apiRequestHandler)
        {
            _apiRequestHandler = apiRequestHandler;
        }


        public PartialViewResult Index()
        {
            var getProjectsTask = Task.Run(() => _apiRequestHandler.GetProjectsHeaders(projectsType: ProjectType.Active));
            IEnumerable<ProjectBase> viewModel = new List<ProjectBase>();
            viewModel = getProjectsTask.GetAwaiter().GetResult();

            return PartialView(viewModel);
        }
    }
}
 