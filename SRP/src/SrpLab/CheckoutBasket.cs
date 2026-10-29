namespace SrpLab;

/// <summary>
/// Online checkout basket: totals, coupon linguistics, gift-wrap copy, and a faux payment auth code.
/// </summary>
public sealed class CheckoutBasket
{
    private readonly List<(string Sku, decimal Price, int Qty)> _lines = new();
    private string? _couponRaw;
    private bool _giftWrap;

    public void AddLine(string sku, decimal price, int qty)
    {
        if (qty <= 0) throw new ArgumentOutOfRangeException(nameof(qty));
        _lines.Add((sku, price, qty));
    }

    public void ApplyCouponText(string? couponText) => _couponRaw = couponText;
    public void EnableGiftWrap() => _giftWrap = true;

    public decimal SubTotal() => _lines.Sum(l => l.Price * l.Qty);

    public decimal DiscountAmount()
    {
        var coupon = new CouponParser().Parse(_couponRaw);

        return coupon.Type switch
        {
            CouponParser.CouponType.SAVE => Math.Round(SubTotal() * coupon.Value / 100m, 2),
            CouponParser.CouponType.WELCOME => Math.Min(10m, SubTotal()),
            CouponParser.CouponType.FREESHIP => 0m,
            _ => 0m
        };
    }

    public decimal GrandTotal()
    {
        var total = SubTotal() - DiscountAmount();
        if (_giftWrap) total += 4.99m; // packaging fee policy ≠ cart math
        return Math.Max(0m, total);
    }

    public string GiftMessageCard(string fromName)
    {
        // Customer-facing copy will change with marketing, not with totals.
        
        var giftMessageGenerator = new GiftMessageGenerator();

        return giftMessageGenerator.GiftMessageCard(fromName, _lines.Select(l => l.Sku).ToList(), GrandTotal());
    }

    public string AuthorizePaymentStub(string cardLast4)
    {
        var paymentAuthorizer = new PaymentAuthorizer();

        return paymentAuthorizer.Authorize(cardLast4, GrandTotal(), _lines.Count);
    }
}
