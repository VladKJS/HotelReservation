namespace HotelReservation.Data;

public interface IReservationData
{
    Task<bool> MakeReservation(BookingDetails bookingDetails);
}

public sealed class ReservationData : IReservationData
{
    private readonly ILogger<ReservationData> _logger;
    private ConcurrentDictionary<BookingDetails, ReservationDetails> _reservation;
    public ReservationData(ILogger<ReservationData> logger) 
    { 
        _logger = logger;
        _reservation = new();
    }
    public Task<bool> MakeReservation(BookingDetails bookingDetails)
    {
        var lookup = new BookingDetails(bookingDetails.Hotel, bookingDetails.RoomNumber);

        if (!_reservation.TryGetValue(lookup, out _))
        {
            _reservation.TryAdd(new BookingDetails(bookingDetails.Hotel, bookingDetails.RoomNumber), new ReservationDetails(bookingDetails.Hotel, bookingDetails.RoomNumber, false));
           _logger.LogInformation( "Reservation made for {0} hotel, room number {1}", bookingDetails.Hotel, bookingDetails.RoomNumber);
            return Task.FromResult(true);
        }
        _logger.LogInformation( "Can not make reservation for {0} hotel, room number {1}",bookingDetails.Hotel, bookingDetails.RoomNumber);
        return Task.FromResult(false);
    }
}
