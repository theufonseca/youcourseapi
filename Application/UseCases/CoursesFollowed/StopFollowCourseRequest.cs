using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.CoursesFollowed
{
    public record StopFollowCourseRequest(int IdCourseFollowed) : IRequest<StopFollowCourseResponse>;
    public record StopFollowCourseResponse();

    public class StopFollowCourseRequestHandler : IRequestHandler<StopFollowCourseRequest, StopFollowCourseResponse>
    {
        private readonly ICoursesFollowedsRepository coursesFollowedsRepository;

        public StopFollowCourseRequestHandler(ICoursesFollowedsRepository coursesFollowedsRepository)
        {
            this.coursesFollowedsRepository = coursesFollowedsRepository;
        }

        public async Task<StopFollowCourseResponse> Handle(StopFollowCourseRequest request, CancellationToken cancellationToken)
        {
            var courseFollowed = await coursesFollowedsRepository.Get(request.IdCourseFollowed);    

            if(courseFollowed is null) 
                throw new Exception("Course followed not found");

            courseFollowed.IsFollow = false;
            await coursesFollowedsRepository.Update(courseFollowed);

            return new StopFollowCourseResponse();
        }
    }
}
