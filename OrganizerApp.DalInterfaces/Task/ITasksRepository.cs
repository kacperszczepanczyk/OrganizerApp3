using OrganizerApp.DataCirculationHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerApp.DalInterfaces.Task
{
    public interface ITasksRepository : ITasksSetActions<DalEntities.Entities.Task>, ITasksGetActions<DalEntities.Entities.Task, IEnumerable<DalEntities.Entities.Task>, TaskGetFilteredArgs>
    {
    }
}
