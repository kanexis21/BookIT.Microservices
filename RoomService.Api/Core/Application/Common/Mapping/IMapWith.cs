using AutoMapper;

namespace RoomService.Api.Core.Application.Common.Interfaces
{
    internal interface IMapWith<T>
    {
        void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
        }
    }
}
