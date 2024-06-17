namespace APBD_Project.DTOs;

public class DiscountDto
{
    public string Name { get; set; }
    public string OfferType { get; set; }
    public decimal Value { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}