using System;
using AutoMapper;
using OrganizerApp.DalEntities.Entities;
using OrganizerApp.BllDtos.Tasks;
using OrganizerApp.BllDtos.Projects;

namespace OrganizerApp.BLL.Infrastructure
{
    public static class AutoMapperBllConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Task, TaskDetail>();
                config.CreateMap<TaskDetail, Task>();
                config.CreateMap<Task, TaskDetailWithoutProjectRef>();
                config.CreateMap<TaskDetailWithoutProjectRef, Task>()
                    .ForMember(x => x.Project , opt => opt.Ignore());
                config.CreateMap<Task, TaskBase>();
                config.CreateMap<TaskBase, Task>()
                    .ForMember(x => x.Priority, opt => opt.Ignore())
                    .ForMember(x => x.Description, opt => opt.Ignore())
                    .ForMember(x => x.Context, opt => opt.Ignore())
                    .ForMember(x => x.ExecutionTime, opt => opt.Ignore())
                    .ForMember(x => x.StartTime, opt => opt.Ignore())
                    .ForMember(x => x.State, opt => opt.Ignore())
                    .ForMember(x => x.ProjectID, opt => opt.Ignore())
                    .ForMember(x => x.Project, opt => opt.Ignore());

                config.CreateMap<Task, TaskBaseWithPriority>();
                config.CreateMap<TaskBaseWithPriority, Task>()
                    .ForMember(x => x.Description, opt => opt.Ignore())
                    .ForMember(x => x.Context, opt => opt.Ignore())
                    .ForMember(x => x.ExecutionTime, opt => opt.Ignore())
                    .ForMember(x => x.StartTime, opt => opt.Ignore())
                    .ForMember(x => x.State, opt => opt.Ignore())
                    .ForMember(x => x.ProjectID, opt => opt.Ignore())
                    .ForMember(x => x.Project, opt => opt.Ignore());

                config.CreateMap<Project, ProjectDetail>();
                config.CreateMap<ProjectDetail, Project>();
                config.CreateMap<Project, ProjectBase>();
                config.CreateMap<ProjectBase, Project>()
                    .ForMember(x => x.Priority, opt => opt.Ignore())
                    .ForMember(x => x.Description, opt => opt.Ignore())
                    .ForMember(x => x.ExecutionTime, opt => opt.Ignore())
                    .ForMember(x => x.StartTime, opt => opt.Ignore())
                    .ForMember(x => x.State, opt => opt.Ignore())
                    .ForMember(x => x.ProjectTasks, opt => opt.Ignore());

                config.CreateMap<Project, ProjectBaseWithPriority>();
                config.CreateMap<ProjectBaseWithPriority, Project>()
                    .ForMember(x => x.Description, opt => opt.Ignore())
                    .ForMember(x => x.ExecutionTime, opt => opt.Ignore())
                    .ForMember(x => x.StartTime, opt => opt.Ignore())
                    .ForMember(x => x.State, opt => opt.Ignore())
                    .ForMember(x => x.ProjectTasks, opt => opt.Ignore());

                config.CreateMap<Project, ProjectBaseWithPriorityAndState>();
                config.CreateMap<ProjectBaseWithPriorityAndState, Project>()
                    .ForMember(x => x.Description, opt => opt.Ignore())
                    .ForMember(x => x.ExecutionTime, opt => opt.Ignore())
                    .ForMember(x => x.StartTime, opt => opt.Ignore())
                    .ForMember(x => x.ProjectTasks, opt => opt.Ignore());

                config.CreateMap<Project, ProjectDetailWithoutTasksRef>();
                config.CreateMap<ProjectDetailWithoutTasksRef, Project>()
                    .ForMember(x => x.ProjectTasks, opt => opt.Ignore());
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
