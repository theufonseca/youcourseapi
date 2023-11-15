using Application.Repositories;
using Domain.Entities.UserAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserUseCases
{
    public record LoginUserRequest(string Email, string Password) : IRequest<LoginUserResponse>;
    public record LoginUserResponse(User? user);

    public class LoginUserHandler : IRequestHandler<LoginUserRequest, LoginUserResponse>
    {
        private readonly IUserRepository userRepository;

        public LoginUserHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.Get(request.Email);

            if (user?.Password != request.Password)
                return new LoginUserResponse(null);
            
            return new LoginUserResponse(user);
        }
    }
}
