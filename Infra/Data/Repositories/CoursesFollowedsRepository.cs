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
    public class CoursesFollowedsRepository : ICoursesFollowedsRepository
    {
        private readonly MySqlConnection connection;

        public CoursesFollowedsRepository(DataContext context)
        {
            connection = context.connection;
        }

        public async Task<int> Create(CourseFollowed courseFollowed)
        {
            var id = await connection
                .ExecuteAsync("INSERT INTO CoursesFolloweds " +
                "(UserId, CourseId, IsFollow, Progress, IsFinished, StartFollowAt) VALUES " +
                "(@UserId, @CourseId, @IsFollow, @Progress, @IsFinished, @StartFollowAt); SELECT LAST_INSERT_ID();",
                courseFollowed);

            return id;
        }
        
        public async Task<bool> Delete(int id)
        {
            await connection.ExecuteAsync("DELETE FROM CoursesFolloweds WHERE Id = @Id", new { Id = id });
            return true;
        }

        public async Task<CourseFollowed?> Get(int id)
        {
            var coursesFolloweds = await connection.QueryFirstOrDefaultAsync<CourseFollowed>("SELECT * FROM CoursesFolloweds WHERE Id = @Id", new { Id = id });
            return coursesFolloweds;
        }

        public async Task<IEnumerable<CourseFollowed>> GetAll()
        {
            var coursesFolloweds = await connection.QueryAsync<CourseFollowed>("SELECT * FROM CoursesFolloweds");
            return coursesFolloweds;
        }

        public async Task<IEnumerable<CourseFollowed>> GetByCourse(int courseId)
        {
            var coursesFolloweds = await connection.QueryAsync<CourseFollowed>("SELECT * FROM CoursesFolloweds WHERE CourseId = @CourseId", new { CourseId = courseId });
            return coursesFolloweds;
        }

        public async Task<IEnumerable<CourseFollowed>> GetByUser(int userId)
        {
            var coursesFolloweds = await connection.QueryAsync<CourseFollowed>("SELECT * FROM CoursesFolloweds WHERE UserId = @UserId", new { UserId = userId });
            return coursesFolloweds;
        }

        public async Task<bool> Update(CourseFollowed courseFollowed)
        {
            await connection.ExecuteAsync("UPDATE CoursesFolloweds " +
                "SET UserId = @UserId, CourseId = @CourseId, IsFollow = @IsFollow, Progress = @Progress, IsFinished = @IsFinished, StartFollowAt = @StartFollowAt " +
                "WHERE Id = @Id", courseFollowed);

            return true;
        }
    }
}
