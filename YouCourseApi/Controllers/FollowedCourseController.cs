using Application.UseCases.CoursesFollowed;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YouCourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowedCourseController : ControllerBase
    {
        private readonly IMediator mediator;

        public FollowedCourseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> FollowCourse(FollowCourseRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> StopFollowCourse(StopFollowCourseRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetFollowedCoursesByUser(int id)
        {
            var response = await mediator.Send(new GetFollowedCoursesByUserRequest(id));
            return Ok(response);
        }
    }
}
