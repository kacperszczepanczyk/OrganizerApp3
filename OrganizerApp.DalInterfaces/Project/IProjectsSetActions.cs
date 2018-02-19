using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerApp.DalInterfaces.Project
{
    public interface IProjectsSetActions <T>
    {
        void Remove(int id);
        void Save(T entity, params Func<T, string>[] modifiedPropertyNames);
    }
}
