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
    public record FollowCourseRequest(int IdCourse, int IdUser) : IRequest<FollowCourseResponse>;
    public record FollowCourseResponse(int Id);

    public class FollowCourseRequestHandler : IRequestHandler<FollowCourseRequest, FollowCourseResponse>
    {
        private readonly ICoursesFollowedsRepository coursesFollowedsRepository;

        public FollowCourseRequestHandler(ICoursesFollowedsRepository coursesFollowedsRepository)
        {
            this.coursesFollowedsRepository = coursesFollowedsRepository;
        }

        public async Task<FollowCourseResponse> Handle(FollowCourseRequest request, CancellationToken cancellationToken)
        {
            var userAreadyFollowCourse = await coursesFollowedsRepository.GetByUser(request.IdUser);

            if(userAreadyFollowCourse is not null && userAreadyFollowCourse.Any())
            {
                var followedCourseFound = userAreadyFollowCourse.FirstOrDefault(x => x.CourseId == request.IdCourse);

                if(followedCourseFound != null && followedCourseFound.IsFollow)
                    throw new Exception("User already follow a course");
                else if(followedCourseFound != null && !followedCourseFound.IsFollow)
                {
                    followedCourseFound.IsFollow = true;
                    await coursesFollowedsRepository.Update(followedCourseFound);
                    return new FollowCourseResponse(followedCourseFound.Id);
                }
            }
            
            var followedCourse = new CourseFollowed
            {
                CourseId = request.IdCourse,
                UserId = request.IdUser,
                StartFollowAt = DateTime.Now,
                IsFollow = true,
                Progress = 0,
                IsFinished = false
            };

            var id = await coursesFollowedsRepository.Create(followedCourse);

            return new FollowCourseResponse(id);
        }
    }
}
