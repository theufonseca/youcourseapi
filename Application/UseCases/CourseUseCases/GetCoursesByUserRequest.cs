using Application.Repositories;
using Domain.Entities.UserAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.CourseUseCases
{
    public record GetCoursesByUserRequest(int UserId) : IRequest<GetCoursesByUserResponse>;
    public record GetCoursesByUserResponse(User User);

    public class GetCoursesByUserRequestHandler : IRequestHandler<GetCoursesByUserRequest, GetCoursesByUserResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly ICourseRepository courseRepository;
        private readonly ITrailRepository trailRepository;
        private readonly IContentRepository contentRepository;

        public GetCoursesByUserRequestHandler(
            IUserRepository userRepository,
            ICourseRepository courseRepository,
            ITrailRepository trailRepository,
            IContentRepository contentRepository)
        {
            this.userRepository = userRepository;
            this.courseRepository = courseRepository;
            this.trailRepository = trailRepository;
            this.contentRepository = contentRepository;
        }

        public async Task<GetCoursesByUserResponse> Handle(GetCoursesByUserRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.Get(request.UserId);

            if (user == null)
                throw new Exception("User not found");

            var courses = await courseRepository.GetByUser(request.UserId);

            if (courses == null || !courses.Any())
                return new GetCoursesByUserResponse(user);

            foreach (var course in courses)
            {
                var trails = await trailRepository.GetByCourse(course.Id);

                if(trails == null || !trails.Any())
                    continue;

                course.Trails = trails;

                foreach (var trail in course.Trails)
                {
                    var contents = await contentRepository.GetByTrail(trail.Id);

                    if(contents == null || !contents.Any())
                        continue;

                    trail.Contents = contents;
                }
            }

            user.Courses = courses;

            return new GetCoursesByUserResponse(user);
        }
    }
}
