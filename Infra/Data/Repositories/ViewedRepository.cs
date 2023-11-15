using Application.Repositories;
using Dapper;
using Domain.Entities.MetricsAggregate;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class ViewedRepository : IViewedRepository
    {
        private readonly MySqlConnection _connection;
        public ViewedRepository(DataContext context)
        {
            _connection = context.connection;
        }

        public async Task<int> Create(Viewed viewd)
        {
            var id = await _connection.ExecuteScalarAsync<int>(
                               "INSERT INTO Views (UserId, CourseId, ViewedAt) VALUES (@UserId, @CourseId, @ViewedAt)",
                                new { viewd.UserId, viewd.CourseId, viewd.ViewedAt });

            return id;
        }

        public async Task<bool> Delete(int id)
        {
            await _connection.ExecuteAsync("DELETE FROM Views WHERE Id = @Id", new { Id = id });
            return true;
        }

        public async Task<Viewed?> Get(int id)
        {
            var viewed = await _connection.QueryFirstOrDefaultAsync<Viewed>(
                               "SELECT * FROM Views WHERE Id = @Id", new { Id = id });
            return viewed;
        }

        public async Task<IEnumerable<Viewed>> GetAll()
        {
            var vieweds = await _connection.QueryAsync<Viewed>("SELECT * FROM Views");
            return vieweds;
        }

        public async Task<IEnumerable<Viewed>> GetByCourse(int courseId)
        {
            var vieweds = await _connection.QueryAsync<Viewed>("SELECT * FROM Views WHERE CourseId = @CourseId", new { CourseId = courseId });
            return vieweds;
        }

        public async Task<IEnumerable<Viewed>> GetByUser(int userId)
        {
            var vieweds = await _connection.QueryAsync<Viewed>("SELECT * FROM Views WHERE UserId = @UserId", new { UserId = userId });
            return vieweds;
        }

        public async Task<int> GetByCoursePerUser(int courseId)
        {
            var vieweds = await _connection
                .ExecuteScalarAsync<int>("select count(distinct UserId) from Views where CourseId = @CourseId", new { CourseId = courseId });
            return vieweds;
        }

        public async Task<bool> Update(Viewed viewd)
        {
            await _connection.ExecuteAsync(
                            "UPDATE Views SET UserId = @UserId, CourseId = @CourseId, ViewedAt = @ViewedAt WHERE Id = @Id",
                            new { viewd.UserId, viewd.CourseId, viewd.ViewedAt, viewd.Id });
            return true;
        }
    }
}
