
namespace Maxula_project.Services.ActivityService
{
    public interface IActivityService
    {
        Task<ServiceResponse<List<GetActivityResponseDto>>> GetAllActivities(string searchString, DateOnly startDate, DateOnly endDate, int pageNumber = 1, int pageSize = 10);
        Task<ServiceResponse<GetActivityResponseDto>> GetActivityById(int id);
        Task<ServiceResponse<List<GetActivityResponseDto>>> GetActivitiesByUserId(int userId);
        Task<ServiceResponse<List<GetActivityResponseDto>>> AddActivity(AddActivityRequestDto newActivity);
        Task<ServiceResponse<List<GetActivityResponseDto>>> DeleteActivityById(int id);
        Task<ServiceResponse<List<GetActivityResponseDto>>> GetActivitiesBySearchString(DateOnly startDate, DateOnly endDate, string searchString);
        Task<ServiceResponse<TimeOnly>> GetAverageCheckInTimeByDay(DateOnly today);
        Task<ServiceResponse<double>> GetAverageWorkHoursByDay(DateOnly today);
        Task<ServiceResponse<double>> GetTotalWorkHoursByWeek(DateOnly inputDate);
        Task<ServiceResponse<List<QrCodeModel>>> AddQRCode(QrCodeModel qr);
        Task<ServiceResponse<List<QrCodeModel>>> GetAllQRCodes(string query);

    }
}
