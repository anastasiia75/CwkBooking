using AutoMapper;
using CwkBooking.Api.Dtos;
using CwkBooling.Domain.Models;

namespace CwkBooking.Api.AutoMapper
{
    public class ReservationMappingProfile : Profile
    {
        public ReservationMappingProfile()
        {
            CreateMap<ReservationPutPostDto, Reservation>();
            CreateMap<Reservation, ReservationGetDto>();
        }
    }
}
