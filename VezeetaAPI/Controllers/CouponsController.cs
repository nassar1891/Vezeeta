using Microsoft.AspNetCore.Mvc;
using VezeetaAPI.Models;
using VezeetaAPI.Repositories;

namespace VezeetaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CouponsController : ControllerBase
    {
        private readonly CouponRepository _couponRepository;

        public CouponsController(CouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoupons()
        {
            var coupons = await _couponRepository.GetAll();
            return Ok(coupons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCouponById(int id)
        {
            var coupon = await _couponRepository.GetById(id);
            if (coupon == null)
            {
                return NotFound();
            }
            return Ok(coupon);
        }

        [HttpPost]
        public async Task<IActionResult> AddCoupon(Coupon coupon)
        {
            await _couponRepository.Add(coupon);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoupon(int id, Coupon coupon)
        {
            await _couponRepository.Update(id, coupon);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            await _couponRepository.Delete(id);
            return Ok();
        }
    }
}
