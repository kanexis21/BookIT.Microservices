using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomService.Api.Core.Application.AppData.Command.CreateRoom;
using RoomService.Api.Core.Application.AppData.Command.UpdateRoom.UpdateRoomStatus;
using RoomService.Api.Core.Application.AppData.Queries.GetAllRooms;
using RoomService.Api.Core.Application.AppData.Query.GetResourcesByIds;
using RoomService.Api.Core.Application.AppData.Query.GetRoomById;
using RoomService.Api.Core.Domain.Model;

namespace BookIT.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для управления комнатами.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : BaseApiController
    {
        /// <summary>
        /// Создаёт новую комнату.
        /// </summary>
        /// <param name="command">Команда для создания комнаты.</param>
        /// <returns>ID созданной комнаты.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateRoomCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateRoomStatusCommand request)
        {
            var command = new UpdateRoomStatusCommand
            {
                RoomId = id,
                Status = request.Status
            };

            await Mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        /// Получает помещение по Id.
        /// </summary>
        /// <param name="id">ID бронирования.</param>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var room = await Mediator.Send(new GetRoomByIdQuery(id));
            return room == null ? NotFound() : Ok(room);
        }
        [HttpPost("by-ids")]
        public async Task<ActionResult<List<RoomShortViewModel>>> GetByIds([FromBody] List<Guid> ids)
        {
            if (ids == null || !ids.Any())
                return BadRequest("Нет Ids");

            var result = await Mediator.Send(new GetRoomsByIdsQuery(ids));
            return Ok(result);
        }
        /// <summary>
        /// Получает список всех комнат.
        /// </summary>
        /// <returns>Список комнат.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var rooms = await Mediator.Send(new GetAllRoomsQuery());
            return Ok(rooms);
        }
    }
}
