using BookingService.Api.Application.AppData.Command.CreateBooking;
using BookingService.Api.Application.AppData.Command.DeleteBooking;
using BookingService.Api.Application.AppData.Command.UpdateBooking;
using BookingService.Api.Application.AppData.Command.UpdateBooking.UpdateBookingStatus;
using BookingService.Api.Application.AppData.Queries.GetAllBooking;
using BookingService.Api.Application.AppData.Queries.GetBookingById;
using BookingService.Api.Controllers;
using BookingService.Api.Core.Application.AppData.Queries.GetFreeSlots;
using BookingService.Api.Core.Application.AppData.Queries.GetMyBookings;
using BookingService.Api.Core.Domain.Model;
using BookingService.Api.Core.Domain.Model.ViewModel;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookIT.WebAPI.Controllers
{
    /// <summary>
    /// ���������� ��� ���������� ��������������.
    /// </summary>
    [Authorize(Policy = "ApiScope")]
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : BaseApiController
    {
        [HttpGet("free-slots-resource")]
        public async Task<IActionResult> GetFreeSlotsResource([FromQuery] Guid resourceId, [FromQuery] DateTime date)
        {
            var query = new GetFreeSlotsQuery
            {
                ResourceId = resourceId,
                Date = date.Date
            };

            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("free-slots-room")]
        public async Task<IActionResult> GetFreeSlotsRoom([FromQuery] Guid roomId, [FromQuery] DateTime date)
        {
            var query = new GetFreeSlotsQuery
            {
                ResourceId = roomId,
                Date = date.Date
            };

            var result = await Mediator.Send(query);
            return Ok(result);
        }
        /// <summary>
        /// ������ ����� ������������.
        /// </summary>
        /// <param name="request">������ �� �������� ������������.</param>
        /// <returns>ID ���������� ������������.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingRequest request)
        {
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"{claim.Type} = {claim.Value}");
            }
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            request.UserId = Guid.Parse(userIdClaim);
            CreateBookingCommand command;

            if (request.ResourceId != null)
            {
                command = new CreateBookingCommand
                {
                    UserId = request.UserId,
                    ResourceId = request.ResourceId,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    Description = request.Description,
                    Status = "�������������"
                };
            }
            else
            {
                command = new CreateBookingCommand
                {
                    UserId = request.UserId,
                    RoomId = request.RoomId,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    Description = request.Description,
                    Status = "�������������"
                };
            }

            var bookingId = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = bookingId }, bookingId);
        }

        /// <summary>
        /// �������� ������ ���� ������������.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllBookingQuery());
            return Ok(result);
        }

        /// <summary>
        /// �������� ������������ �� Id.
        /// </summary>
        /// <param name="id">ID ������������.</param>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var booking = await Mediator.Send(new GetBookingByIdQuery(id));
            return booking == null ? NotFound() : Ok(booking);
        }
        /// <summary>
        /// �������� ������ ������������ �� Id ������������.
        /// </summary>
        [HttpGet("my")]
        public async Task<IActionResult> GetMyBookings(Guid userId)
        {
            var query = new GetMyBookingsQuery { UserId = userId };
            var result = await Mediator.Send(query);

            return Ok(result);
        }
        /// <summary>
        /// ��������� ������������ �� Id.
        /// </summary>
        /// <param name="id">ID ������������.</param>
        /// <param name="command">������� ��� ����������.</param>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookingCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID � URL � ���� ������� �� ���������.");

            var updated = await Mediator.Send(command);
            return updated ? NoContent() : NotFound();
        }

        /// <summary>
        /// ������� ������������ �� Id.
        /// </summary>
        /// <param name="id">ID ������������.</param>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await Mediator.Send(new DeleteBookingCommand(id));
            return deleted ? NoContent() : NotFound();
        }

        /// <summary>
        /// ��������� ������ ������������.
        /// </summary>
        /// <param name="command">������� ���������� �������</param>
        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateBookingStatusCommand command)
        {   
            await Mediator.Send(command);
            return NoContent();
        }

    }
}
