
using apbd_cw5.DTOs;
using apbd_cw5.Models;

namespace apbd_cw5.Services;

public interface IDbService
{
    Task<int> AddPrescription(Prescription prescription);
    Task<List<Prescription>> GetPrescriptions();
    Task<List<Medicament>> GetMedicaments();
    Task<Doctor?> GetDoctor(int id);
    Task<Patient?> GetPatient(int id);
    PatientDetailsDto? GetPatientDetailsAsync(int id);
}