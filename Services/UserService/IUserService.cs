namespace Maxula_project.Services.UserService;

public interface IUserService
{
    Task<ServiceResponse<List<GetUserResponseDto>>> GetAllUsers();
    Task<ServiceResponse<GetUserResponseDto>> GetUserById(int id);
    Task<ServiceResponse<GetUserResponseDto>> GetUserByString(string targetString);
    Task<ServiceResponse<GetUserResponseDto>> AddUser(AddUserRequestDto newUser);
    Task<ServiceResponse<List<GetUserResponseDto>>> DeleteUserById(int id);
    Task<ServiceResponse<GetUserResponseDto>> Login(string email, string password);
    Task<ServiceResponse<string>> Verify(string token);

}
