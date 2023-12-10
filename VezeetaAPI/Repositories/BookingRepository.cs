using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VezeetaAPI.Models;

namespace VezeetaAPI.Repositories
{
    public class BookingRepository
    {
        private readonly VezeetaContext _context;

        public BookingRepository(VezeetaContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetAll()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking> GetById(int id)
        {
            return await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task<List<Booking>> GetDoctorBookings(int doctorId)
        {
            return await _context.Bookings
                .Where(b => b.Schedule.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetPatientBookings(int patientId)
        {
            return await _context.Bookings
                .Where(b => b.PatientId == patientId)
                .ToListAsync();
        }
        public async Task Add(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Booking booking)
        {
            if (id != booking.Id)
                return;

            _context.Entry(booking).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var bookingToDelete = await _context.Bookings.FindAsync(id);
            if (bookingToDelete != null)
            {
                _context.Bookings.Remove(bookingToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
