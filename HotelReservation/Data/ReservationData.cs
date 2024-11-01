namespace HotelReservation.Data;

public interface IReservationData
{
    Task<bool> MakeReservation(BookingDetails bookingDetails);
}

public sealed class ReservationData(ILogger<ReservationData> logger) : IReservationData
{
    private static ConcurrentDictionary<BookingDetails, ReservationDetails> _reservation=new();
   
    public Task<bool> MakeReservation(BookingDetails bookingDetails)
    {
        var lookup = new BookingDetails(bookingDetails.Hotel, bookingDetails.RoomNumber);

        if (!_reservation.TryGetValue(lookup, out _))
        {
            _reservation.TryAdd(new BookingDetails(bookingDetails.Hotel, bookingDetails.RoomNumber), new ReservationDetails(bookingDetails.Hotel, bookingDetails.RoomNumber, false));
            logger.LogInformation( "Reservation made for {0} hotel, room number {1}", bookingDetails.Hotel, bookingDetails.RoomNumber);
            return Task.FromResult(true);
        }
        logger.LogInformation( "Can not make reservation for {0} hotel, room number {1}",bookingDetails.Hotel, bookingDetails.RoomNumber);
        return Task.FromResult(false);
    }
}
