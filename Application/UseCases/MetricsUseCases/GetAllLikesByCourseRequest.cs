using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.MetricsUseCases
{
    public record GetAllLikesByCourseRequest(int IdCourse) : IRequest<GetAllLikesByCourseResponse>;
    public record GetAllLikesByCourseResponse(int Likes);

    public class GetAllLikesByCourseRequestHandler : IRequestHandler<GetAllLikesByCourseRequest, GetAllLikesByCourseResponse>
    {
        private readonly ILikedRepository likedRepository;

        public GetAllLikesByCourseRequestHandler(ILikedRepository likedRepository)
        {
            this.likedRepository = likedRepository;
        }

        async public Task<GetAllLikesByCourseResponse> Handle(GetAllLikesByCourseRequest request, CancellationToken cancellationToken)
        {
            var likes = await likedRepository.GetByCourse(request.IdCourse);
            return new GetAllLikesByCourseResponse(likes.Count());
        }
    }
}
