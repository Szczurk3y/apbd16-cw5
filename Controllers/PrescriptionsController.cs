using apbd_cw5.Data;
using apbd_cw5.DTOs;
using apbd_cw5.Models;
using apbd_cw5.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd_cw5.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PrescriptionsController(IDbService dbService) : ControllerBase
    {
        
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var prescriptions = await dbService.GetPrescriptions();
            return Ok(prescriptions);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PrescriptionDto? requestDto)
        {
            Console.WriteLine(requestDto);
            if (requestDto == null) return BadRequest("Invalid data.");
            
            var medicaments = await dbService.GetMedicaments();
            var doctor = await dbService.GetDoctor(requestDto.IdDoctor);

            if (doctor == null) throw new ArgumentException("Doctor not found.");
            if (requestDto.DueDate <= requestDto.Date)  throw new ArgumentException("DueDate must be after Date.");
            if (requestDto.Medicaments.Count > 10)  throw new ArgumentException("A prescription can have at most 10 medicaments.");
            if (requestDto.Medicaments.Select(reqMed => medicaments.FirstOrDefault(m => reqMed.IdMedicament == m.MedicamentId)).Any(med => med == null)) throw new ArgumentException($"Medicament does not exist.");
           
            var prescription = new Prescription()
            {
                Date = requestDto.Date,
                DueDate = requestDto.DueDate,
                Patient = new Patient
                {
                    FirstName = requestDto.Patient.FirstName,
                    LastName = requestDto.Patient.LastName,
                    BirthDate = requestDto.Patient.BirthDate
                },
                Doctor = doctor
            };
            var prescriptionId = await dbService.AddPrescription(prescription);
            return Ok($"Prescription of id: {prescriptionId} created");
        }
    }
}