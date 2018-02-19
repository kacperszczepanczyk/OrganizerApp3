using OrganizerApp.BllDtos.Projects;
using OrganizerApp.DataCirculationHelpers;

namespace OrganizerApp.BLL.Interfaces
{
    public interface IProjectsBusinessDataManager : IProjectGetActions<ProjectBase , ProjectGetFilteredArgs> , IProjectSetActions
    {
        
    }
}
