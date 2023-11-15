using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.MetricsUseCases
{
    public record GetAllViewsByCourseRequest(int courseId) : IRequest<GetAllViewsByCourseResponse>;
    public record GetAllViewsByCourseResponse(int views);

    public class GetAllViewsByCourseRequestHandler : IRequestHandler<GetAllViewsByCourseRequest, GetAllViewsByCourseResponse>
    {
        private readonly IViewedRepository viewedRepository;

        public GetAllViewsByCourseRequestHandler(IViewedRepository viewedRepository)
        {
            this.viewedRepository = viewedRepository;
        }

        async public Task<GetAllViewsByCourseResponse> Handle(GetAllViewsByCourseRequest request, CancellationToken cancellationToken)
        {
            var views = await viewedRepository.GetByCourse(request.courseId);
            return new GetAllViewsByCourseResponse(views.Count());
        }
    }
}
