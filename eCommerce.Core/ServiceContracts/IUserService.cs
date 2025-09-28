using eCommerce.Core.DTO;

namespace eCommerce.Core.ServiceContracts;

public interface IUserService
{
    /// <summary>
    /// Method to handle user login use case and returns an AuthenticationResponse object that contains status of login
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    Task<AuthenticationResponse?> Login(LoginRequest loginRequest);

    /// <summary>
    /// Method to handle user register use case and returns an AuthenticationResponse object that contains status of register
    /// </summary>
    /// <param name="registerRequest"></param>
    /// <returns></returns>
    Task<AuthenticationResponse?> Register(RegisterRequest registerRequest);

    /// <summary>
    /// Get user service method
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    Task<UserResponse?> GetUserById(Guid userid);
}
