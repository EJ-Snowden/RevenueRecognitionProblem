namespace APBD_Project.Models;

public class Client
{
    public int ClientId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string PESEL { get; set; } // For individuals
    public string CompanyName { get; set; } // For companies
    public string KRS { get; set; } // For companies
    public bool IsDeleted { get; set; }
}
