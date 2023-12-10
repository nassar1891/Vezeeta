using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VezeetaAPI.Models;

namespace VezeetaAPI.Repositories
{
    public class CouponRepository
    {
        private readonly VezeetaContext _context;

        public CouponRepository(VezeetaContext context)
        {
            _context = context;
        }

        public async Task<List<Coupon>> GetAll()
        {
            return await _context.Coupons.ToListAsync();
        }

        public async Task<Coupon> GetById(int id)
        {
            return await _context.Coupons.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Coupon> GetCouponByCode(string code)
        {
            return await _context.Coupons.FirstOrDefaultAsync(c => c.Code == code);
        }

        public async Task Add(Coupon coupon)
        {
            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Coupon coupon)
        {
            if (id != coupon.Id)
                return;

            _context.Entry(coupon).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var couponToDelete = await _context.Coupons.FindAsync(id);
            if (couponToDelete != null)
            {
                _context.Coupons.Remove(couponToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
