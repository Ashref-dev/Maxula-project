using AutoMapper;

namespace Maxula_project;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, GetUserResponseDto>();
        CreateMap<AddUserRequestDto, User>();

        CreateMap<Activity, GetActivityResponseDto>();
        CreateMap<AddActivityRequestDto, Activity>();

    }
}
