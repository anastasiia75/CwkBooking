﻿using CwkBooling.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CwkBooling.Domain.Abstractions.Repositories
{
    public interface IHotelsRepository
    {
        Task<List<Hotel>> GetAllHotelsAsync();
        Task<Hotel> GetHotelByIdAsync(int id);
        Task<Hotel> CreateHotelAsync(Hotel hotel);
        Task<Hotel> UpdateHotelAsync(Hotel updatedHotel);
        Task<Hotel> DeleteHotelAsync(int id);
        Task<List<Room>> ListHotelRoomAsync(int hotelId);
        Task<Room> GetHotelRoomByIdAsync(int hotelId, int roomId);
        Task<Room> CreateHotelRoomAsync(int hotelId, Room room);
        Task<Room> UpdateHotelRoomAsync(int hotelId, Room updatedRoom);
        Task<Room> DeleteHotelRoomAsync(int hotelId, int roomId);
    }
}
