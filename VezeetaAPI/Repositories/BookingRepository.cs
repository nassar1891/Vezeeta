using VezeetaAPI.Models;
using VezeetaAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VezeetaAPI.Repositories
{
    public class BookingRepository : ICrudRepository<Booking>
    {
        private readonly VezeetaContext _context;

        public BookingRepository(VezeetaContext context)
        {
            _context = context;
        }

        public async Task<Booking> GetById(int id)
        {
            return await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Booking>> GetAll()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<List<Booking>> GetPatientBookings(int patientId)
        {
            var patientBookings = await _context.Bookings
                .Where(b => b.PatientId == patientId)
                .ToListAsync();

            return patientBookings;
        }
        public async Task<List<Booking>> GetDoctorBookings(int doctortId)
        {
            var doctorBookings = await _context.Bookings
                .Include(b=>b.Schedule)
                .Where(b => b.Schedule.DoctorId == doctortId)
                .ToListAsync();

            return doctorBookings;
        }

        public async Task Add(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Booking booking)
        {
            var existingBooking = await _context.Bookings.FindAsync(id);
            if (existingBooking != null)
            {
                existingBooking.PatientId = booking.PatientId;
                existingBooking.ScheduleId = booking.ScheduleId;
                existingBooking.State = booking.State;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var bookingToDelete = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id);
            if (bookingToDelete != null)
            {
                _context.Bookings.Remove(bookingToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
