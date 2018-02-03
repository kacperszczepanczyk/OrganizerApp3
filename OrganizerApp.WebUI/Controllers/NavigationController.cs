using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Collections.Specialized;
using OrganizerApp.WebUI.Constants;
using OrganizerApp.BllDtos.Projects;
using OrganizerApp.DataCirculationHelpers;
using RestSharp;
using System.Threading.Tasks;
using OrganizerApp.WebUI.Infrastructure;

namespace OrganizerApp.WebUI.Controllers
{
    public class NavigationController : Controller
    {
        public PartialViewResult Index()
        {
            RestClient client = DependencyResolver.Current.GetService<RestClient>();
            RestRequest request = new RestRequest(ApiUriInfo.Path.GetProjects , Method.GET);
            request.AddParameter("projectsType", nameof(ProjectType.Active));
            request.AddParameter("responseDataSetType", nameof(ProjectResponseDataSetType.Header));
            var responseTask = Task.Run(() => client.Execute<List<ProjectBase>>(request));

            IEnumerable<ProjectBase> viewModel = null;

            var response = responseTask.Result;

            if(response.IsSuccessful)
            {
                viewModel = responseTask.Result.Data;
            }
            else
            {
                throw new ExternalDataCirculationException("Uzyskanie danych od zewnętrznego dostawcy zakończyło się niepowodzeniem");
            }

            return PartialView(viewModel);
        }
    }
}
 