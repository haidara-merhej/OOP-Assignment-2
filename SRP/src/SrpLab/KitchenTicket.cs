namespace SrpLab;

/// <summary>
/// Kitchen ticket: allergen scan from ingredients, cook ETA heuristics, and printed ticket layout.
/// </summary>
public sealed class KitchenTicket
{   
    private readonly List<KitchenTicketItem> _items = new();
    private readonly KitchenAllergenDetector _allergenDetector = new();
    private readonly KitchenReadyTimeEstimator _timeEstimator = new();
    private readonly ThermalTicketFormatter _ticketFormatter = new();
    

    public void AddItem(string item, IEnumerable<string> ingredients, int prepMinutes)
    {
        _items.Add(new KitchenTicketItem (item, 
            ingredients.Select(i => i.Trim().ToLowerInvariant()).ToList(), prepMinutes));
    }

    public IReadOnlyList<string> DetectAllergens()
    {
        // Regulatory allergen dictionary changes separately from ticket layout.

        return _allergenDetector.Detect(_items);
    }

    public int EstimatedReadyMinutes(int openStations)
    {
        // Kitchen ops model ≠ printing.
        return _timeEstimator.Estimate(openStations, _items, DetectAllergens().Count);
    }

    public string RenderThermalTicket(int orderNumber)
    {
        // Hardware/formatting concerns — width, separators — change with printer vendor.

        var allergens = DetectAllergens();
        int estimatedMinutes = EstimatedReadyMinutes(2);
        return _ticketFormatter.Render(orderNumber, _items, estimatedMinutes, allergens);
    }

    public string ExpoLaneHint()
    {
        return DetectAllergens().Count > 0 ? "LANE-ALLERGY" : EstimatedReadyMinutes(2) > 20 ? "LANE-SLOW" : "LANE-FAST";
    }
}
