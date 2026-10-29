namespace SrpLab;

public class GiftMessageGenerator
{
    public string GiftMessageCard(string fromName, IEnumerable<string> items, decimal grandTotal)
    {
        var itemList = string.Join(", ", items);
        return $"Dear friend,\nA gift from {fromName} awaits ({itemList}).\nTotal surprise value: {grandTotal:C}\n";
    }
}