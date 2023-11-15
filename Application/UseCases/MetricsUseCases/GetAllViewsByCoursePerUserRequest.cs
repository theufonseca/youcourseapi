using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.MetricsUseCases
{
    public record GetAllViewsByCoursePerUserRequest(int CourseId) : IRequest<GetAllViewsByCoursePerUserResponse>;
    public record GetAllViewsByCoursePerUserResponse(int UniqueViews);

    public class GetAllViewsByCoursePerUserRequestHandler : IRequestHandler<GetAllViewsByCoursePerUserRequest, GetAllViewsByCoursePerUserResponse>
    {
        private readonly IViewedRepository viewedRepository;

        public GetAllViewsByCoursePerUserRequestHandler(IViewedRepository viewedRepository)
        {
            this.viewedRepository = viewedRepository;
        }

        public async Task<GetAllViewsByCoursePerUserResponse> Handle(GetAllViewsByCoursePerUserRequest request, CancellationToken cancellationToken)
        {
            var views = await viewedRepository.GetByCoursePerUser(request.CourseId);
            return new GetAllViewsByCoursePerUserResponse(views);
        }
    }
}
