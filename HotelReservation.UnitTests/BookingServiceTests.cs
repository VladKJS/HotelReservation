using FluentAssertions;
using HotelReservation.Data;
using HotelReservation.Models;
using HotelReservation.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace HotelReservation.UnitTests;

public class BookingServiceTests
{
    private readonly Mock<IReservationData> _reservationDataMock; 
    private readonly BookingService _bookingService;

    public BookingServiceTests()
    {
        _reservationDataMock = new Mock<IReservationData>();        
        _bookingService = new BookingService(_reservationDataMock.Object);
    }

    [Fact]
    public async Task MakeReservation_Successful_ShouldReturnTrue()
    {
        var bookingDetails = new BookingDetails("GrandHotel", 303);
        _reservationDataMock.Setup(r => r.MakeReservation(bookingDetails)).ReturnsAsync(true);

        var result = await _bookingService.MakeReservation(bookingDetails);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task MakeReservation_Unsuccessful_ShouldReturnFalse()
    {
        var bookingDetails = new BookingDetails("Hilton", 404);
        _reservationDataMock.Setup(r => r.MakeReservation(bookingDetails)).ReturnsAsync(false);

        var result = await _bookingService.MakeReservation(bookingDetails);

        result.Should().BeFalse();
    }
}
