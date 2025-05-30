using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ResourceService.Api.Application.AppData.Command.CreateResource;
using ResourceService.Api.Core.Domain.Model;
using ResourceService.Api.Application.AppData.Queries.GetAllResources;
using ResourceService.Api.Application.AppData.Command.UpdateResource;
using ResourceService.Api.Application.AppData.Command.UpdateResource.UpdateResourceStatus;
using ResourceService.Api.Application.AppData.Command.DeleteResource;
using ResourceService.Api.Application.AppData.Queries.GetResourceById;
using ResourceService.Api.Core.Application.AppData.Query.GetResourcesByIds;

namespace ResourceService.Api.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для управления ресурсами.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ResourceController : BaseApiController
    {
        /// <summary>
        /// Создаёт новый ресурс.
        /// </summary>
        /// <param name="command">Команда на создание ресурса.</param>
        /// <returns>ID созданного ресурса.</returns>
        /// <response code="200">Ресурс успешно создан.</response>
        /// <response code="400">Некорректные входные данные.</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateResourceCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }

        /// <summary>
        /// Получает список всех ресурсов.
        /// </summary>
        /// <returns>Список ресурсов.</returns>
        /// <response code="200">Список ресурсов успешно получен.</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<Resource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var resources = await Mediator.Send(new GetAllResourcesQuery());
            return Ok(resources);
        }

        /// <summary>
        /// Обновляет информацию о ресурсе.
        /// </summary>
        /// <param name="id">ID ресурса.</param>
        /// <param name="command">Команда на обновление ресурса.</param>
        /// <response code="204">Ресурс успешно обновлён.</response>
        /// <response code="400">ID ресурса не совпадает с телом запроса.</response>
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateResourceCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID в URL и в теле запроса не совпадают.");

            await Mediator.Send(command);
            return NoContent();
        }
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateResourceStatusRequest request)
        {
            var command = new UpdateResourceStatusCommand
            {
                ResourceId = id,
                Status = request.Status
            };

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost("by-ids")]
        public async Task<ActionResult<List<ResourceShortViewModel>>> GetByIds([FromBody] List<Guid> ids)
        {
            if (ids == null || !ids.Any())
                return BadRequest("Нет Ids");

            var result = await Mediator.Send(new GetResourcesByIdsQuery(ids));
            return Ok(result);
        }
        /// <summary>
        /// Удаляет ресурс по ID.
        /// </summary>
        /// <param name="id">ID ресурса.</param>
        /// <response code="204">Ресурс успешно удалён.</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteResourceCommand(id));
            return NoContent();
        }

        /// <summary>
        /// Получает информацию о ресурсе по ID.
        /// </summary>
        /// <param name="id">ID ресурса.</param>
        /// <returns>Информация о ресурсе.</returns>
        /// <response code="200">Ресурс найден.</response>
        /// <response code="404">Ресурс не найден.</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Resource), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var resource = await Mediator.Send(new GetResourceByIdQuery(id));
            if (resource == null) return NotFound();
            return Ok(resource);
        }
    }
}
