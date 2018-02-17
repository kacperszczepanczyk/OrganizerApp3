using System;

namespace OrganizerApp.WebUI.Helpers.Api.OrganizerApp
{
    public static class ApiUriBuilder
    {
        public static readonly Uri DeactivateTaskUri = new UriBuilder(
            ApiUriInfo.Scheme,
            ApiUriInfo.Host,
            ApiUriInfo.Port,
            ApiUriInfo.Path.DeactivateTask
        ).Uri;

        public static readonly Uri DoneTaskUri = new UriBuilder(
            ApiUriInfo.Scheme,
            ApiUriInfo.Host,
            ApiUriInfo.Port,
            ApiUriInfo.Path.DoneTask
        ).Uri;

        public static readonly Uri DoneProjectUri = new UriBuilder(
            ApiUriInfo.Scheme,
            ApiUriInfo.Host,
            ApiUriInfo.Port,
            ApiUriInfo.Path.DoneProject
        ).Uri;

        public static readonly Uri RemoveTaskUri = new UriBuilder(
            ApiUriInfo.Scheme,
            ApiUriInfo.Host,
            ApiUriInfo.Port,
            ApiUriInfo.Path.RemoveTask
        ).Uri;

        public static readonly Uri RemoveProjectUri = new UriBuilder(
            ApiUriInfo.Scheme,
            ApiUriInfo.Host,
            ApiUriInfo.Port,
            ApiUriInfo.Path.RemoveProject
        ).Uri;

        public static readonly Uri GetTaskByIdUri = new UriBuilder(
            ApiUriInfo.Scheme,
            ApiUriInfo.Host,
            ApiUriInfo.Port,
            ApiUriInfo.Path.GetTaskById
        ).Uri;

        public static readonly Uri GetTasksUri = new UriBuilder(
            ApiUriInfo.Scheme,
            ApiUriInfo.Host,
            ApiUriInfo.Port,
            ApiUriInfo.Path.GetTasks
        ).Uri;

        public static readonly Uri SaveTaskUri = new UriBuilder(
            ApiUriInfo.Scheme,
            ApiUriInfo.Host,
            ApiUriInfo.Port,
            ApiUriInfo.Path.SaveTask
        ).Uri;

        public static readonly Uri GetProjectsUri = new UriBuilder(
            ApiUriInfo.Scheme,
            ApiUriInfo.Host,
            ApiUriInfo.Port,
            ApiUriInfo.Path.GetProjects
        ).Uri;

        public static readonly Uri GetProjectByIdUri = new UriBuilder(
            ApiUriInfo.Scheme,
            ApiUriInfo.Host,
            ApiUriInfo.Port,
            ApiUriInfo.Path.GetProjectById
        ).Uri;

        public static readonly Uri SaveProjectUri = new UriBuilder(
            ApiUriInfo.Scheme,
            ApiUriInfo.Host,
            ApiUriInfo.Port,
            ApiUriInfo.Path.SaveProject
        ).Uri;
    }
}