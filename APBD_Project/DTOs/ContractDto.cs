namespace APBD_Project.DTOs;

public class ContractDto
{
    public int ClientId { get; set; }
    public int SoftwareId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
    public bool IsSigned { get; set; }
    public int SupportYears { get; set; } = 0;
}