using Application.Repositories;
using Dapper;
using Domain.Entities.CourseAggregate;
using Domain.Entities.UserAggregate;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Infra.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly MySqlConnection _connection;
        public CourseRepository(DataContext context)
        {
            _connection = context.connection;
        }


        public async Task<int> Create(Course course)
        {
            var id = await _connection.ExecuteScalarAsync<int>("INSERT INTO Courses (UserId, Name, Description, ThumbImage, Tags, CreatedDate, IsActive) VALUES (@UserId, @Name, @Description, @ThumbImage, @Tags, @CreatedDate, @IsActive); SELECT LAST_INSERT_ID()", course);
            return id;
        }

        public async Task<bool> Delete(int id)
        {
            await _connection.ExecuteAsync("DELETE FROM Courses WHERE Id = @Id", new { id });
            return true;
        }

        public async Task<Course?> Get(int id)
        {
            var course = await _connection.QueryAsync<Course>("" +
                                "SELECT " +
                                "Id, UserId, Name, Description, ThumbImage, Tags, CreatedDate, IsActive " +
                                "FROM Courses " +
                                "WHERE Id = @Id",
                                new { Id = id });

            return course.FirstOrDefault();
        }

        public Task<IEnumerable<Course>> GetAll()
        {
            var courses = _connection.QueryAsync<Course>("" +
                                "SELECT " +
                                "Id, UserId, Name, Description, ThumbImage, Tags, CreatedDate, IsActive " +
                                "FROM Courses ");

            return courses;
        }

        public Task<IEnumerable<Course>> GetByUser(int userId)
        {
            var courses = _connection.QueryAsync<Course>("" +
                                "SELECT " +
                                "Id, UserId, Name, Description, ThumbImage, Tags, CreatedDate, IsActive " +
                                "FROM Courses " +
                                "WHERE UserId = @UserId",
                                new { UserId = userId });

            return courses;
        }

        public async Task<bool> Update(Course course)
        {
            await _connection.ExecuteAsync("UPDATE Courses SET Name = @Name, Description = @Description, ThumbImage = @ThumbImage, Tags = @Tags, CreatedDate = @CreatedDate, IsActive = @IsActive WHERE Id = @Id", course);
            return true;
        }
    }
}
