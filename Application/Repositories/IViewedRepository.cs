using Domain.Entities.MetricsAggregate;
using Domain.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IViewedRepository
    {
        Task<int> Create(Viewed viewd);
        Task<bool> Update(Viewed viewd);
        Task<bool> Delete(int id);
        Task<Viewed?> Get(int id);
        Task<IEnumerable<Viewed>> GetByCourse(int courseId);
        Task<IEnumerable<Viewed>> GetByUser(int userId);
        Task<int> GetByCoursePerUser(int courseId);
        Task<IEnumerable<Viewed>> GetAll();
    }
}
