namespace MVCCourse.Models;

public class Transaction
{
    public int TransactionId { get; set; }
    public DateTime TimeStamp { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = ""; //Save because the name of the product may change
    public double Price { get; set; } //Save because the price of the product may change
    public int BeforeQty { get; set; }
    public int SoldQty { get; set; }
    public string CashierName { get; set; } = "";
}