﻿using Application.Models;
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
    public record NewTrailRequest(int CourseId, int OrderNumber, string Title, bool IsVisible) : IRequest<NewTrailResponse>;
    public record NewTrailResponse(int TrailId);

    public class NewTrailRequestHandler : IRequestHandler<NewTrailRequest, NewTrailResponse>
    {
        private readonly ITrailRepository trailRepository;

        public NewTrailRequestHandler(ITrailRepository trailRepository)
        {
            this.trailRepository = trailRepository;
        }

        public async Task<NewTrailResponse> Handle(NewTrailRequest request, CancellationToken cancellationToken)
        {
            var trail = new Trail
            {
                CourseId = request.CourseId,
                OrderNumber = request.OrderNumber,
                Title = request.Title,
                IsVisible = request.IsVisible
            };

            var id = await trailRepository.Create(trail);

            return new NewTrailResponse(id);
        }
    }
}
