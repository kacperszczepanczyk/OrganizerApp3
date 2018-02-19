using OrganizerApp.DataCirculationHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizerApp.DalEntities;

namespace OrganizerApp.DalInterfaces.Project
{
    public interface IProjectsRepository : IProjectsSetActions<DalEntities.Entities.Project> , IProjectsGetActions<DalEntities.Entities.Project , IEnumerable<DalEntities.Entities.Project> , ProjectGetFilteredArgs>
    {

    }
}
