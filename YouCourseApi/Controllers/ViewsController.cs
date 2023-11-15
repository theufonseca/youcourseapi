using Application.UseCases.MetricsUseCases;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YouCourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ViewsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewViewUseCaseRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetByCourse(int courseId)
        {
            var request = new GetAllViewsByCourseRequest(courseId);
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("unique/course/{courseId}")]
        public async Task<IActionResult> GetByCoursePerUser(int courseId)
        {
            var request = new GetAllViewsByCoursePerUserRequest(courseId);
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
