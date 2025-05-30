using BookingService.Api.Core.Application.Interfaces;
using BookingService.Api.Core.Domain.Model;
using BookingService.Api.Core.Domain.Model.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Api.Core.Application.AppData.Queries.GetFreeSlots
{
    public class GetFreeTimeSlotsQueryHandler : IRequestHandler<GetFreeSlotsQuery, List<TimeSlotViewModel>>
    {
        private readonly IBookingDbContext _bookingRepository;

        public GetFreeTimeSlotsQueryHandler(IBookingDbContext bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<List<TimeSlotViewModel>> Handle(GetFreeSlotsQuery request, CancellationToken cancellationToken)
        {
            var workingDayStart = request.Date.Date.AddHours(11); 
            var workingDayEnd = request.Date.Date.AddHours(21);
            List<Booking> allBookings;
            if (request.ResourceId != null)
            {
                allBookings = await _bookingRepository.Bookings
                    .Where(b => b.ResourceId == request.ResourceId &&
                                b.Status != BookingStatus.Истёк &&
                                b.EndTime > workingDayStart &&
                                b.StartTime < workingDayEnd)
                    .ToListAsync();
            }
            else
            {
                allBookings = await _bookingRepository.Bookings
                    .Where(b => b.RoomId == request.RoomId &&
                                b.Status != BookingStatus.Истёк &&
                                b.EndTime > workingDayStart &&
                                b.StartTime < workingDayEnd)
                    .ToListAsync();
            }

            var mergedBusyIntervals = MergeIntervals(allBookings);

            var freeSlots = CalculateFreeSlots(mergedBusyIntervals, workingDayStart, workingDayEnd);

            return freeSlots.Select(s => new TimeSlotViewModel
            {
                StartTime = s.StartTime,
                EndTime = s.EndTime
            }).ToList();
        }
        private List<(DateTime Start, DateTime End)> MergeIntervals(List<Booking> bookings)
        {
            var intervals = bookings
                .OrderBy(b => b.StartTime)
                .Select(b => (b.StartTime, b.EndTime))
                .ToList();

            var merged = new List<(DateTime Start, DateTime End)>();

            foreach (var interval in intervals)
            {
                if (!merged.Any())
                {
                    merged.Add(interval);
                    continue;
                }

                var last = merged.Last();
                if (interval.StartTime <= last.End)
                {
                    merged[merged.Count - 1] = (last.Start, interval.EndTime > last.End ? interval.EndTime : last.End);
                }
                else
                {
                    merged.Add(interval);
                }
            }

            return merged;
        }

        private List<TimeSlotViewModel> CalculateFreeSlots(List<(DateTime Start, DateTime End)> busyIntervals, DateTime dayStart, DateTime dayEnd)
        {
            var result = new List<TimeSlotViewModel>();
            var current = dayStart;

            foreach (var interval in busyIntervals)
            {
                if (interval.Start > current)
                {
                    result.Add(new TimeSlotViewModel
                    {
                        StartTime = current,
                        EndTime = interval.Start
                    });
                }

                if (interval.End > current)
                    current = interval.End;
            }

            if (current < dayEnd)
            {
                result.Add(new TimeSlotViewModel
                {
                    StartTime = current,
                    EndTime = dayEnd
                });
            }

            return result;
        }
    }
}
