using AutoMapper;
using CwkBooking.Api.Dtos;
using CwkBooling.Domain.Models;
namespace CwkBooking.Api.AutoMapper
{
    public class HotelMappingProfiles : Profile
    {
        public HotelMappingProfiles()
        {
            CreateMap<HotelCreateDto, Hotel>();
            CreateMap<Hotel, HotelGetDto>();
        }

    }
}
