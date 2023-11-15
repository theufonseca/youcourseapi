using Application.Repositories;
using Application.Services.NotifyService;
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
        private readonly INotifyChangeService notifyChangeService;

        public DeleteCourseRequestHandler(ICourseRepository courseRepository, INotifyChangeService notifyChangeService)
        {
            this.courseRepository = courseRepository;
            this.notifyChangeService = notifyChangeService;
        }

        public async Task<DeleteCourseResponse> Handle(DeleteCourseRequest request, CancellationToken cancellationToken)
        {
            await courseRepository.Delete(request.Id);
            await notifyChangeService.NotifyCourseChangeAsync(request.Id, NotifyOperationEnum.Delete);
            return new DeleteCourseResponse();
        }
    }
}
