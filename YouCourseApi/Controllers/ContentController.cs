using Application.UseCases.ContentUseCases;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YouCourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IMediator mediator;

        public ContentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContent([FromBody] CreateContentRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> EditContent([FromBody] EditContentRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContent([FromBody] DeleteContentRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
