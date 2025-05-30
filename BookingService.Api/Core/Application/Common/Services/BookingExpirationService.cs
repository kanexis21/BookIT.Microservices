using BookingService.Api.Core.Application.Interfaces;
using BookingService.Api.Core.Domain.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BookingService.Api.Core.Application.Common.Services
{
    public class BookingExpirationService : BackgroundService
    {
        private readonly ILogger<BookingExpirationService> _logger;
        private readonly IBookingDbContext _bookingRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public BookingExpirationService(
            ILogger<BookingExpirationService> logger, IBookingDbContext bookingRepository, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _bookingRepository = bookingRepository;
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Booking expiration service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var expiredBookings = await _bookingRepository.Bookings
                        .Where(b => b.EndTime < DateTime.UtcNow && b.Status == BookingStatus.Забронировано)
                        .ToListAsync();

                    foreach (var booking in expiredBookings)
                    {
                        booking.Status = BookingStatus.Истёк;
                        await _bookingRepository.SaveChangesAsync(stoppingToken);

                        _logger.LogInformation($"Booking {booking.Id} marked as expired.");

                        var client = _httpClientFactory.CreateClient("ResourceService");

                        var dto = new { Status = "Доступен" };

                        var content = new StringContent(
                            JsonConvert.SerializeObject(dto),
                            Encoding.UTF8,
                            "application/json");

                        var response = await client.PatchAsync($"/api/resources/{booking.ResourceId}/status", content);
                        response.EnsureSuccessStatusCode();
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while checking expired bookings.");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            _logger.LogInformation("Booking expiration service stopped.");
        }
    }

}
