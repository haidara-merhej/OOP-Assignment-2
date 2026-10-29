namespace SrpLab;

public class PaymentAuthorizer
{
    public string Authorize(string cardLast4, decimal grandTotal, int itemsCount)
    {
                // Pretends to talk to a gateway — auth scheme changes independently of cart rules.
        var payload = $"{grandTotal:0.00}|{cardLast4}|{itemsCount}";
        var hash = payload.GetHashCode();
        return $"AUTH-{Math.Abs(hash):X8}";
    }
}