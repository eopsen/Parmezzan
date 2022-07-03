using Parmezzan.Web.Models;
using Parmezzan.Web.Models.Dto;

namespace Parmezzan.Web.Services.IServices
{
    public interface IBaseService: IDisposable
    {
        ResponseDto responseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
