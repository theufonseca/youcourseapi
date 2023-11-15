using Application.Models;
using Domain.Entities.CourseAggregate;

namespace Application.Repositories
{
    public interface ICourseIndex
    {
        Task CreateOrUpdateDocumentAsync(CourseModel courseModel);
        Task DeleteDocumentAsync(int id);
    }
}
