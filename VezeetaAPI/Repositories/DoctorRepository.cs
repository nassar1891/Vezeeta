using VezeetaAPI.Models;
using VezeetaAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VezeetaAPI.Repositories
{
    public class DoctorRepository : ICrudRepository<Doctor>
    {
        private readonly VezeetaContext _context;

        public DoctorRepository(VezeetaContext context)
        {
            _context = context;
        }

        public async Task<Doctor> GetById(int id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<Doctor>> GetAll()
        {
            return await _context.Doctors.ToListAsync();
        }
        public async Task<List<Doctor>> GetDoctorsBySpecialization(string specialization)
        {
            return await _context.Doctors
                .Where(d => d.Specialization == specialization )
                .ToListAsync();
        }
        public async Task<IEnumerable<Doctor>> SearchDoctors(DoctorSearchCriteria criteria)
        {
            var query = _context.Doctors.AsQueryable();

            if (!string.IsNullOrEmpty(criteria.Search))
            {
                query = query.Where(d => (d.FirstName.Contains(criteria.Search) || d.LastName.Contains(criteria.Search)));
            }

            // Paging
            var skip = (criteria.Page - 1) * criteria.PageSize;
            query = query.Skip(skip).Take(criteria.PageSize);

            return await query.ToListAsync();
        }

        public async Task Add(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Doctor doctor)
        {
            var existingDoctor = await _context.Doctors.FindAsync(id);
            if (existingDoctor != null)
            {
                existingDoctor.FirstName = doctor.FirstName;
                existingDoctor.LastName = doctor.LastName;
                existingDoctor.Email = doctor.Email;
                existingDoctor.Phone = doctor.Phone;
                existingDoctor.Gender = doctor.Gender;
                existingDoctor.DateOfBirth = doctor.DateOfBirth;
                existingDoctor.Specialization = doctor.Specialization;
                existingDoctor.Price = doctor.Price;

                await _context.SaveChangesAsync();
            }

            
        }


        public async Task Delete(int id)
        {
            var doctorToDelete = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
            if (doctorToDelete != null)
            {
                _context.Doctors.Remove(doctorToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
