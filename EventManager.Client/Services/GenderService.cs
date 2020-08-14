using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;

namespace EventManager.Client.Services
{
    public class GenderService : HttpCall<GenderListDto, GenderDto, GenderModel>, IGenderService
    {
        public GenderService(IHttpService httpService) : base(httpService, $"{ApplicationSettings.BaseApiUrl}/gender", "Gender")
        {
        }
    }
}