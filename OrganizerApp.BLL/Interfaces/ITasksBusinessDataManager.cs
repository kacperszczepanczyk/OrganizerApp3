using OrganizerApp.BLL.Helpers.FilterData;
using OrganizerApp.BllDtos.Tasks;
using OrganizerApp.DalEntities.Entities;

namespace OrganizerApp.BLL.Interfaces
{
    public interface ITasksBusinessDataManager : ITaskGetActions <TaskBase , TaskGetFilteredArgs> , ITaskSetActions
    {
        
    }
}
