using Application.Repositories;
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

        public DeleteContentRequestHandler(IContentRepository contentRepository)
        {
            this.contentRepository = contentRepository;
        }

        public async Task<DeleteContentResponse> Handle(DeleteContentRequest request, CancellationToken cancellationToken)
        {
            var content = await contentRepository.Get(request.Id);

            if (content == null)
                throw new Exception("Content not found");

            await contentRepository.Delete(request.Id);

            return new DeleteContentResponse();
        }
    }
}
