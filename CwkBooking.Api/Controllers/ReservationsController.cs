using AutoMapper;
using CwkBooking.Api.Dtos;
using CwkBooling.Domain.Abstractions.Services;
using CwkBooling.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CwkBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;
        public ReservationsController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationPutPostDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            var result = await _reservationService.MakeReservation(reservation);
            //if (reservation == null)
            // return NotFound("Invalid data");
            if (result == null)
                return BadRequest("Cannot create the reservation");
            return Ok(reservation);
        }
        [HttpGet]
        [Route("{reservationId}")]
        public async Task<IActionResult> GetReservationByID(int reservationId)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(reservationId);
            if (reservation == null)
                return NotFound($"No reservation found for the id {reservationId}");
            var reservationDto = _mapper.Map<ReservationGetDto>(reservation);

            return Ok(reservationDto);
        }

        [HttpDelete]
        [Route("{reservationId}")]
        public async Task<IActionResult> DeleteReservationById(int reservationId)
        {
            var reservation = await _reservationService.DeleteReservationByIdAsync(reservationId);
            if (reservation == null)
                return NotFound();
            return NoContent();

        }
        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();

            var reservationsDto = _mapper.Map<List<ReservationGetDto>>(reservations);
            return Ok(reservationsDto);
        }
    }
}
