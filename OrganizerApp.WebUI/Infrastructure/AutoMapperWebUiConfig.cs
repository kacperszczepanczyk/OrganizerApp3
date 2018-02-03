using AutoMapper;
using OrganizerApp.BllDtos.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizerApp.WebUI.Infrastructure
{
    public static class AutoMapperWebUiConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap <TaskDetailWithoutProjectRef, TaskBaseWithPriority>();
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}