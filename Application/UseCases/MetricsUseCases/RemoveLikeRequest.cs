using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.MetricsUseCases
{
    public record RemoveLikeRequest(int Id) : IRequest<RemoveLikeResponse>;
    public record RemoveLikeResponse();

    public class RemoveLikeRequestHandler : IRequestHandler<RemoveLikeRequest, RemoveLikeResponse>
    {
        private readonly ILikedRepository likedRepository;

        public RemoveLikeRequestHandler(ILikedRepository likedRepository)
        {
            this.likedRepository = likedRepository;
        }

        async public Task<RemoveLikeResponse> Handle(RemoveLikeRequest request, CancellationToken cancellationToken)
        {
            await likedRepository.Delete(request.Id);
            return new RemoveLikeResponse();
        }
    }
}
