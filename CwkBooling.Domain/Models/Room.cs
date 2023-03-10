
using System;

namespace CwkBooling.Domain.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public double Surface { get; set; }
        public bool NeedsRepair { get; set; }
        public DateTime? BusyFrom { get; set; }
        public DateTime? BusyTo { get; set; }
        public int HotelId { get; set; }
    }
}
