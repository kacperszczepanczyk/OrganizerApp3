using OrganizerApp.BllDtos.Tasks;
using OrganizerApp.DataCirculationHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerApp.WebUI.Helpers.Api.OrganizerApp.Tasks
{
    public interface IOrganizerAppTaskApiRequestHandler
    {
        Task<IEnumerable<TaskBaseWithPriority>> GetTasksHeadersWithPriority(string timeType = null, int? projectID = null, string searchPhrase = null, string date = null, TaskType tasksType = TaskType.All);
        Task<TaskDetail> GetTaskById(int id);
        Task SaveTask<T>(T objectToSerialize);
        Task<List<TaskBaseWithPriority>> SearchTasks(string searchPhrase);
    }
}
