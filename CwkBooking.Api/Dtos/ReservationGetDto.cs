using System;

namespace CwkBooking.Api.Dtos
{
    public class ReservationGetDto
    {
        public int ReservationId { get; set; }
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public RoomGetDto Room;
        public HotelGetDto Hotel;
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Customer { get; set; }
    }
}
