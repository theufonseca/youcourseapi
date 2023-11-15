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
    public record UpdateUserInfoRequest(int Id, string? Name, string? ProfileDescription, string? ProfilePicture, bool? IsActive) : IRequest<UpdateUserInfoResponse>;

    public record UpdateUserInfoResponse(User user);

    public class UpdateUserInfoRequestHandler : IRequestHandler<UpdateUserInfoRequest, UpdateUserInfoResponse>
    {
        private readonly IUserRepository userRepository;

        public UpdateUserInfoRequestHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        async public Task<UpdateUserInfoResponse> Handle(UpdateUserInfoRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.Get(request.Id);

            if (user == null)
            {
                throw new Exception("User not found");
            }
            
            if (request.Name is not null)
                user.Name = request.Name;

            if (request.ProfileDescription is not null)
                user.ProfileDescription = request.ProfileDescription;

            if (request.ProfilePicture is not null)
                user.ProfilePicture = request.ProfilePicture;

            if (request.IsActive is not null)
                user.IsActive = request.IsActive.Value;

            await userRepository.Update(user);

            return new UpdateUserInfoResponse(user);
        }
    }   
}
