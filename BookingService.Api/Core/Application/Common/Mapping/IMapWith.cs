using AutoMapper;

namespace BookingService.Api.Application.Interfaces
{
    internal interface IMapWith<T>
    {
        void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
        }
    }
}
