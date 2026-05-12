using Desafio3API.Models;
using Desafio3API.Repositories;

namespace Desafio3API.Services;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _userRepository.GetAllUsers();
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _userRepository.GetUserById(id);
    }

    public async Task<User?> CreateUser(User user)
    {
        var emailAlreadyExists = await _userRepository.EmailExists(user.Email);

        if (emailAlreadyExists)
        {
            return null;
        }

        return await _userRepository.CreateUser(user);
    }

    public async Task<User?> UpdateUser(int id, User updatedUser)
    {
        return await _userRepository.UpdateUser(id, updatedUser);
    }

    public async Task<bool> DeleteUser(int id)
    {
        return await _userRepository.DeleteUser(id);
    }
}