using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Services
{
    public class SeriesCommentService : HttpCall<SeriesCommentListDto, SeriesCommentDto, SeriesCommentModel>, ISeriesCommentService
    {
        public SeriesCommentService(IHttpService http) : base(http, $"{ApplicationSettings.BaseApiUrl}/series-comment", "Series Comment")
        {
        }

        public async Task<List<SeriesCommentListDto>> GetList(int movieÍd)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(movieÍd, -1);

            var settings = new HttpSettings($"{this.Url}/series", null, pathParams);

            return await this.Http.Get<List<SeriesCommentListDto>>(settings);
        }
    }
}