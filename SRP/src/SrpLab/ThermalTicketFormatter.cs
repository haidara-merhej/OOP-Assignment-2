namespace SrpLab;

public class ThermalTicketFormatter
{
    public string Render(int orderNumber, IReadOnlyList<KitchenTicketItem> items, 
        int estimatedMinutes, IReadOnlyList<string> allergens)
    {
        // Hardware/formatting concerns — width, separators — change with printer vendor.
        var width = 32;
        var line = new string('=', width);
        var body = string.Join('\n', items.Select(i => $"* {i.Item.ToUpperInvariant()} ({i.PrepMinutes}m)"));
        var allergyLine = allergens.Count == 0 ? "ALLERGENS: none" : "ALLERGENS: " + string.Join(",", allergens);
        return $"{line}\nORDER #{orderNumber}\nETA {estimatedMinutes} MIN\n{body}\n{allergyLine}\n{line}\n";
    }
}