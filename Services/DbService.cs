using apbd_cw5.Data;
using apbd_cw5.DTOs;
using apbd_cw5.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace apbd_cw5.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<Prescription>> GetPrescriptions()
    {
        var prescriptions = await _context.Prescriptions
            .Include(p => p.Patient)
            .Include(p => p.PrescriptionMedicaments)
            .ToListAsync();
        return prescriptions;
    }

    public async Task<Doctor?> GetDoctor(int id)
    {
        var doctor = await _context.Doctors
            .Where(d => d.DoctorId == id)
            .Include(p => p.Prescriptions)
            .FirstOrDefaultAsync();
        return doctor;
    }

    public async Task<Patient?> GetPatient(int id)
    {
        var patient = await _context.Patients
            .Where(p => p.IdPatient == id)
            .Include(p => p.Prescriptions)
            .FirstOrDefaultAsync();
        return patient;
    }

    public async Task<List<Medicament>> GetMedicaments()
    {
        var medicaments = await _context.Medicaments
            .Include(m => m.PrescriptionMedicaments)
            .ToListAsync();
        return medicaments;
    }


    public PatientDetailsDto? GetPatientDetailsAsync(int id)
    {
        var patient = _context.Patients.Include(patient => patient.Prescriptions)
            .ThenInclude(prescription => prescription.Doctor).Include(patient => patient.Prescriptions)
            .ThenInclude(prescription => prescription.PrescriptionMedicaments)
            .ThenInclude(prescriptionMedicament => prescriptionMedicament.Medicament).FirstOrDefault(p => p.IdPatient == id);
        
        if (patient == null) return null;

        var dto = new PatientDetailsDto
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Prescriptions = patient.Prescriptions
                .OrderBy(p => p.DueDate)
                .Select(p2 => new PrescriptionDto {
                    Date = p2.Date,
                    DueDate = p2.DueDate,
                    Patient = new PatientDto {
                        IdPatient= p2.PatientId,
                        FirstName = p2.Patient.FirstName,
                        LastName = p2.Patient.LastName
                    },
                    Medicaments = p2.PrescriptionMedicaments
                        .Select(pm => new PrescriptionMedicamentDto() {
                            IdMedicament = pm.MedicamentId,
                            Dose = pm.Dose,
                            Description = pm.Medicament.Description,
                        }).ToList()
                }).ToList()
        };
        return dto;
    }

    public async Task<int> AddPrescription(Prescription prescription)
    {
        var patient = await GetPatient(prescription.PatientId);
        if (patient == null)
        {
            patient = prescription.Patient;
            await _context.Patients.AddAsync(patient);  
            await _context.SaveChangesAsync();
        }

        var newPrescription = await _context.Prescriptions.AddAsync(prescription);
        await _context.SaveChangesAsync();
        return newPrescription.Entity.PrescriptionId;
    }
}