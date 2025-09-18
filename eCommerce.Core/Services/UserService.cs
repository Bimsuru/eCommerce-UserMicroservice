using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;

namespace eCommerce.Core.Services;

internal class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
    {
        // check existing user
        ApplicationUser? user = await _userRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);

        if (user == null)
        {
            return null;
        }

        // Mapped applicationUser into authenticationResponse
        return _mapper.Map<AuthenticationResponse>(user) with {Success = true, Token = "token"};
    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
    {
        // Mapped RegisterRequest into ApplicationUser
        var user = _mapper.Map<ApplicationUser>(registerRequest);

        ApplicationUser? registerdUser = await _userRepository.AddUser(user);

        if (registerdUser == null)
        {
            return null;
        }

        return _mapper.Map<AuthenticationResponse>(registerdUser) with {Success = true, Token = "token"};

    }
}
