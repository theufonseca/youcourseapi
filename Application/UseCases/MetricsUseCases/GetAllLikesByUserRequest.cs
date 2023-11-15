using Application.Repositories;
using Domain.Entities.MetricsAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.MetricsUseCases
{
    public record GetAllLikesByUserRequest(int IdUser) : IRequest<GetAllLikesByUserResponse>;
    public record GetAllLikesByUserResponse(IEnumerable<Liked> Likes);

    public class GetAllLikesByUserRequestHandler : IRequestHandler<GetAllLikesByUserRequest, GetAllLikesByUserResponse>
    {
        private readonly ILikedRepository likedRepository;

        public GetAllLikesByUserRequestHandler(ILikedRepository likedRepository)
        {
            this.likedRepository = likedRepository;
        }

        async public Task<GetAllLikesByUserResponse> Handle(GetAllLikesByUserRequest request, CancellationToken cancellationToken)
        {
            var likes = await likedRepository.GetByUser(request.IdUser);
            return new GetAllLikesByUserResponse(likes);
        }
    }
}
