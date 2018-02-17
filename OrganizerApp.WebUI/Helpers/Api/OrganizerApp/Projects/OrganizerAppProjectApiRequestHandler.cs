using OrganizerApp.BllDtos.Projects;
using OrganizerApp.DataCirculationHelpers;
using OrganizerApp.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OrganizerApp.WebUI.Helpers.Api.OrganizerApp.Projects
{
    public class OrganizerAppProjectApiRequestHandler : IOrganizerAppProjectApiRequestHandler
    {
        private IApiRequestHandler _apiRequestHandler;


        public OrganizerAppProjectApiRequestHandler(IApiRequestHandler apiRequestHandler) 
        {
            _apiRequestHandler = apiRequestHandler;
        }


        public async Task<ProjectDetail> GetProjectById(int id)
        {
            string getProjectByIdUri = ApiUriBuilder.GetProjectByIdUri.ToString();
            NameValueCollection parameters = new NameValueCollection()
            {
                { "id" , id.ToString() } ,
            };

            return await _apiRequestHandler.ExecuteGetAsync<ProjectDetail>(getProjectByIdUri, parameters);
        }

        public async Task SaveProject<T>(T objectToSerialize)
        {
            string saveProjectUri = ApiUriBuilder.SaveProjectUri.ToString();
            await _apiRequestHandler.ExecutePostAsync(saveProjectUri , objectToSerialize);
        }

        public async Task<List<ProjectBaseWithPriorityAndState>> SearchProjects(string searchPhrase)
        {
            return await GetProjects<List<ProjectBaseWithPriorityAndState>>(searchPhrase, ProjectType.All, ProjectResponseDataSetType.HeaderWithPriorityAndState);
        }

        public async Task<IEnumerable<ProjectBaseWithPriorityAndState>> GetProjectsHeadersWithPriorityAndState(string searchPhrase = null, ProjectType projectsType = ProjectType.All)
        {
            return await GetProjects<List<ProjectBaseWithPriorityAndState>>(searchPhrase, projectsType , ProjectResponseDataSetType.HeaderWithPriorityAndState);
        }

        public async Task<IEnumerable<ProjectBase>> GetProjectsHeaders(string searchPhrase = null, ProjectType projectsType = ProjectType.All)
        {
            return await GetProjects<List<ProjectBase>>(searchPhrase, projectsType , ProjectResponseDataSetType.Header);
        }

        private async Task<T> GetProjects<T>(string searchPhrase, ProjectType projectsType , ProjectResponseDataSetType projectResponseType) where T : new()
        {
            string getProjectsUri = ApiUriBuilder.GetProjectsUri.ToString();
            NameValueCollection parameters = new NameValueCollection()
            {
                { "searchPhrase" , searchPhrase } ,
                { "responseDataSetType" , projectResponseType.GetEnumName() } ,
                { "projectsType" , projectsType.GetEnumName() }
            };

            return await _apiRequestHandler.ExecuteGetAsync<T>(getProjectsUri, parameters);
        }
    }
}