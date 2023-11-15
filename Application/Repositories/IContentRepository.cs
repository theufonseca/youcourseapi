using Domain.Entities.CourseAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IContentRepository
    {
        Task<int> Create(Content content);
        Task<bool> Update(Content content);
        Task<bool> Delete(int id);
        Task<Content?> Get(int id);
        Task<IEnumerable<Content>> GetByTrail(int trailId);
        Task<IEnumerable<Content>> GetAll();
    }
}
