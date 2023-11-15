using Domain.Entities.CourseAggregate;
using Domain.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface ICourseRepository
    {
        Task<int> Create(Course course);
        Task<bool> Update(Course course);
        Task<bool> Delete(int id);
        Task<Course?> Get(int id);
        Task<IEnumerable<Course>> GetByUser(int userId);
        Task<IEnumerable<Course>> GetAll();
    }
}
