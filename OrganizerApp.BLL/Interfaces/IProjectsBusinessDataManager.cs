using OrganizerApp.BLL.Helpers.FilterData;
using OrganizerApp.BllDtos.Projects;
using OrganizerApp.DalEntities.Entities;

namespace OrganizerApp.BLL.Interfaces
{
    public interface IProjectsBusinessDataManager : IProjectGetActions<ProjectBase , ProjectGetFilteredArgs> , IProjectSetActions
    {
        
    }
}
