using Microsoft.AspNetCore.Mvc;
using VezeetaAPI.Models;
using VezeetaAPI.Repositories;
using System.Collections.Generic;

namespace VezeetaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorRepository _doctorRepository;

        public DoctorsController(DoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        [HttpGet]
        public IActionResult GetAllDoctors()
        {
            var doctors = _doctorRepository.GetAll();
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public IActionResult GetDoctorById(int id)
        {
            var doctor = _doctorRepository.GetById(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }

        [HttpPost]
        public IActionResult AddDoctor(Doctor doctor)
        {
            _doctorRepository.Add(doctor);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDoctor(int id, Doctor doctor)
        {
            var existingDoctor = _doctorRepository.GetById(id);
            if (existingDoctor == null)
            {
                return NotFound();
            }

            doctor.Id = id;
            _doctorRepository.Update(doctor);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            _doctorRepository.Delete(id);
            return Ok();
        }
    }
}
