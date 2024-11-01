namespace HotelReservation.Models;

public sealed record ReservationDetails
{
    public string? Hotel { get; }
    public int RoomNumber { get; }
    public bool IsAvailable { get; }

    public ReservationDetails(string? hotel, int roomNumber, bool isAvailable)
    {
        Hotel = hotel;
        RoomNumber = roomNumber;
        IsAvailable = isAvailable;
    }
}
