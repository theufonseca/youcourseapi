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
    public record NewLikeRequest(int CourseId, int UserId) : IRequest<NewLikeResponse>;
    public record NewLikeResponse();
    
    public class NewLikeRequestHandler : IRequestHandler<NewLikeRequest, NewLikeResponse>
    {
        private readonly ILikedRepository likedRepository;

        public NewLikeRequestHandler(ILikedRepository likedRepository)
        {
            this.likedRepository = likedRepository;
        }

        async public Task<NewLikeResponse> Handle(NewLikeRequest request, CancellationToken cancellationToken)
        {
            var userAreadyLiked = await likedRepository.GetByUser(request.UserId);

            if (userAreadyLiked.Any())
                return new NewLikeResponse();

            var like = new Liked
            {
                CourseId = request.CourseId,
                UserId = request.UserId,
                LikedAt = DateTime.Now
            };

            await likedRepository.Create(like);
            return new NewLikeResponse();
        }
    }
}
