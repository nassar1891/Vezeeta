using Microsoft.AspNetCore.Mvc;
using VezeetaAPI.Models;
using VezeetaAPI.Repositories;

namespace VezeetaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly BookingRepository _bookingRepository;
        private readonly CouponRepository _couponRepository;

        public BookingsController(BookingRepository bookingRepository, CouponRepository couponRepository)
        {
            _bookingRepository = bookingRepository;
            _couponRepository = couponRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
        {
            var bookings = await _bookingRepository.GetAll();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBookingById(int id)
        {
            var booking = await _bookingRepository.GetById(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [HttpPost]
        public async Task<ActionResult> AddBooking(Booking booking)
        {
            // Check if the booking applies a coupon
            if (!string.IsNullOrEmpty(booking.CouponCode))
            {
                var coupon = await _couponRepository.GetCouponByCode(booking.CouponCode);
                if (coupon != null)
                {
                    // Apply discount and update booking details
                    booking.DiscountAmount = coupon.Discount;
                    booking.DiscountApplied = true;
                    await _couponRepository.Delete(coupon.Id); // Remove the used coupon
                }
            }

            await _bookingRepository.Add(booking);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBooking(int id, Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            await _bookingRepository.Update(id, booking);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBooking(int id)
        {
            await _bookingRepository.Delete(id);
            return Ok();
        }
    }
}