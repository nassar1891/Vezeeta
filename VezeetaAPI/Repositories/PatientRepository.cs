//using VezeetaAPI.Models;
using VezeetaAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VezeetaAPI.Models;

namespace VezeetaAPI.Repositories
{
    public class PatientRepository : ICrudRepository<Patient>
    {
        private readonly VezeetaContext _context;

        public PatientRepository(VezeetaContext context)
        {
            _context = context;
        }

        public async Task<Patient> GetById(int id)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Patient>> GetAll()
        {
            return await _context.Patients.ToListAsync();
        }
        
        public async Task Add(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Patient patient)
        {
            var existingPatient = await _context.Patients.FindAsync(id);
            if (existingPatient != null)
            {
                existingPatient.FirstName = patient.FirstName;
                existingPatient.LastName = patient.LastName;
                existingPatient.Email = patient.Email;
                existingPatient.Phone = patient.Phone;
                existingPatient.Gender = patient.Gender;
                existingPatient.DateOfBirth = patient.DateOfBirth;

                await _context.SaveChangesAsync();
            }
        }


        public async Task Delete(int id)
        {
            var patientToDelete = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (patientToDelete != null)
            {
                _context.Patients.Remove(patientToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
