using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Maxula_project.Services.EmailService;

namespace Maxula_project.Services.UserService;
public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public UserService(IMapper mapper, DataContext context)
    {
        _context = context;
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

    private string HashPassword(string plainPassword)
    {
        return BCrypt.Net.BCrypt.HashPassword(plainPassword);
    }

    public async Task<ServiceResponse<List<GetUserResponseDto>>> GetAllUsers()
    {
        try
        {
            var dbLoginQrCodes = await _context.Users.ToListAsync();
            var mappedQrCodes = _mapper.Map<List<GetUserResponseDto>>(dbLoginQrCodes);

            return new ServiceResponse<List<GetUserResponseDto>>
            {
                Data = mappedQrCodes,
                Success = true,
                Message = "Users retrieved successfully."
            };
        }
        catch (Exception ex)
        {
            return await HandleException<List<GetUserResponseDto>>(ex);
        }
    }

    public async Task<ServiceResponse<GetUserResponseDto>> GetUserById(int id)
    {
        try
        {
            var dbQrCode = await _context.Users.FirstOrDefaultAsync(qr => qr.UserId == id);
            if (dbQrCode is null)
            {
                return new ServiceResponse<GetUserResponseDto>
                {
                    Data = default,
                    Success = false,
                    Message = "User not found"
                };
            }
            var mappedQrCode = _mapper.Map<GetUserResponseDto>(dbQrCode);

            return new ServiceResponse<GetUserResponseDto>
            {
                Data = mappedQrCode,
                Success = true,
                Message = "User retrieved by ID successfully."
            };
        }
        catch (Exception ex)
        {
            return await HandleException<GetUserResponseDto>(ex);
        }
    }

    public async Task<ServiceResponse<GetUserResponseDto>> GetUserByString(string targetString)
    {
        try
        {
            var targetUser = await _context.Users.FirstOrDefaultAsync(user => user.Email == targetString);
            if (targetUser is null)
            {
                return new ServiceResponse<GetUserResponseDto>
                {
                    Data = null,
                    Success = false,
                    Message = "User not found"
                };
            }

            var mappedQrCode = _mapper.Map<GetUserResponseDto>(targetUser);

            return new ServiceResponse<GetUserResponseDto>
            {
                Data = mappedQrCode,
                Success = true,
                Message = "User retrieved successfully."
            };
        }
        catch (Exception ex)
        {
            return await HandleException<GetUserResponseDto>(ex);
        }
    }

    public async Task<ServiceResponse<GetUserResponseDto>> AddUser(AddUserRequestDto newUser)
    {
        try
        {
            //mapping user to an object
            var user = _mapper.Map<User>(newUser);

            // Adding hashed password
            string hashedPassword = HashPassword(newUser.Password);
            user.Password = hashedPassword;

            user.VerificationToken = CreateRandomToken();

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var updatedUser = await _context.Users.FirstOrDefaultAsync(user => user.Email == newUser.Email);

            var updatedUserResponse = _mapper.Map<GetUserResponseDto>(updatedUser);


            return new ServiceResponse<GetUserResponseDto>
            {
                Data = updatedUserResponse,
                Success = true,
                Message = "User added successfully."
            };
        }
        catch (Exception ex)
        {
            return await HandleException<GetUserResponseDto>(ex);
        }
    }

    private static string CreateRandomToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }

    public async Task<ServiceResponse<List<GetUserResponseDto>>> DeleteUserById(int id)
    {
        try
        {
            var loginQrCode = await _context.Users.FirstOrDefaultAsync(qr => qr.UserId == id);

            if (loginQrCode is null)
            {
                throw new Exception($"User with ID '{id}' not found");
            }

            _context.Users.Remove(loginQrCode);
            await _context.SaveChangesAsync();

            return new ServiceResponse<List<GetUserResponseDto>>
            {
                Success = true,
                Message = "User deleted successfully."
            };
        }
        catch (Exception ex)
        {
            return await HandleException<List<GetUserResponseDto>>(ex);
        }
    }

    public async Task<ServiceResponse<GetUserResponseDto>> Login(string email, string password)
    {
        try
        {
            var targetUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception("User not found");

            if (!BCrypt.Net.BCrypt.Verify(password, targetUser.Password))
            {

                return new ServiceResponse<GetUserResponseDto>
                {
                    Data = null,
                    Success = false,
                    Message = "Password is incorrect."
                };
            }

            if (targetUser.VerifiedAt is null)
            {
                return new ServiceResponse<GetUserResponseDto>
                {
                    Data = null,
                    Success = false,
                    Message = "User is not verified."
                };
            }

            var updatedUserResponse = _mapper.Map<GetUserResponseDto>(targetUser);
            return new ServiceResponse<GetUserResponseDto>
            {
                Data = updatedUserResponse,
                Success = true,
                Message = "Password is correct, login authorized."
            };
        }
        catch (Exception ex)
        {
            return await HandleException<GetUserResponseDto>(ex);
        }
    }

    public async Task<ServiceResponse<string>> Verify(string token)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);

        if (user == null)
        {
            return new ServiceResponse<string>
            {
                Data = "Invalid token.",
                Success = false,
                Message = "Invalid token."
            };
        }

        user.VerifiedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new ServiceResponse<string>
        {
            Data = "User verified.",
            Success = true,
            Message = "User verified."
        };
    }
}

