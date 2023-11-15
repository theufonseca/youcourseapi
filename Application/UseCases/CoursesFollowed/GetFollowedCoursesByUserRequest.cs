using Application.Repositories;
using Domain.Entities.UserAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.CoursesFollowed
{
    public record GetFollowedCoursesByUserRequest(int IdUser) : IRequest<GetFollowedCoursesByUserResponse>;
    public record GetFollowedCoursesByUserResponse(IEnumerable<CourseFollowed> CourseFolloweds);

    public class GetFollowedCoursesByUserRequestHandler : IRequestHandler<GetFollowedCoursesByUserRequest, GetFollowedCoursesByUserResponse>
    {
        private readonly ICoursesFollowedsRepository coursesFollowedsRepository;

        public GetFollowedCoursesByUserRequestHandler(ICoursesFollowedsRepository coursesFollowedsRepository)
        {
            this.coursesFollowedsRepository = coursesFollowedsRepository;
        }
        public async Task<GetFollowedCoursesByUserResponse> Handle(GetFollowedCoursesByUserRequest request, CancellationToken cancellationToken)
        {
            var coursesFolloweds = await coursesFollowedsRepository.GetByUser(request.IdUser);
            return new GetFollowedCoursesByUserResponse(coursesFolloweds.Where(x => x.IsFollow).ToList());
        }
    }
}
