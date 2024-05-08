using System;

namespace TechnicalTest;

public class PatientDetails
{
    public int PatientId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Gender { get; set; }

    public DateTime Dob { get; set; }
        
    public decimal Height { get; set; }

    public decimal Weight { get; set; }
}