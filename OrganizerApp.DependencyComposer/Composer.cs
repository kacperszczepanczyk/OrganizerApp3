using Ninject;
using OrganizerApp.DAL.Contexts;
using OrganizerApp.DAL.Interfaces;
using OrganizerApp.DAL.Repositorys;
using OrganizerApp.DalEntities.Entities;
using OrganizerApp.DalInterfaces;
using OrganizerApp.DalInterfaces.Project;
using OrganizerApp.DalInterfaces.Task;
using System.Linq;

namespace DependencyComposer
{
    public class Composer : IComposer
    {
        public void Bind(IKernel kernel)
        {
            kernel.Bind<ITasksRepository>().To<TasksRepository>();
            kernel.Bind<IProjectsRepository>().To<ProjectsRepository>();
            kernel.Bind<AbstractDbContext>().To<StandardDbContext>();
        }
    }
}
