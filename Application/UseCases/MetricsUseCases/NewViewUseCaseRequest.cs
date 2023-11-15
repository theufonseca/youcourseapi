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
    public record NewViewUseCaseRequest(int IdUser, int IdCourse) : IRequest<NewViewUseCaseResponse>;
    public record NewViewUseCaseResponse();

    public class NewViewUseCaseRequestHandler : IRequestHandler<NewViewUseCaseRequest, NewViewUseCaseResponse>
    {
        private readonly IViewedRepository viewedRepository;

        public NewViewUseCaseRequestHandler(IViewedRepository viewedRepository)
        {
            this.viewedRepository = viewedRepository;
        }

        public async Task<NewViewUseCaseResponse> Handle(NewViewUseCaseRequest request, CancellationToken cancellationToken)
        {
            var view = new Viewed
            {
                CourseId = request.IdCourse,
                UserId = request.IdUser,
                ViewedAt = DateTime.Now
            };

            await viewedRepository.Create(view);
            return new NewViewUseCaseResponse();
        }
    }
}
