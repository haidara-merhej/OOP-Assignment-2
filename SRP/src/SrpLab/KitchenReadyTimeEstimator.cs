namespace SrpLab;

public class KitchenReadyTimeEstimator
{
    public int Estimate(int openStations, IReadOnlyList<KitchenTicketItem> items, int allergenCount)
    {
        // Kitchen ops model ≠ printing.
        if (openStations <= 0) openStations = 1;
        var sequential = items.Sum(i => i.PrepMinutes);
        var parallel = (int)Math.Ceiling(sequential / (double)openStations);
        if (allergenCount > 0) parallel += 3; // allergy protocol delay mixed in
        var longest = items.Count == 0 ? 0 : items.Max(i => i.PrepMinutes);
        return Math.Max(parallel, longest);
    }
}