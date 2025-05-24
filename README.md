Ćwiczenia 5 - Maciej Stoiński s24196

1) POST http://localhost:5107/api/prescriptions
body->raw :
{
    "Patient": {
        "IdPatient": 3,
        "FirstName": "TestCreatedJohn",
        "LastName": "TestCreatedLastName",
        "BirthDate": "2000-10-05"
    },
    "Medicaments": [{
        "IdMedicament": 1,
        "Dose": 5,
        "Description": "Some desc.."
    }],
    "IdDoctor": 1,
    "Date": "2025-05-22",
    "DueDate": "2025-06-01"
}

2) GET http://localhost:5107/api/patients/1
