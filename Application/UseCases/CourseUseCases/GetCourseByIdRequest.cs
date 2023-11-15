using Application.Repositories;
using Domain.Entities.CourseAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.CourseUseCases
{
    public record GetCourseByIdRequest(int Id) : IRequest<GetCourseByIdResponse>;
    public record GetCourseByIdResponse(Course course);

    public class GetCourseByIdRequestHandler : IRequestHandler<GetCourseByIdRequest, GetCourseByIdResponse>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ITrailRepository _trailRepository;
        private readonly IContentRepository _contentRepository;

        public GetCourseByIdRequestHandler(ICourseRepository courseRepository, ITrailRepository trailRepository,
            IContentRepository contentRepository)
        {
            _courseRepository = courseRepository;
            _trailRepository = trailRepository;
            _contentRepository = contentRepository;
        }

        public async Task<GetCourseByIdResponse> Handle(GetCourseByIdRequest request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.Get(request.Id);

            if (course == null)
                throw new Exception("Course not found");

            var trails = await _trailRepository.GetByCourse(course.Id);

            if (trails == null || !trails.Any())
                return new GetCourseByIdResponse(course);

            foreach (var trail in trails)
            {
                var contents = await _contentRepository.GetByTrail(trail.Id);

                if (contents == null || !contents.Any())
                    continue;

                trail.Contents = contents;
            }

            course.Trails = trails;

            return new GetCourseByIdResponse(course);
        }
    }

}
