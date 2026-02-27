using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Security.Core.Models;
using Security.Core.Services.Interfaces;
using Security.Data.DBContext;
using Security.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Security.Core.Services
{
    public class UserService : IUserService
    {
        private readonly SecurityContext m_Context;

        public UserService(SecurityContext context)
        {
            m_Context = context;
        }

        public async Task<bool> Validate(string userName, string password)
        {
            var passwordHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
            var user = await m_Context.Users.FirstAsync(u => u.Name == userName && u.Password.Equals(passwordHash));
            return user != null;
        }

        public async Task<User?> GetUser(string userName, string password)
        {
            var passwordHash = GetHashString(password);
            var user = await m_Context.Users.FirstOrDefaultAsync(u => u.Name == userName && u.Password.Equals(passwordHash));
            return user;
        }

        public async Task<User?> GetUser(int userId)
        {
            var user = await m_Context.Users.FirstAsync(u => u.Id == userId);
            return user;
        }

        public async Task<UserModel> AddUser(UserModel user, string password)
        {
            await m_Context.Users.AddAsync(new Data.DBModels.User()
            {
                Active = true,
                LastLogin = DateTime.UtcNow,
                Name = user.Name,
                Password = GetHashString(password),
                OptionsJson = user.OptionJson,
            });

            await m_Context.SaveChangesAsync();
            return user;
        }


        private string GetHashString(string password)
        {
            var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var stringBuilder = new StringBuilder();
            stringBuilder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                stringBuilder.Append(hash[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public async Task SetUserToken(User user, string token)
        {
            user.RefreshToken = token;
            await m_Context.SaveChangesAsync();
        }

        public async Task UpdateUser(int id, Action<UpdateSettersBuilder<User>> action)
        {
           await  m_Context.Users.Where(x => x.Id == id).ExecuteUpdateAsync(action);
        }

        public async Task SaveAsync(User user)
        {
            await m_Context.SaveChangesAsync();
        }
    }
}
