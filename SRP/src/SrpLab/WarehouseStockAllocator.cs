namespace SrpLab;

public class WarehouseStockAllocator
{
    public IReadOnlyList<(string Sku, int Allocated)> Allocate(IEnumerable<(string Sku, string Aisle, int Bin,
         int QtyNeeded, int QtyOnHand)> lines)
    {
        // Allocation/backorder policy ≠ walking path ≠ human instructions.
        var result = new List<(string, int)>();
        foreach (var line in lines)
        {
            var alloc = Math.Min(line.QtyNeeded, line.QtyOnHand);
            result.Add((line.Sku, alloc));
        }
        return result;
    }
}