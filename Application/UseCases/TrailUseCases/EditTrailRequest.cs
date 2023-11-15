using Application.Models;
using Application.Repositories;
using Domain.Entities.CourseAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.TrailUseCases
{
    public record EditTrailRequest(int Id, int? OrderNumber, string? Title, bool? IsVisible) : IRequest<EditTrailResponse>;
    public record EditTrailResponse(Trail trail);

    public class EditTrailRequestHandler : IRequestHandler<EditTrailRequest, EditTrailResponse>
    {
        private readonly ITrailRepository trailRepository;
        public EditTrailRequestHandler(ITrailRepository trailRepository)
        {
            this.trailRepository = trailRepository;
        }
        public async Task<EditTrailResponse> Handle(EditTrailRequest request, CancellationToken cancellationToken)
        {
            var trail = await trailRepository.Get(request.Id);

            if (trail is null)
                throw new Exception("Trail not found");

            if (request.OrderNumber!= null)
                trail.OrderNumber = request.OrderNumber.Value;

            if (request.Title is not null)
                trail.Title = request.Title;

            if (request.IsVisible != null)
                trail.IsVisible = request.IsVisible.Value;

            await trailRepository.Update(trail);

            return new EditTrailResponse(trail);
        }
    }
}
