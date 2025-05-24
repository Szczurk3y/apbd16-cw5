namespace apbd_cw5.DTOs;

public class PrescriptionDto
{
    public PatientDto Patient { get; set; }
    public List<PrescriptionMedicamentDto> Medicaments { get; set; } = [];

    public int IdDoctor { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }

    public override string ToString()
    {
        return $"""
               Patient: {Patient}
               IdDoctor: {IdDoctor}
               Date: {Date}
               DueDate: {DueDate}
               Medicaments: {string.Join(", ", Medicaments)}
               """;
    }
}