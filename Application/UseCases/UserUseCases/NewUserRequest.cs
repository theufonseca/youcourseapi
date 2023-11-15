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
    public record NewUserRequest(string Name, string Email, string Password) : IRequest<NewUserResponse>;
    public record NewUserResponse(int id);

    public class NewUserRequestHandler : IRequestHandler<NewUserRequest, NewUserResponse>
    {
        private readonly IUserRepository userRepository;

        public NewUserRequestHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<NewUserResponse> Handle(NewUserRequest request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            var userAlreadyExists = await userRepository.Get(user.Email);

            if (userAlreadyExists is not null)
                throw new Exception("User already exists");

            var userId = await userRepository.Create(user);
            return new NewUserResponse(userId);
        }
    }
}
