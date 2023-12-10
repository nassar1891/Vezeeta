using VezeetaAPI.Models;
using VezeetaAPI.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace VezeetaAPI.Repositories
{
    public class DoctorRepository : ICrudRepository<Doctor>
    {
        private readonly VezeetaContext _context;

        public DoctorRepository(VezeetaContext context)
        {
            _context = context;
        }

        public Doctor GetById(int id)
        {
            return _context.Doctors.FirstOrDefault(d => d.Id == id);
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _context.Doctors.ToList();
        }

        public void Add(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }

        public void Update(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var doctorToDelete = _context.Doctors.FirstOrDefault(d => d.Id == id);
            if (doctorToDelete != null)
            {
                _context.Doctors.Remove(doctorToDelete);
                _context.SaveChanges();
            }
        }
    }
}
