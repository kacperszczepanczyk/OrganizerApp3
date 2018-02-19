using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerApp.DalInterfaces.Project
{
    public interface IProjectsGetActions <T , V , Z>
    {
        T GetById(int id);
        V GetFiltered(Z arguments);
    }
}

