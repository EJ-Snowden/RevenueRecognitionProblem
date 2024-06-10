namespace APBD_Project.Models;

public class Discount
{
    public int DiscountId { get; set; }
    public string Name { get; set; }
    public decimal Percentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
