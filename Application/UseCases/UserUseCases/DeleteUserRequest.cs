using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserUseCases
{
    public record DeleteUserRequest(int Id) : IRequest<DeleteUserResponse>;
    public record DeleteUserResponse();

    public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest, DeleteUserResponse>
    {
        private readonly IUserRepository userRepository;

        public DeleteUserRequestHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        async public Task<DeleteUserResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.Get(request.Id);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (!user.IsActive)
            {
                throw new Exception("User alreary deleted");
            }

            user.IsActive = false;

            await userRepository.Update(user);

            return new DeleteUserResponse();
        }
    }
}
