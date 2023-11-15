using Application.Models;
using Application.Repositories;
using Application.Services.NotifyService;
using Domain.Entities.CourseAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.TrailUseCases
{
    public record DeleteTrailRequest(int Id) : IRequest<DeleteTrailResponse>;
    public record DeleteTrailResponse();

    public class DeleteTrailRequestHandler : IRequestHandler<DeleteTrailRequest, DeleteTrailResponse>
    {
        private readonly ITrailRepository trailRepository;
        private readonly INotifyChangeService notifyChangeService;

        public DeleteTrailRequestHandler(ITrailRepository trailRepository, INotifyChangeService notifyChangeService)
        {
            this.trailRepository = trailRepository;
            this.notifyChangeService = notifyChangeService;
        }

        public async Task<DeleteTrailResponse> Handle(DeleteTrailRequest request, CancellationToken cancellationToken)
        {
            await trailRepository.Delete(request.Id);
            await notifyChangeService.NotifyTrailChangeAsync(request.Id, NotifyOperationEnum.Delete);
            return new DeleteTrailResponse();
        }
    }
}
