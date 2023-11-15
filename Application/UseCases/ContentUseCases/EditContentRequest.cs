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
    public record EditContentRequest(
        int Id,
        int? OrderNumber,
        string? Title, 
        ContentTypeEnum? ContentType,
        string? ContentUrl,
        bool? IsVisible) : IRequest<EditContentResponse>;

    public record EditContentResponse(Content Content);

    public class EditContentRequestHandler : IRequestHandler<EditContentRequest, EditContentResponse>
    {
        private readonly IContentRepository contentRepository;

        public EditContentRequestHandler(IContentRepository contentRepository)
        {
            this.contentRepository = contentRepository;
        }

        public async Task<EditContentResponse> Handle(EditContentRequest request, CancellationToken cancellationToken)
        {
            var content = await contentRepository.Get(request.Id);

            if (content == null)
                throw new Exception("Content not found");
            

            if (request.OrderNumber != null)
                content.OrderNumber = request.OrderNumber.Value;
            
            if (request.Title != null)
                content.Title = request.Title;

            if (request.ContentType != null)
                content.ContentType = request.ContentType.Value;

            if (request.ContentUrl != null)
                content.ContentUrl = request.ContentUrl;

            if (request.IsVisible != null)
                content.IsVisible = request.IsVisible.Value;

            await contentRepository.Update(content);

            return new EditContentResponse(content);
        }
    }
}
