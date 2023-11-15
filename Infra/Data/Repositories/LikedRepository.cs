using Application.Repositories;
using Dapper;
using Domain.Entities.MetricsAggregate;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class LikedRepository : ILikedRepository
    {
        private readonly MySqlConnection _connection;
        public LikedRepository(DataContext context)
        {
            this._connection = context.connection;
        }
        public async Task<int> Create(Liked liked)
        {
            var id = await _connection.ExecuteScalarAsync<int>(
                               "INSERT INTO Likes (UserId, CourseId, LikedAt) VALUES (@UserId, @CourseId, @LikedAt)",
                                new { liked.UserId, liked.CourseId, liked.LikedAt });

            return id;
        }

        public async Task<bool> Delete(int id)
        {
            await _connection.ExecuteAsync("DELETE FROM Likes WHERE Id = @Id", new { Id = id });
            return true;
        }

        public async Task<Liked?> Get(int id)
        {
            var liked = await _connection.QueryFirstOrDefaultAsync<Liked>(
                                              "SELECT * FROM Likes WHERE Id = @Id", new { Id = id });
            return liked;
        }

        public async Task<IEnumerable<Liked>> GetAll()
        {
            var liked = await _connection.QueryAsync<Liked>("SELECT * FROM Likes");
            return liked;
        }

        public async Task<IEnumerable<Liked>> GetByCourse(int courseId)
        {
            var liked = await _connection.QueryAsync<Liked>("SELECT * FROM Likes WHERE CourseId = @CourseId", new { CourseId = courseId });
            return liked;
        }

        public async Task<IEnumerable<Liked>> GetByUser(int userId)
        {
            var likeds = await _connection.QueryAsync<Liked>("SELECT * FROM Likes WHERE UserId = @UserId", new { UserId = userId });
            return likeds;
        }

        public async Task<bool> Update(Liked liked)
        {
            await _connection.ExecuteAsync(
                                "UPDATE Likes SET UserId = @UserId, CourseId = @CourseId, LikedAt = @LikedAt WHERE Id = @Id",
                                new { liked.UserId, liked.CourseId, liked.LikedAt, liked.Id });
            return true;
        }
    }
}
