namespace SrpLab;

public class CouponParser
{
    public enum CouponType
    {
        NONE,
        SAVE,
        WELCOME,
        FREESHIP
    };


    public (CouponType Type, int Value) Parse(string? couponRaw)
    {
        if (string.IsNullOrWhiteSpace(couponRaw)) return (CouponType.NONE, 0);

        var t = couponRaw.Trim().ToUpperInvariant();

        if(t.StartsWith("SAVE") && int.TryParse(t[4..], out var pct) && pct is > 0 and <= 50)
        {
            return (CouponType.SAVE, pct);
        }

        if (t.Contains("FREESHIP")) return (CouponType.FREESHIP, 0);

        if (t == "WELCOME10") return (CouponType.WELCOME, 10);

        return (CouponType.NONE, 0);
    }
}