using Application.UseCases.TrailUseCases;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YouCourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrailController : ControllerBase
    {
        private readonly IMediator mediator;

        public TrailController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrail([FromBody] NewTrailRequest request)
        {
            var response = await mediator.Send(request);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> EditTrail([FromBody] EditTrailRequest request)
        {
            var response = await mediator.Send(request);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTrail([FromBody] DeleteTrailRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
