using Domain.Entities.CourseAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface ITrailRepository
    {
        Task<int> Create(Trail trail);
        Task<bool> Update(Trail trail);
        Task<bool> Delete(int id);
        Task<Trail?> Get(int id);
        Task<IEnumerable<Trail>> GetByCourse(int courseId);
        Task<IEnumerable<Trail>> GetAll();
    }
}
