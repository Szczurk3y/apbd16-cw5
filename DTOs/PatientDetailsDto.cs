namespace apbd_cw5.DTOs;

public class PatientDetailsDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public List<PrescriptionDto> Prescriptions { get; set; }
}