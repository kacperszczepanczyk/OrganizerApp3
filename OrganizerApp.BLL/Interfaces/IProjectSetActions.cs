using OrganizerApp.BllDtos.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerApp.BLL.Interfaces
{
    public interface IProjectSetActions
    {
        void Save(ProjectDetail project);
        void Deactivate(int id);
        void Remove(int id);
        void Done(int id);
    }
}
