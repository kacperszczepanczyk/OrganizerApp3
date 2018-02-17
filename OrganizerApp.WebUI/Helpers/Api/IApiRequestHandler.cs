using System.Collections.Specialized;
using System.Threading.Tasks;

namespace OrganizerApp.WebUI.Helpers.Api
{
    public interface IApiRequestHandler
    {
        Task<T> ExecuteGetAsync<T>(string resourceUri, NameValueCollection parameters = null) where T : new();
        Task ExecutePostAsync<T>(string resourceUri, T objectToSerialize);
    }
}