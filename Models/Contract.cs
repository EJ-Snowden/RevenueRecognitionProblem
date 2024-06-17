namespace APBD_Project.Models;

public class Contract
{
    public int ContractId { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; }
    public int SoftwareId { get; set; }
    public Software Software { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
    public bool IsSigned { get; set; }
    public int SupportYears { get; set; }
    
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
