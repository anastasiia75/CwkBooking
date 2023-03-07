using CwkBooling.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CwkBooling.Domain.Abstractions.Services
{
    public interface IReservationService
    {
        Task<Reservation> MakeReservation(Reservation reservation);
        Task<Reservation> GetReservationByIdAsync(int id);
        Task<Reservation> DeleteReservationByIdAsync(int id);
        Task<List<Reservation>> GetAllReservationsAsync();
    }
}
