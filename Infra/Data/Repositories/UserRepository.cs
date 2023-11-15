using Application.Repositories;
using Dapper;
using Domain.Entities.UserAggregate;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MySqlConnection _connection;

        public UserRepository(DataContext dataContext)
        {
            this._connection = dataContext.connection;
        }

        public async Task<int> Create(User user)
        {
             var newId = await _connection.ExecuteScalarAsync<int>(
                 "INSERT INTO Users(Name, Email, Password, ProfileDescription, ProfilePicture, CreatedDate, IsActive) " +
                 "VALUES (@Name, @Email, @Password, @ProfileDescription, @ProfilePicture, @CreatedDate, @IsActive); SELECT LAST_INSERT_ID()",
                new
                {
                    user.Name,
                    user.Email,
                    user.Password,
                    user.ProfileDescription,
                    user.ProfilePicture,
                    user.CreatedDate,
                    user.IsActive
                });

            return newId;
        }

        public async Task<bool> Delete(int id)
        {
            await _connection.ExecuteAsync("DELETE FROM Users WHERE Id = @Id", new { id });
            return true;
        }

        public async Task<User?> Get(int id)
        {
            var user = await _connection
                .QueryAsync<User>("" +
                "SELECT " +
                "Id, Name, Email, Password, ProfileDescription, ProfilePicture, CreatedDate, IsActive " +
                "FROM Users " +
                "WHERE Id = @Id",
                new { Id = id });

            return user.FirstOrDefault();
        }

        public async Task<User?> Get(string email)
        {
            var user = await _connection.QueryAsync<User>("" +
                    "SELECT " +
                    "Id, Name, Email, Password, ProfileDescription, ProfilePicture, CreatedDate, IsActive " +
                    "FROM Users " +
                    "WHERE Email = @Email",
                    new { Email = email });

            return user.FirstOrDefault();
        }

        public Task<IEnumerable<User>> GetAll()
        {
            var users = _connection.QueryAsync<User>("" +
                    "SELECT " +
                    "Id, Name, Email, Password, ProfileDescription, ProfilePicture, CreatedDate, IsActive " +
                    "FROM Users");

            return users;
        }

        public async Task<bool> Update(User user)
        {
            await _connection.ExecuteAsync(
                "UPDATE Users " +
                "SET Name = @Name, Email = @Email, Password = @Password, ProfileDescription = @ProfileDescription, ProfilePicture = @ProfilePicture, CreatedDate = @CreatedDate, IsActive = @IsActive " +
                "WHERE Id = @Id",
                new
                {
                    user.Id,
                    user.Name,
                    user.Email,
                    user.Password,
                    user.ProfileDescription,
                    user.ProfilePicture,
                    user.CreatedDate,
                    user.IsActive
                });

            return true;
        }
    }
}
