using Application.UseCases.CourseUseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace YouCourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator mediator;

        public CourseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest request)
        {
            var response = await mediator.Send(request);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> EditCourse([FromBody] EditCourseRequest request)
        {
            var response = await mediator.Send(request);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCourse([FromBody] DeleteCourseRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var response = await mediator.Send(new GetCourseByIdRequest(id));

            return Ok(response);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetCoursesByUser(int id)
        {
            var response = await mediator.Send(new GetCoursesByUserRequest(id));

            return Ok(response);
        }
    }
}
