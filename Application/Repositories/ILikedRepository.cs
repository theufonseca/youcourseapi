using Domain.Entities.MetricsAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface ILikedRepository
    {
        Task<int> Create(Liked liked);
        Task<bool> Update(Liked liked);
        Task<bool> Delete(int id);
        Task<Liked?> Get(int id);
        Task<IEnumerable<Liked>> GetByCourse(int courseId);
        Task<IEnumerable<Liked>> GetByUser(int userId);
        Task<IEnumerable<Liked>> GetAll();
    }
}
