namespace SrpLab;

/// <summary>
/// Warehouse pick list: naive path order, stock allocation, and picker instruction prose.
/// </summary>
public sealed class WarehousePickList
{
    private readonly WarehouseStockAllocator _stockAllocator = new();
    private readonly WarehousePathPlanner _pathPlanner = new();
    private readonly PickerInstructionFormatter _instructionFormatter = new();
    private readonly WmsXmlBatchFormatter _xmlFormatter = new();

    
    private readonly List<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> _lines = new();

    public void AddNeed(string sku, string aisle, int bin, int qtyNeeded, int qtyOnHand)
    {
        _lines.Add((sku, aisle, bin, qtyNeeded, qtyOnHand));
    }

    public IReadOnlyList<(string Sku, int Allocated)> Allocate()
    {
        // Allocation/backorder policy ≠ walking path ≠ human instructions.

        return _stockAllocator.Allocate(_lines);
    }

    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> WalkingOrder()
    {
        // Path heuristic will change with warehouse layout tech.
        return _pathPlanner.WalkingOrder(_lines);
    }

    public string PickerScript()
    {
        // UX wording for handheld devices — separate owners.

        return _instructionFormatter.Format(WalkingOrder(), Allocate(), _lines);
    }

    public string WmsXmlBatch(string batchId)
    {
        // Integration contract with WMS — third reason to change.
        return _xmlFormatter.Format(batchId, Allocate());
    }
}
