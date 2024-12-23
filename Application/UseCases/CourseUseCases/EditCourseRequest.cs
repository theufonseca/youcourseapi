﻿using Application.Models;
using Application.Repositories;
using Application.Services.NotifyService;
using Domain.Entities.CourseAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.CourseUseCases
{
    public record EditCourseRequest(
        int Id,
        string Name,
        string Description,
        string ThumbImage,
        string Tags,
        bool IsActive) : IRequest<EditCourseResponse>;

    public record EditCourseResponse(Course Course);

    public class EditCourseRequestHandler : IRequestHandler<EditCourseRequest, EditCourseResponse>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly INotifyChangeService notifyChangeService;

        public EditCourseRequestHandler(ICourseRepository courseRepository, INotifyChangeService notifyChangeService)
        {
            _courseRepository = courseRepository;
            this.notifyChangeService = notifyChangeService;
        }

        public async Task<EditCourseResponse> Handle(EditCourseRequest request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.Get(request.Id);

            if(course is null)
                throw new Exception("Course not found");
            
            if(request.Name is not null)
                course.Name = request.Name;

            if(request.Description is not null)
                course.Description = request.Description;

            if(request.ThumbImage is not null)
                course.ThumbImage = request.ThumbImage;

            if(request.Tags is not null)
                course.Tags = request.Tags;

            if(request.IsActive)
                course.IsActive = request.IsActive;

            await notifyChangeService.NotifyCourseChangeAsync(course.Id, NotifyOperationEnum.Update);

            return new EditCourseResponse(course);
        }
    }
}
