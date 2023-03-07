using AutoMapper;
using CwkBooking.Api.Dtos;
using CwkBooling.Domain.Abstractions.Repositories;
using CwkBooling.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CwkBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : Controller
    {
        private readonly IHotelsRepository _hotelsRepo;
        private readonly IMapper _mapper;

        public HotelsController(IHotelsRepository hotelsRepo, IMapper mapper)
        {
            _hotelsRepo = hotelsRepo;
            _mapper = mapper;
        }

        //will execute on Get
        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _hotelsRepo.GetAllHotelsAsync();
            var getHotels = _mapper.Map<List<HotelGetDto>>(hotels);

            return Ok(getHotels);
        }

        [Route("{id}")]
        [HttpGet]

        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _hotelsRepo.GetHotelByIdAsync(id);
            if (hotel == null)
                return NotFound();
            var getHotel = _mapper.Map<HotelGetDto>(hotel);

            return Ok(getHotel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] HotelCreateDto hotel)
        {
            var domainHotel = _mapper.Map<Hotel>(hotel);

            domainHotel = await _hotelsRepo.CreateHotelAsync(domainHotel);
            var hotelGet = _mapper.Map<HotelGetDto>(domainHotel);


            return CreatedAtAction(nameof(GetHotelById), new { id = domainHotel.HotelId }, hotelGet);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateHotel([FromBody] HotelCreateDto updated, int id)
        {
            var toUpdate = _mapper.Map<Hotel>(updated);
            toUpdate.HotelId = id;

            await _hotelsRepo.UpdateHotelAsync(toUpdate);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelsRepo.DeleteHotelAsync(id);
            if (hotel == null)
                return NotFound();

            return NoContent();
        }


        [HttpGet]
        [Route("{hotelId}/rooms")]
        public async Task<IActionResult> GetAllHotelRoom(int hotelId)
        {
            var rooms = await _hotelsRepo.ListHotelRoomAsync(hotelId);

            var mappedRooms = _mapper.Map<List<RoomGetDto>>(rooms);

            return Ok(mappedRooms);

        }

        [HttpGet]
        [Route("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> GetHotelRoomById(int hotelId, int roomId)
        {
            var room = await _hotelsRepo.GetHotelRoomByIdAsync(hotelId, roomId);
            if (room == null)
                return NotFound();

            var mappedRoom = _mapper.Map<RoomGetDto>(room);

            return Ok(mappedRoom);
        }

        [HttpPost]
        [Route("{hotelId}/rooms")]
        public async Task<IActionResult> AddHotelRoom([FromBody] RoomPostPutDto newroom, int hotelId)
        {
            var room = _mapper.Map<Room>(newroom);
            room.HotelId = hotelId;

            await _hotelsRepo.CreateHotelRoomAsync(hotelId, room);

            var mappedRoom = _mapper.Map<RoomGetDto>(room);

            return CreatedAtAction(nameof(GetHotelRoomById),
                new { hotelId = hotelId, roomId = room.RoomId }, mappedRoom);
        }
        [HttpPut]
        [Route("{hotelId}/rooms{roomId}")]
        public async Task<IActionResult> UpdateHotelRoom(int hotelId, int roomId,
            [FromBody] RoomPostPutDto updatedroom)
        {
            var toUpdate = _mapper.Map<Room>(updatedroom);
            toUpdate.RoomId = roomId;
            toUpdate.HotelId = hotelId;

            await _hotelsRepo.UpdateHotelRoomAsync(hotelId, toUpdate);

            return NoContent();
        }

        [HttpDelete]
        [Route("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> RemoveHotelRoom(int hotelId, int roomId)
        {
            var room = await _hotelsRepo.DeleteHotelRoomAsync(hotelId, roomId);
            if (room == null)
                return NotFound("Room not found");

            return NoContent();
        }

    }
}
