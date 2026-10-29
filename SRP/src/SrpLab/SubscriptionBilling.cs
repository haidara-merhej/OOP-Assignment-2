namespace SrpLab;

/// <summary>
/// Subscription billing: proration math, invoice number minting, and dunning email bodies.
/// </summary>
public sealed class SubscriptionBilling
{
    private readonly SubscriptionProrationCalculator _prorationCalculator = new();
    private readonly InvoiceNumberGenerator _invoiceNumberGenerator = new();
    private readonly DunnigEmailFormatter _emailFormatter = new();
    private readonly LedgerJournalFormatter _ledgerFormatter = new();

    
    public string CustomerId { get; }
    public decimal MonthlyPrice { get; }
    public DateOnly PeriodStart { get; }
    public DateOnly PeriodEnd { get; }
    public int FailedPayments { get; private set; }

    public SubscriptionBilling(string customerId, decimal monthlyPrice, DateOnly periodStart, DateOnly periodEnd)
    {
        CustomerId = customerId;
        MonthlyPrice = monthlyPrice;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
    }

    public decimal Prorate(DateOnly activeFrom)
    {
        // Finance calendar rules change independently of email copy.

        return _prorationCalculator.Prorate(activeFrom, PeriodStart, PeriodEnd, MonthlyPrice);
    }

    public string NextInvoiceNumber()
    {
        // Numbering scheme / fiscal prefixes — ops concern, not pricing.
        return _invoiceNumberGenerator.Generate(PeriodStart);
    }

    public void RegisterFailedPayment() => FailedPayments++;

    public string DunningEmail(string customerName, DateOnly asOf)
    {
        // Collections tone & legal boilerplate ≠ proration formula.
        var amount = Prorate(PeriodStart);
        var invoice = NextInvoiceNumber(); // side-effect while composing mail — nasty on purpose

        return _emailFormatter.DunningEmail(customerName, asOf, amount, FailedPayments, invoice);
    }

    public string LedgerJournalLine(DateOnly activeFrom)
    {
        // Accounting export format is another axis of change.
        var invoice = NextInvoiceNumber();
        var amount = Prorate(activeFrom);
        
        return _ledgerFormatter.Format(CustomerId, invoice, amount);
    }
}
