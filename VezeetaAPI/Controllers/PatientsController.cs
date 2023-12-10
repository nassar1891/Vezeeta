using Microsoft.AspNetCore.Mvc;
using VezeetaAPI.Models;
using VezeetaAPI.Repositories;
using System.Collections.Generic;
using VezeetaAPI.Interfaces;

namespace VezeetaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly PatientRepository _patientRepository;
        private readonly BookingRepository _bookingRepository;

        public PatientsController(PatientRepository patientRepository, BookingRepository bookingRepository)
        {
            _patientRepository = patientRepository;
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        public IActionResult GetAllPatients()
        {
            var patients = _patientRepository.GetAll();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public IActionResult GetPatientById(int id)
        {
            var patient = _patientRepository.GetById(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        [HttpGet("{patientId}/bookings")]
        public async Task<IActionResult> GetPatientBookings(int patientId)
        {
            var patient = await _patientRepository.GetById(patientId);
            if (patient == null)
            {
                return NotFound("Patient not found");
            }

            var patientBookings = await _bookingRepository.GetPatientBookings(patientId);
            return Ok(patientBookings);
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient(Patient patient)
        {
            await _patientRepository.Add(patient);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, Patient patient)
        {
            var existingPatient = _patientRepository.GetById(id);
            if (existingPatient == null)
            {
                return NotFound();
            }
            await _patientRepository.Update(id, patient);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            await _patientRepository.Delete(id);
            return Ok();
        }
    }
}
