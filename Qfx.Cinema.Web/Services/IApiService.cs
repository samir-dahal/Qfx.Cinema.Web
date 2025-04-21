using Refit;

namespace Qfx.Cinema.Web.Services
{
    public interface IApiService
    {
        [Get("/api/cinema/admin/upcoming-show-list?limit={limit}&currentPage={page}&rtk=true")]
        Task<string> UpcomingShows(
            [AliasAs("limit")] int limit = 1000,
            [AliasAs("page")] int page = 1
            );

        [Post("/api/external/cinemas")]
        Task<string> Cinemas();

        [Get("/api/cinema/admin/now-showing-confirmed-list/bffv1?city_id={cityId}")]
        Task<string> NowShowing([AliasAs("cityId")] int cityId);
    }
}
