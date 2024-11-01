using FluentAssertions;
using HotelReservation.Data;
using HotelReservation.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace HotelReservation.UnitTests;

public class ReservationDataTests
{
    private readonly ReservationData _reservationData;
    private readonly Mock<ILogger<ReservationData>> _loggerMock;

    public ReservationDataTests()
    {
        _loggerMock = new Mock<ILogger<ReservationData>>();
        _reservationData = new ReservationData(_loggerMock.Object);
    }

    [Fact]
    public async Task MakeReservation_NewReservation_ShouldReturnTrue()
    {
        var bookingDetails = new BookingDetails("Carlton", 101);

        var result = await _reservationData.MakeReservation(bookingDetails);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task MakeReservation_ExistingReservation_ShouldReturnFalse()
    {
        var bookingDetails = new BookingDetails("Hilton", 202);
        await _reservationData.MakeReservation(bookingDetails);

        var result = await _reservationData.MakeReservation(bookingDetails);

        result.Should().BeFalse();
    }
}
