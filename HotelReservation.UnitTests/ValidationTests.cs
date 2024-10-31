using FluentAssertions;
using HotelReservation.Helpers;
using Xunit;

namespace HotelReservation.UnitTests;

public class ValidationTests
{
    [Fact]
    public void Validator_ValidBooking_ShouldReturnBookingDetails()
    {
        var booking = "Hilton-101";

        var result = booking.Validator();

        result.Should().NotBeNull();
        result.Hotel.Should().Be("Hilton");
        result.RoomNumber.Should().Be(101);
    }
}
