using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerApp.DalInterfaces
{
    public interface ISetDataActions <T>
    {
        void Remove(int id);
        void Save(T entity, params Func<T, string>[] modifiedPropertyNames);
    }
}
