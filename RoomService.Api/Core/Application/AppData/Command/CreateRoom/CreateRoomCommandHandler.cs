using MediatR;
using RoomService.Api.Core.Application.Interfaces;
using RoomService.Api.Core.Domain.Model;
using RoomService.Api.Infrastructure;

namespace RoomService.Api.Core.Application.AppData.Command.CreateRoom
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Guid>
    {
        private readonly IRoomDbContext _context;

        public CreateRoomCommandHandler(IRoomDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = new Room
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Capacity = request.Capacity,
                Location = request.Location,
                Description = request.Description,
                Photos = new List<RoomPhoto>(), // обязательно инициализируем
                Status = RoomStatus.Свободно
            };

            foreach (var base64Photo in request.Photos)
            {
                // Генерируем имя файла
                var fileName = $"{Guid.NewGuid()}.jpg";
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                // Преобразуем base64 в байты
                var bytes = Convert.FromBase64String(base64Photo.ToString());

                // Сохраняем файл
                await File.WriteAllBytesAsync(filePath, bytes, cancellationToken);

                room.Photos.Add(new RoomPhoto
                {
                    Id = Guid.NewGuid(),
                    FileName = "/uploads/" + fileName,
                    RoomId = room.Id
                });
            }

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync(cancellationToken);

            return room.Id;
        }
    }

}
