using apbd_cw5.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_cw5.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(d =>
        {
            d.ToTable("Doctor");
            d.HasKey(e => e.DoctorId);
            d.Property(e => e.FirstName).HasMaxLength(100);
            d.Property(e => e.LastName).HasMaxLength(100);
            d.Property(e => e.Email).HasMaxLength(100);
        });
        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>()
        {
            new Doctor() { DoctorId = 1, FirstName = "Doctor Jane", LastName = "Doe", Email = "jane@doe.com" },
            new Doctor() { DoctorId = 2, FirstName = "Doctor John", LastName = "Doe", Email = "john@doe.com" }
        });

        modelBuilder.Entity<Patient>(p =>
        {
            p.ToTable("Patient");
            p.HasKey(e => e.IdPatient);
            p.Property(e => e.FirstName).HasMaxLength(100);
            p.Property(e => e.LastName).HasMaxLength(100);
        });

        modelBuilder.Entity<Patient>().HasData(new List<Patient>()
        {
            new Patient
            {
                IdPatient = 1,
                FirstName = "Patient Jane",
                LastName = "Doe",
                BirthDate = DateTime.Today.AddYears(-25)
            },
            new Patient
            {
                IdPatient = 2,
                FirstName = "Patient John",
                LastName = "Doe",
                BirthDate = DateTime.Today.AddYears(-20)
            },
        });

        modelBuilder.Entity<Medicament>(p =>
        {
            p.ToTable("Medicament");
            p.HasKey(e => e.MedicamentId);
            p.Property(e => e.Name).HasMaxLength(100);
            p.Property(e => e.Description).HasMaxLength(500);
            p.Property(e => e.Type).HasMaxLength(100);
        });
        
        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>()
        {
            new Medicament
            {
                MedicamentId = 1,
                Name = "Medicament 1",
                Description = "Description 1",
                Type = "Type 1"
            },
            new Medicament
            {
                MedicamentId = 2,
                Name = "Medicament 2",
                Description = "Description 2",
                Type = "Type 2"
            }
        });

        modelBuilder.Entity<Prescription>(p =>
        {
            p.ToTable("Prescription");
            p.HasKey(e => e.PrescriptionId);
        });

        modelBuilder.Entity<Prescription>().HasData(new List<Prescription>()
        {
            new Prescription
            {
                PrescriptionId = 1,
                Date = DateTime.Today.AddDays(-5),
                DueDate = DateTime.Today.AddDays(15),
                PatientId = 1,
                DoctorId = 2
            },
            new Prescription
            {
                PrescriptionId = 2,
                Date = DateTime.Today.AddDays(-3),
                DueDate = DateTime.Today.AddDays(12),
                PatientId = 2,
                DoctorId = 1
            },
        });
        
        modelBuilder.Entity<PrescriptionMedicament>(pm =>
        {
            pm.ToTable("PrescriptionMedicament");
            pm.HasKey(e => new { e.PrescriptionId, e.MedicamentId });
            pm.Property(e => e.Details).HasMaxLength(100);
        });

        modelBuilder.Entity<PrescriptionMedicament>().HasData(new List<PrescriptionMedicament>()
        {
            new PrescriptionMedicament
            {
                MedicamentId = 1,
                PrescriptionId = 1,
                Dose = 10,
                Details = "Some details 1"
            },
            new PrescriptionMedicament
            {
                MedicamentId = 2,
                PrescriptionId = 2,
                Dose = 6,
                Details = "Some details 2"
            }
        });

    }
}