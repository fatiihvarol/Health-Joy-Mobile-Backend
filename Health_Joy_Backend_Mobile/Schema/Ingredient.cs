using Health_Joy_Mobile_Backend.Data.Entity;

namespace Health_Joy_Mobile_Backend.Schema;


public class IngredientRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int RiskLevel { get; set; }
}

public class IngredientResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int RiskLevel { get; set; }
}


public class ProductIngredientResponse
{
    public List<Ingredient> Ing { get; set; }
    public double AverageRiskLevel { get; set; }
}

