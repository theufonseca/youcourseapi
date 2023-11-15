using Application.Repositories;
using Application.Services.NotifyService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.ContentUseCases
{
    public record DeleteContentRequest(int Id) : IRequest<DeleteContentResponse>;
    public record DeleteContentResponse();

    public class DeleteContentRequestHandler : IRequestHandler<DeleteContentRequest, DeleteContentResponse>
    {
        private readonly IContentRepository contentRepository;
        private readonly INotifyChangeService notifyChangeService;

        public DeleteContentRequestHandler(IContentRepository contentRepository, INotifyChangeService notifyChangeService)
        {
            this.contentRepository = contentRepository;
            this.notifyChangeService = notifyChangeService;
        }

        public async Task<DeleteContentResponse> Handle(DeleteContentRequest request, CancellationToken cancellationToken)
        {
            var content = await contentRepository.Get(request.Id);

            if (content == null)
                throw new Exception("Content not found");

            await contentRepository.Delete(request.Id);
            await notifyChangeService.NotifyContentChangeAsync(content.Id, NotifyOperationEnum.Delete);

            return new DeleteContentResponse();
        }
    }
}
