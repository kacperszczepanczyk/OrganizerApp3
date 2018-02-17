using OrganizerApp.BllDtos.Projects;
using OrganizerApp.DataCirculationHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrganizerApp.WebUI.Helpers.Api.OrganizerApp.Projects
{
    public interface IOrganizerAppProjectApiRequestHandler
    {
        Task<IEnumerable<ProjectBaseWithPriorityAndState>> GetProjectsHeadersWithPriorityAndState(string searchPhrase = null, ProjectType projectsType = ProjectType.All);
        Task<IEnumerable<ProjectBase>> GetProjectsHeaders(string searchPhrase = null, ProjectType projectsType = ProjectType.All);
        Task<ProjectDetail> GetProjectById(int id);
        Task SaveProject<T>(T objectToSerialize);
        Task<List<ProjectBaseWithPriorityAndState>> SearchProjects(string searchPhrase);
    }
}
