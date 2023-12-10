using Microsoft.AspNetCore.Mvc;
using VezeetaAPI.Models;
using VezeetaAPI.Repositories;

namespace VezeetaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorRepository _doctorRepository;
        private readonly BookingRepository _bookingRepository;

        public DoctorsController(DoctorRepository doctorRepository,
            BookingRepository bookingRepository)
        {
            _doctorRepository = doctorRepository;
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _doctorRepository.GetAll();
            return Ok(doctors);
        }
        [HttpGet("{doctorId}/bookings")]
        public async Task<IActionResult> GetDoctorBookings(int doctorId)
        {
            var doctor = await _doctorRepository.GetById(doctorId);
            if (doctor == null)
            {
                return NotFound("Doctor not found");
            }

            var doctorBookings = await _bookingRepository.GetDoctorBookings(doctorId);

            return Ok(doctorBookings);
        }

        [HttpGet("doctors")]
        public async Task<IActionResult> SearchDoctors(DoctorSearchCriteria criteria)
        {
            var doctors = await _doctorRepository.SearchDoctors(criteria);

            return Ok(doctors);
        }
        [HttpGet("doctors")]
        public async Task<IActionResult> GetDoctorsBySpecialization(string specialization)
        {
            var doctors = await _doctorRepository.GetDoctorsBySpecialization(specialization);

            return Ok(doctors);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, Doctor doctor)
        {
            await _doctorRepository.Update(id, doctor);
            return Ok();
        }

        [HttpPut("{id}/schedules")]
        public async Task<IActionResult> UpdateDoctorSchedules(int id, List<Schedule> updatedSchedules)
        {
            var doctor = await _doctorRepository.GetById(id);
            if (doctor == null)
            {
                return NotFound();
            }

            // Update the doctor's schedules
            doctor.Schedules = updatedSchedules;

            // Update the doctor in the repository
            await _doctorRepository.Update(doctor.Id, doctor);

            return Ok();
        }

        [HttpPut("{doctorId}/confirm-booking/{bookingId}")]
        public async Task<IActionResult> ConfirmBooking(int doctorId, int bookingId)
        {
            var doctor = await _doctorRepository.GetById(doctorId);
            if (doctor == null)
            {
                return NotFound("Doctor not found");
            }

            var booking = await _bookingRepository.GetById(bookingId);
            if (booking == null)
            {
                return NotFound("Booking not found");
            }

            // Assuming the booking belongs to the doctor and the doctor can confirm it
            if (booking.Schedule?.DoctorId != doctorId)
            {
                return BadRequest("Doctor cannot confirm this booking");
            }

            booking.State = BookingState.Confirmed;

            await _bookingRepository.Update(bookingId, booking);

            return Ok("Booking confirmed");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDoctor(int id)
        {
            await _doctorRepository.Delete(id);
            return Ok();
        }
    }
}

