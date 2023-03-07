using CwkBooking.DAL;
using CwkBooling.Domain.Abstractions.Repositories;
using CwkBooling.Domain.Abstractions.Services;
using CwkBooling.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwkBooking.Services.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IHotelsRepository _hotelRepository;
        private readonly DataContext _ctx;

        public ReservationService(IHotelsRepository hotelsRepo, DataContext ctx)
        {
            _hotelRepository = hotelsRepo;
            _ctx = ctx;
        }

        public async Task<Reservation> MakeReservation(Reservation reservation)
        {
            //Step 1: get hotel, inckuding all rooms
            var hotel = await _hotelRepository.GetHotelByIdAsync(reservation.HotelId);

            //Step 2: find the specified room
            var room = hotel.Rooms.FirstOrDefault(r => r.RoomId == reservation.RoomId);

            //Step 3: Make sure the room is available

            if (hotel == null || room == null)
                return null;

            bool isBusy = await _ctx.Reservations.AnyAsync(r => 
                (reservation.CheckInDate >= r.CheckInDate && reservation.CheckInDate <= r.CheckOutDate)
                && (reservation.CheckOutDate   >= r.CheckInDate && reservation.CheckOutDate <= r.CheckOutDate)
            );

            if (isBusy)
                return null; 
            //Step 4: persist all changes to the database

            _ctx.Rooms.Update(room);
            _ctx.Reservations.Add(reservation);

            await _ctx.SaveChangesAsync();

            return reservation;
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            return await _ctx.Reservations.Include(r => r.Hotel).
                Include(r => r.Room).
                FirstOrDefaultAsync(r => r.ReservationId == id);
        }
        public async Task<Reservation> DeleteReservationByIdAsync(int id)
        {
            var reservation = await _ctx.Reservations.FirstOrDefaultAsync(r => r.ReservationId == id);
            if (reservation == null)
                return null;
            _ctx.Reservations.Remove(reservation);
            await _ctx.SaveChangesAsync();

            return reservation;
        }

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            return await _ctx.Reservations.ToListAsync();
        }
    }
}
