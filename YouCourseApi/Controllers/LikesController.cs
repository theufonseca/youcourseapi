using Application.UseCases.MetricsUseCases;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YouCourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly IMediator mediator;

        public LikesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewLikeRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await mediator.Send(new RemoveLikeRequest(id));
            return Ok(response);
        }

        [HttpGet("course/{id}")]
        public async Task<IActionResult> GetByCourse(int id)
        {
            var request = new GetAllLikesByCourseRequest(id);
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetByUser(int id)
        {
            var request = new GetAllLikesByUserRequest(id);
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
