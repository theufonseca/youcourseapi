using Application.Repositories;
using Domain.Entities.UserAggregate;
using MediatR;

namespace Application.UseCases.UserUseCases
{
    public record GetActiveUserRequest(int id) : IRequest<GetActiveUserResponse>;
    public record GetActiveUserResponse(User user);

    public class GetActiveUserRequestHandler : IRequestHandler<GetActiveUserRequest, GetActiveUserResponse>
    {
        private readonly IUserRepository userRepository;

        public GetActiveUserRequestHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        async public Task<GetActiveUserResponse> Handle(GetActiveUserRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.Get(request.id);

            if (user.IsActive == false)
            {
                throw new Exception("User is not active");
            }

            if (user == null)
            {
                throw new Exception("User not found");
            }

            return new GetActiveUserResponse(user);
        }
    }
}
