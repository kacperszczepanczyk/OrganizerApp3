using OrganizerApp.BllDtos.Tasks;
using OrganizerApp.DataCirculationHelpers;

namespace OrganizerApp.BLL.Interfaces
{
    public interface ITasksBusinessDataManager : ITaskGetActions <TaskBase , TaskGetFilteredArgs> , ITaskSetActions
    {
        
    }
}
