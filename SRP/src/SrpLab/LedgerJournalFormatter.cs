namespace SrpLab;

public class LedgerJournalFormatter
{
    public string Format(string customerId, string invoice, decimal amount)
    {
        // Accounting export format is another axis of change.
        return $"{customerId},{invoice},{amount:0.00},AR-SUB";
    }
}