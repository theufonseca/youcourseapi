using Domain.Entities.CourseAggregate;
using Domain.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface ICoursesFollowedsRepository
    {
        Task<int> Create(CourseFollowed courseFollowed);
        Task<bool> Update(CourseFollowed courseFollowed);
        Task<bool> Delete(int id);
        Task<CourseFollowed?> Get(int id);
        Task<IEnumerable<CourseFollowed>> GetByCourse(int courseId);
        Task<IEnumerable<CourseFollowed>> GetByUser(int userId);
        Task<IEnumerable<CourseFollowed>> GetAll();
    }
}
