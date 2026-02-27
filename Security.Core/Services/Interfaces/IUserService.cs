using Microsoft.EntityFrameworkCore.Query;
using Security.Core.Models;
using Security.Data.DBModels;

namespace Security.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> AddUser(UserModel user, string password);
        Task<User?> GetUser(string userName, string password);
        Task<User?> GetUser(int userId);
        Task SetUserToken(User user, string token);
        Task<bool> Validate(string userName, string password);
        Task UpdateUser(int id, Action<UpdateSettersBuilder<User>> action);
        Task SaveAsync(User user);
    }
}