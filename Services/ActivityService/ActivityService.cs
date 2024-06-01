using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq;

namespace Maxula_project.Services.ActivityService;

public class ActivityService : IActivityService
{

    private readonly IDbContextFactory<DataContext> _contextFactory;

    private readonly IMapper _mapper;

    public ActivityService(IMapper mapper, IDbContextFactory<DataContext> contextFactory)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    private async Task<ServiceResponse<T>> HandleException<T>(Exception ex)
    {
        return new ServiceResponse<T>
        {
            Data = default,
            Success = false,
            Message = $"An error occurred: {ex.Message}"
        };
    }

    public async Task<ServiceResponse<List<GetActivityResponseDto>>> GetAllActivities(string searchString, DateOnly startDate, DateOnly endDate, int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            using var _context = _contextFactory.CreateDbContext();

            int skip = (pageNumber - 1) * pageSize;

            var q = searchString.ToLower();

            var activities = await _context.Activities
                    .Include(a => a.User)
                    .Where(a => a.Date >= startDate && a.Date <= endDate) // Filter codes based on the input dates
                     .Where(a => (a.Sector != null && a.Sector.ToLower().Contains(q))
                            || (a.DeskId != null && a.DeskId.ToString().ToLower().Contains(q))
                            || (a.User.FirstName != null && a.User.FirstName.ToLower().Contains(q))
                            || (a.User.LastName != null && a.User.LastName.ToLower().Contains(q))
                            || (a.User.Email != null && a.User.Email.ToLower().Contains(q))
                            || (a.User.Title != null && a.User.Title.ToLower().Contains(q))
                            || (a.User.Address != null && a.User.Address.ToLower().Contains(q))
                            || (a.User.Country != null && a.User.Country.ToLower().Contains(q))
                            || (a.User.ProductTeam != null && a.User.ProductTeam.ToLower().Contains(q))
                            || (a.User.Tags != null && a.User.Tags.ToLower().Contains(q))
                )
                    .OrderBy(a => a.CheckOut == null ? 0 : 1)
                    .ThenByDescending(a => a.Date)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToListAsync();

            var mappedActivities = _mapper.Map<List<GetActivityResponseDto>>(activities);

            return new ServiceResponse<List<GetActivityResponseDto>>
            {
                Data = mappedActivities,
                Success = true,
                Message = "Activities retrieved successfully."
            };
        }
        catch (Exception ex)
        {
            return await HandleException<List<GetActivityResponseDto>>(ex);
        }
    }



    public async Task<ServiceResponse<GetActivityResponseDto>> GetActivityById(int id)
    {
        try
        {
            using var _context = _contextFactory.CreateDbContext();

            var activity = await _context.Activities.FirstOrDefaultAsync(a => a.ActivityId == id);
            if (activity == null)
            {
                return new ServiceResponse<GetActivityResponseDto>
                {
                    Data = null,
                    Success = false,
                    Message = $"Activity with ID {id} not found."
                };
            }

            var mappedActivity = _mapper.Map<GetActivityResponseDto>(activity);

            return new ServiceResponse<GetActivityResponseDto>
            {
                Data = mappedActivity,
                Success = true,
                Message = "Activity retrieved successfully."
            };
        }
        catch (Exception ex)
        {
            return await HandleException<GetActivityResponseDto>(ex);
        }
    }

    public async Task<ServiceResponse<List<GetActivityResponseDto>>> GetActivitiesByUserId(int userId)
    {
        try
        {
            using var _context = _contextFactory.CreateDbContext();

            var activities = await _context.Activities.Where(a => a.UserId == userId).ToListAsync();
            var mappedActivities = _mapper.Map<List<GetActivityResponseDto>>(activities);

            return new ServiceResponse<List<GetActivityResponseDto>>
            {
                Data = mappedActivities,
                Success = true,
                Message = "Activities retrieved successfully."
            };
        }
        catch (Exception ex)
        {
            return await HandleException<List<GetActivityResponseDto>>(ex);
        }
    }

    public async Task<ServiceResponse<List<GetActivityResponseDto>>> AddActivity(AddActivityRequestDto newActivity)
    {
        try
        {
            using var _context = _contextFactory.CreateDbContext();

            var activity = _mapper.Map<Activity>(newActivity);

            await _context.Activities.AddAsync(activity);
            await _context.SaveChangesAsync();

            var activities = await _context.Activities.ToListAsync();
            var mappedActivities = _mapper.Map<List<GetActivityResponseDto>>(activities);

            return new ServiceResponse<List<GetActivityResponseDto>>
            {
                Data = mappedActivities,
                Success = true,
                Message = "Activity added successfully."
            };
        }
        catch (Exception ex)
        {
            return await HandleException<List<GetActivityResponseDto>>(ex);
        }
    }

    public async Task<ServiceResponse<List<GetActivityResponseDto>>> DeleteActivityById(int id)
    {
        try
        {
            using var _context = _contextFactory.CreateDbContext();

            var activity = await _context.Activities.FirstOrDefaultAsync(a => a.ActivityId == id);
            if (activity == null)
            {
                return new ServiceResponse<List<GetActivityResponseDto>>
                {
                    Success = false,
                    Message = $"Activity with ID {id} not found."
                };
            }

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();

            var activities = await _context.Activities.ToListAsync();
            var mappedActivities = _mapper.Map<List<GetActivityResponseDto>>(activities);

            return new ServiceResponse<List<GetActivityResponseDto>>
            {
                Data = mappedActivities,
                Success = true,
                Message = "Activity deleted successfully."
            };
        }
        catch (Exception ex)
        {
            return await HandleException<List<GetActivityResponseDto>>(ex);
        }
    }


    //StringToDateOnlyConverter: debug this 
    public async Task<ServiceResponse<List<GetActivityResponseDto>>> GetActivitiesBySearchString(DateOnly startDate, DateOnly endDate, string searchString)
    {
        try
        {
            using var _context = _contextFactory.CreateDbContext();

            var q = searchString.ToLower();

            var activities = await _context.Activities
                .Include(a => a.User)
                .Where(a => a.Date >= startDate && a.Date <= endDate) // Filter codes based on the input dates
                .Where(a => (a.Sector != null && a.Sector.ToLower().Contains(q))
                            || (a.DeskId != null && a.DeskId.ToString().ToLower().Contains(q))
                            || (a.User.FirstName != null && a.User.FirstName.ToLower().Contains(q))
                            || (a.User.LastName != null && a.User.LastName.ToLower().Contains(q))
                            || (a.User.Email != null && a.User.Email.ToLower().Contains(q))
                            || (a.User.Title != null && a.User.Title.ToLower().Contains(q))
                            || (a.User.Address != null && a.User.Address.ToLower().Contains(q))
                            || (a.User.Country != null && a.User.Country.ToLower().Contains(q))
                            || (a.User.ProductTeam != null && a.User.ProductTeam.ToLower().Contains(q))
                            || (a.User.Tags != null && a.User.Tags.ToLower().Contains(q))
                )
                .ToListAsync();

            var mappedActivities = _mapper.Map<List<GetActivityResponseDto>>(activities);

            return new ServiceResponse<List<GetActivityResponseDto>>
            {
                Data = mappedActivities,
                Success = true,
                Message = "Activities retrieved successfully."
            };
        }
        catch (Exception ex)
        {
            return await HandleException<List<GetActivityResponseDto>>(ex);
        }
    }





    //Analytics 
    public async Task<ServiceResponse<TimeOnly>> GetAverageCheckInTimeByDay(DateOnly today)
    {
        try
        {
            using var _context = _contextFactory.CreateDbContext();

            // CheckinTimes are of type TimeOnly
            var checkInTimes = await _context.Activities
                .Where(a => a.Date == today && a.CheckIn != null)
                .Select(a => a.CheckIn)
                .ToListAsync();

            if (checkInTimes.Count == 0)
            {

                return new ServiceResponse<TimeOnly>
                {
                    Data = new TimeOnly(00, 00, 00),
                    Success = true,
                    Message = "There is no activity today yet."
                };
            }

            // because TimeOnly sucks we need timespan xD
            var timeSpans = checkInTimes.Select(t => t.ToTimeSpan()).ToList();

            var averageTicks = (long)timeSpans.Average(t => t.Ticks);
            var averageTimeSpan = new TimeSpan(averageTicks);
            var averageCheckInTime = TimeOnly.FromTimeSpan(averageTimeSpan);

            return new ServiceResponse<TimeOnly>
            {
                Data = averageCheckInTime,
                Success = true,
                Message = "Average time calculated successfully."
            };
        }
        catch (Exception ex)
        {
            return await HandleException<TimeOnly>(ex);
        }
    }

    public async Task<ServiceResponse<double>> GetAverageWorkHoursByDay(DateOnly today)
    {
        try
        {
            using var _context = _contextFactory.CreateDbContext();

            // -1 to accomodate for the break hour 
            var workHours = await _context.Activities
                .Where(a => a.Date == today && a.CheckIn != null && a.CheckOut != null)
                .Select(a => (a.CheckOut!.Value - a.CheckIn).TotalHours - 1) // Use .Value to access nullable TimeSpan
                .ToListAsync();

            if (workHours.Count == 0)
            {
                return new ServiceResponse<double>
                {
                    Data = 0,
                    Success = true,
                    Message = "There is no completed work time today yet."
                };
            }

            var averageWorkHours = workHours.Average();

            return new ServiceResponse<double>
            {
                Data = averageWorkHours,
                Success = true,
                Message = "Average work hours calculated successfully."
            };
        }
        catch (Exception ex)
        {
            return await HandleException<double>(ex);
        }
    }

    public async Task<ServiceResponse<double>> GetTotalWorkHoursByWeek(DateOnly inputDate)
    {
        try
        {
            using var _context = _contextFactory.CreateDbContext();

            // Calculate the start of the week (Monday)
            int offset = inputDate.DayOfWeek - DayOfWeek.Monday;
            DateOnly startOfWeek = inputDate.AddDays(-offset);

            var endOfWeek = startOfWeek.AddDays(6);

            // -1 to accommodate for the break hour 
            var workHours = await _context.Activities
                .Where(a => a.Date >= startOfWeek && a.Date <= endOfWeek && a.CheckIn != null && a.CheckOut != null)
                .Select(a => (a.CheckOut!.Value - a.CheckIn).TotalHours - 1) // Use .Value to access nullable TimeSpan
                .ToListAsync();

            if (workHours.Count == 0)
            {
                return new ServiceResponse<double>
                {
                    Data = 0,
                    Success = true,
                    Message = "There is no completed work time this week yet."
                };
            }

            var totalWorkHours = workHours.Sum();

            return new ServiceResponse<double>
            {
                Data = totalWorkHours,
                Success = true,
                Message = "Total work hours for the week calculated successfully."
            };
        }
        catch (Exception ex)
        {
            return await HandleException<double>(ex);
        }
    }
    //QR Code methods

    public async Task<ServiceResponse<List<QrCodeModel>>> AddQRCode(QrCodeModel qr)
    {
        try
        {
            using var _context = _contextFactory.CreateDbContext();

            await _context.QRCodes.AddAsync(qr);
            await _context.SaveChangesAsync();

            var codes = await _context.QRCodes.ToListAsync();

            return new ServiceResponse<List<QrCodeModel>>
            {
                Data = codes,
                Success = true,
                Message = "QR Code added successfully."
            };
        }
        catch (Exception ex)
        {
            return await HandleException<List<QrCodeModel>>(ex);
        }
    }
    public async Task<ServiceResponse<List<QrCodeModel>>> GetAllQRCodes(string query)
    {
        try
        {
            using var _context = _contextFactory.CreateDbContext();

            var codes = await _context.QRCodes
                .Where(qr => qr.Sector.Contains(query))
                .ToListAsync();

            return new ServiceResponse<List<QrCodeModel>>
            {
                Data = codes,
                Success = true,
                Message = "QR Codes retrieved successfully."
            };
        }
        catch (Exception ex)
        {
            return await HandleException<List<QrCodeModel>>(ex);
        }
    }


}
