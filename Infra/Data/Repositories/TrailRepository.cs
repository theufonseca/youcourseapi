using Application.Repositories;
using Dapper;
using Domain.Entities.CourseAggregate;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class TrailRepository : ITrailRepository
    {
        private readonly MySqlConnection _connection;

        public TrailRepository(DataContext context)
        {
            _connection = context.connection;
        }

        public async Task<int> Create(Trail trail)
        {
            var id = await _connection.ExecuteScalarAsync<int>("INSERT INTO Trails (CourseId, OrderNumber, Title, IsVisible) VALUES (@CourseId, @OrderNumber, @Title, @IsVisible); SELECT LAST_INSERT_ID();", trail);
            return id;
        }

        public async Task<bool> Delete(int id)
        {
            await _connection.ExecuteAsync("DELETE FROM Trails WHERE Id = @Id", new { Id = id });
            return true;
        }


        public async Task<bool> Update(Trail trail)
        {
            await _connection.ExecuteAsync("UPDATE Trails SET CourseId = @CourseId, OrderNumber = @OrderNumber, Title = @Title, IsVisible = @IsVisible WHERE Id = @Id", trail);
            return true;
        }

        public async Task<Trail?> Get(int id)
        {
            var trail = await _connection.QueryAsync<Trail>("SELECT * FROM Trails WHERE Id = @Id", new { Id = id });
            return trail.FirstOrDefault();
        }

        public async Task<IEnumerable<Trail>> GetAll()
        {
            var trails = await _connection.QueryAsync<Trail>("SELECT * FROM Trails");
            return trails;
        }

        public async Task<IEnumerable<Trail>> GetByCourse(int courseId)
        {
            var trails = await _connection.QueryAsync<Trail>("SELECT * FROM Trails WHERE CourseId = @CourseId", new { CourseId = courseId });
            return trails;
        }

    }
}
