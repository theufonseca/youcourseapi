using Application.Repositories;
using Domain.Entities.CourseAggregate;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.ContentUseCases
{
    public record CreateContentRequest(
        int TrailId,
        int OrderNumber,
        string Title,
        ContentTypeEnum ContentType,
        string ContentUrl,
        bool IsVisible) : IRequest<CreateContentResponse>;

    public record CreateContentResponse(int ContentId);

    public class CreateContentRequestHandler : IRequestHandler<CreateContentRequest, CreateContentResponse>
    {
        private readonly IContentRepository contentRepository;

        public CreateContentRequestHandler(IContentRepository contentRepository)
        {
            this.contentRepository = contentRepository;
        }

        public async Task<CreateContentResponse> Handle(CreateContentRequest request, CancellationToken cancellationToken)
        {
            var content = new Content
            {
                TrailId = request.TrailId,
                OrderNumber = request.OrderNumber,
                Title = request.Title,
                ContentType = request.ContentType,
                ContentUrl = request.ContentUrl,
                IsVisible = request.IsVisible
            };

            var id = await contentRepository.Create(content);

            return new CreateContentResponse(id);
        }
    }
}