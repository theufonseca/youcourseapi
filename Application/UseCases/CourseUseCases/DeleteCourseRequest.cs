using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.CourseUseCases
{
    public record DeleteCourseRequest(int Id) : IRequest<DeleteCourseResponse>;

    public record DeleteCourseResponse();

    public class DeleteCourseRequestHandler : IRequestHandler<DeleteCourseRequest, DeleteCourseResponse>
    {
        private readonly ICourseRepository courseRepository;

        public DeleteCourseRequestHandler(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async Task<DeleteCourseResponse> Handle(DeleteCourseRequest request, CancellationToken cancellationToken)
        {
            await courseRepository.Delete(request.Id);
            return new DeleteCourseResponse();
        }
    }
}
