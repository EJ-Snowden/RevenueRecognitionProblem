namespace APBD_Project.Models;

public class Software
{
    public int SoftwareId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CurrentVersion { get; set; }
    public string Category { get; set; }
    public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
}