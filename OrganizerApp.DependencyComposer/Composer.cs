using Ninject;
using OrganizerApp.DAL.Contexts;
using OrganizerApp.DAL.Interfaces;
using OrganizerApp.DAL.Repositorys;
using OrganizerApp.DalEntities.Entities;
using OrganizerApp.Dalnterfaces;
using System.Linq;

namespace DependencyComposer
{
    public class Composer : IComposer
    {
        public void Bind(IKernel kernel)
        {
            kernel.Bind<IRepository<IQueryable<Project>, IQueryable<Project> , Project>>().To<ProjectsRepository>();
            kernel.Bind<IRepository<IQueryable<Task>, IQueryable<Task> , Task>>().To<TasksRepository>();
            kernel.Bind<AbstractDbContext>().To<StandardDbContext>();
        }
    }
}
