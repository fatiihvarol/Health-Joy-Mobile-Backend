namespace Health_Joy_Mobile_Backend.Schema;


public class ProductRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal TotalRiskValue { get; set; }
    public string Image { get; set; }
    public string ProductType { get; set; }
}

public class ProductResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal TotalRiskValue { get; set; }
    public string Image { get; set; }
    public string ProductType { get; set; }
}