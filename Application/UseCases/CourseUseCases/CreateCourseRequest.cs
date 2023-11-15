using Application.Models;
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
    public record CreateCourseRequest(
        int UserId, 
        string CourseName, 
        string Description, 
        string ThumbImage, 
        string Tags) : IRequest<CreateCourseResponse>;

    public record CreateCourseResponse(int CourseId);

    public class CreateCourseHandler : IRequestHandler<CreateCourseRequest, CreateCourseResponse>
    {
        private readonly ICourseRepository courseRepository;

        public CreateCourseHandler(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async Task<CreateCourseResponse> Handle(CreateCourseRequest request, CancellationToken cancellationToken)
        {
            var course = new Course
            {
                Name = request.CourseName,
                Description = request.Description,
                ThumbImage = request.ThumbImage,
                Tags = request.Tags,
                UserId = request.UserId,
                CreatedDate = DateTime.Now,
                IsActive = false
            };

            var courseId = await courseRepository.Create(course);

            return new CreateCourseResponse(courseId);
        }
    }   
}
