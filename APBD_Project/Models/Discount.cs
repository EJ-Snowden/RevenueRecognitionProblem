namespace APBD_Project.Models;

public class Discount
{
    public int DiscountId { get; set; }
    public string Name { get; set; }
    public string OfferType { get; set; }
    public decimal Value { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int SoftwareId { get; set; }
    public Software Software { get; set; }
}